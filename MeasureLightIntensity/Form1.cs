using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OpenCvSharp;

namespace MeasureLightIntensity
{
    public partial class MainForm : Form
    {
        private VideoCapture? capture;
        private System.Windows.Forms.Timer? timer;

        // Die aktuell gewählte Zeitspanne für die X-Achse (Standard: 1 Minute)
        private TimeSpan? displayWindow = TimeSpan.FromMinutes(1);

        public MainForm()
        {
            InitializeComponent();
            ForceTextBoxStyles(); // Erzwingt Weiß und Editierbar direkt im Code

            // 1. Kamera initialisieren
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("Die Notebook-Kamera konnte nicht geöffnet werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Dropdown für Zeitspanne füllen
            comboBoxTimeSpan.Items.Clear();
            comboBoxTimeSpan.Items.Add("1 Minute");
            comboBoxTimeSpan.Items.Add("5 Minuten");
            comboBoxTimeSpan.Items.Add("10 Minuten");
            comboBoxTimeSpan.Items.Add("Alles anzeigen");
            comboBoxTimeSpan.SelectedIndex = 0; // Wählt "1 Minute"

            // 3. Timer initialisieren
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)numericUpDownInterval.Value * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            // 4. UI vorbereiten
            ResetLog();
            SetupChart();
        }

        private void ForceTextBoxStyles()
        {
            richTextBoxLog.ReadOnly = false;
            richTextBoxLog.BackColor = System.Drawing.Color.White;
            richTextBoxLog.ForeColor = System.Drawing.Color.Black;
        }

        private void SetupChart()
        {
            chart1.Series.Clear();
            var series = chart1.Series.Add("Helligkeit");

            // Standard-Linie ist stabiler bei Echtzeit-Zeichnungen
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = System.Drawing.Color.DodgerBlue;

            // Wichtig: Dem Chart explizit sagen, dass die X-Achse ein Datum/Uhrzeit ist
            series.XValueType = ChartValueType.DateTime;

            // Y-Achse (0-255)
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 255;
            chart1.ChartAreas[0].AxisY.Title = "Luminanz";

            // X-Achse
            chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisX.Title = "Uhrzeit";
        }

        private void ResetLog()
        {
            richTextBoxLog.Clear();
            richTextBoxLog.AppendText($"Zeitstempel\t\tHelligkeitswert (0 - 255){Environment.NewLine}");
            richTextBoxLog.AppendText($"---------------------------------------------------------{Environment.NewLine}");
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (capture == null || !capture.IsOpened()) return;

            using (Mat frame = new Mat())
            {
                capture.Read(frame);
                if (frame.Empty()) return;

                using (Mat ycrcb = new Mat())
                {
                    Cv2.CvtColor(frame, ycrcb, ColorConversionCodes.BGR2YCrCb);
                    Scalar mean = ycrcb.Mean();
                    double brightness = Math.Round(mean.Val0, 2);

                    UpdateUI(brightness);
                }
            }
        }

        private void UpdateUI(double brightnessValue)
        {
            DateTime currentTime = DateTime.Now;

            // --- 1. CHART AKTUALISIEREN ---

            // Punkt einfach ans Ende anhängen (ohne vorher Clear() aufzurufen!)
            chart1.Series["Helligkeit"].Points.AddXY(currentTime, brightnessValue);

            // Arbeitsspeicher schonen: Alle Punkte, die älter als 60 Minuten sind, direkt aus dem Chart löschen
            double minLimit = currentTime.AddMinutes(-60).ToOADate();
            while (chart1.Series["Helligkeit"].Points.Count > 0 && chart1.Series["Helligkeit"].Points[0].XValue < minLimit)
            {
                chart1.Series["Helligkeit"].Points.RemoveAt(0);
            }

            // Rolling Window berechnen: X-Achse verschieben
            if (displayWindow.HasValue)
            {
                chart1.ChartAreas[0].AxisX.Minimum = (currentTime - displayWindow.Value).ToOADate();
                // WICHTIG: 1 Sekunde Puffer in die Zukunft, damit der aktuellste Punkt gezeichnet wird!
                chart1.ChartAreas[0].AxisX.Maximum = currentTime.AddSeconds(1).ToOADate();
            }
            else
            {
                // "Alles anzeigen" Modus
                chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
            }

            // --- 2. TEXTBOX AKTUALISIEREN ---

            string timestamp = currentTime.ToString("HH:mm:ss");
            richTextBoxLog.AppendText($"{timestamp}\t\t{brightnessValue}{Environment.NewLine}");

            if (richTextBoxLog.Lines.Length > 250)
            {
                richTextBoxLog.SelectionStart = 0;
                richTextBoxLog.SelectionLength = richTextBoxLog.GetFirstCharIndexFromLine(richTextBoxLog.Lines.Length - 250);
                richTextBoxLog.SelectedText = "";
            }

            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            richTextBoxLog.ScrollToCaret();
        }

        private void comboBoxTimeSpan_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (comboBoxTimeSpan.SelectedIndex)
            {
                case 0: displayWindow = TimeSpan.FromMinutes(1); break;
                case 1: displayWindow = TimeSpan.FromMinutes(5); break;
                case 2: displayWindow = TimeSpan.FromMinutes(10); break;
                case 3: displayWindow = null; break; // Alles anzeigen
            }
        }

        private void numericUpDownInterval_ValueChanged(object? sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Interval = (int)numericUpDownInterval.Value * 1000;
            }
        }

        private void buttonClear_Click(object? sender, EventArgs e)
        {
            // Jetzt müssen wir nur noch das Chart leeren, da die History weg ist
            chart1.Series["Helligkeit"].Points.Clear();
            ResetLog();
            ForceTextBoxStyles();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timer != null) { timer.Stop(); timer.Dispose(); }
            if (capture != null) { capture.Release(); capture.Dispose(); }
            base.OnFormClosing(e);
        }
    }
}