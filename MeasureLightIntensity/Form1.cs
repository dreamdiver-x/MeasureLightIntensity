using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
using OpenCvSharp;

namespace MeasureLightIntensity
{
    public class AppSettings
    {
        public int IntervalSeconds { get; set; } = 1;
        public int TimeSpanIndex { get; set; } = 0;
        public bool EnableCsvLogging { get; set; } = false;
        public bool AutoExposure { get; set; } = true;
        public int ExposureValue { get; set; } = -6;
    }

    public partial class MainForm : Form
    {
        private VideoCapture? capture;
        private System.Windows.Forms.Timer? timer;
        private TimeSpan? displayWindow = TimeSpan.FromMinutes(1);

        private string csvFilePath = "";
        private const string SettingsFilePath = "settings.xml";
        private bool isMeasuring = false;

        public MainForm()
        {
            InitializeComponent();
            ForceTextBoxStyles();

            // 1. Einstellungen laden
            LoadSettings();

            // 2. Kamera initialisieren
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("Die Notebook-Kamera konnte nicht geöffnet werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Kamera-Hardwareeinstellungen initial anwenden
            ApplyCameraSettings();

            // 4. Timer initialisieren
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)numericUpDownInterval.Value * 1000;
            timer.Tick += Timer_Tick;

            buttonStartStop.Text = "Start";
            buttonStartStop.BackColor = System.Drawing.Color.LightGreen;

            ResetLog();
            SetupChart();
        }

        private void LoadSettings()
        {
            comboBoxTimeSpan.Items.Clear();
            comboBoxTimeSpan.Items.Add("1 Minute");
            comboBoxTimeSpan.Items.Add("5 Minuten");
            comboBoxTimeSpan.Items.Add("10 Minuten");
            comboBoxTimeSpan.Items.Add("Alles anzeigen");

            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    using (StreamReader reader = new StreamReader(SettingsFilePath))
                    {
                        AppSettings? settings = (AppSettings?)serializer.Deserialize(reader);
                        if (settings != null)
                        {
                            numericUpDownInterval.Value = Math.Max(1, settings.IntervalSeconds);
                            comboBoxTimeSpan.SelectedIndex = settings.TimeSpanIndex;
                            checkBoxCsv.Checked = settings.EnableCsvLogging;
                            checkBoxAutoExposure.Checked = settings.AutoExposure;

                            // Sicherstellen, dass der geladene Wert im erlaubten Bereich der TrackBar liegt
                            if (settings.ExposureValue >= trackBarExposure.Minimum && settings.ExposureValue <= trackBarExposure.Maximum)
                            {
                                trackBarExposure.Value = settings.ExposureValue;
                            }
                            labelExposureValue.Text = trackBarExposure.Value.ToString();
                            return;
                        }
                    }
                }
            }
            catch { }

            // Standardwerte falls keine XML existiert
            comboBoxTimeSpan.SelectedIndex = 0;
            numericUpDownInterval.Value = 1;
            checkBoxCsv.Checked = false;
            checkBoxAutoExposure.Checked = true;
            trackBarExposure.Value = -6;
            labelExposureValue.Text = "-6";
        }

        private void SaveSettings()
        {
            try
            {
                AppSettings settings = new AppSettings
                {
                    IntervalSeconds = (int)numericUpDownInterval.Value,
                    TimeSpanIndex = comboBoxTimeSpan.SelectedIndex,
                    EnableCsvLogging = checkBoxCsv.Checked,
                    AutoExposure = checkBoxAutoExposure.Checked,
                    ExposureValue = trackBarExposure.Value
                };

                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                using (StreamWriter writer = new StreamWriter(SettingsFilePath))
                {
                    serializer.Serialize(writer, settings);
                }
            }
            catch { }
        }

        // Sendet die Belichtungsbefehle an die Kamera-Hardware
        private void ApplyCameraSettings()
        {
            if (capture == null || !capture.IsOpened()) return;

            if (checkBoxAutoExposure.Checked)
            {
                // Wert 1 (oder je nach Treiber 3) aktiviert die Kamera-Automatik
                capture.Set(VideoCaptureProperties.AutoExposure, 1);
                trackBarExposure.Enabled = false;
                labelExposureValue.Text = "Auto";
            }
            else
            {
                // Wert 0 schaltet die Automatik ab (Manuell)
                capture.Set(VideoCaptureProperties.AutoExposure, 0);
                trackBarExposure.Enabled = true;

                // Schreibt den Schieberegler-Wert direkt in den Kamerasensor
                capture.Set(VideoCaptureProperties.Exposure, trackBarExposure.Value);
                labelExposureValue.Text = trackBarExposure.Value.ToString();
            }
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
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = System.Drawing.Color.DodgerBlue;
            series.XValueType = ChartValueType.DateTime;

            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 255;
            chart1.ChartAreas[0].AxisY.Title = "Luminanz";

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

        private void buttonStartStop_Click(object? sender, EventArgs e)
        {
            isMeasuring = !isMeasuring;

            if (isMeasuring)
            {
                string timestampForFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Messung_{timestampForFile}.csv");

                if (timer != null) timer.Start();
                buttonStartStop.Text = "Stopp";
                buttonStartStop.BackColor = System.Drawing.Color.LightCoral;
            }
            else
            {
                if (timer != null) timer.Stop();
                buttonStartStop.Text = "Start";
                buttonStartStop.BackColor = System.Drawing.Color.LightGreen;
            }
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

            chart1.Series["Helligkeit"].Points.AddXY(currentTime, brightnessValue);

            double minLimit = currentTime.AddMinutes(-60).ToOADate();
            while (chart1.Series["Helligkeit"].Points.Count > 0 && chart1.Series["Helligkeit"].Points[0].XValue < minLimit)
            {
                chart1.Series["Helligkeit"].Points.RemoveAt(0);
            }

            if (displayWindow.HasValue)
            {
                chart1.ChartAreas[0].AxisX.Minimum = (currentTime - displayWindow.Value).ToOADate();
                chart1.ChartAreas[0].AxisX.Maximum = currentTime.AddSeconds(1).ToOADate();
            }
            else
            {
                chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
            }

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

            if (checkBoxCsv.Checked && !string.IsNullOrEmpty(csvFilePath))
            {
                try
                {
                    if (!File.Exists(csvFilePath))
                    {
                        File.WriteAllText(csvFilePath, $"Zeitstempel;Helligkeit (Luminanz){Environment.NewLine}");
                    }
                    File.AppendAllText(csvFilePath, $"{timestamp};{brightnessValue}{Environment.NewLine}");
                }
                catch { }
            }
        }

        private void comboBoxTimeSpan_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (comboBoxTimeSpan.SelectedIndex)
            {
                case 0: displayWindow = TimeSpan.FromMinutes(1); break;
                case 1: displayWindow = TimeSpan.FromMinutes(5); break;
                case 2: displayWindow = TimeSpan.FromMinutes(10); break;
                case 3: displayWindow = null; break;
            }
        }

        private void numericUpDownInterval_ValueChanged(object? sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Interval = (int)numericUpDownInterval.Value * 1000;
            }
        }

        // Event: Checkbox für Auto-Belichtung wurde geklickt
        private void checkBoxAutoExposure_CheckedChanged(object? sender, EventArgs e)
        {
            ApplyCameraSettings();
        }

        // Event: Schieberegler für Belichtungszeit wurde bewegt
        private void trackBarExposure_Scroll(object? sender, EventArgs e)
        {
            ApplyCameraSettings();
        }

        private void buttonClear_Click(object? sender, EventArgs e)
        {
            chart1.Series["Helligkeit"].Points.Clear();
            ResetLog();
            ForceTextBoxStyles();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            if (timer != null) { timer.Stop(); timer.Dispose(); }
            if (capture != null) { capture.Release(); capture.Dispose(); }
            base.OnFormClosing(e);
        }
    }
}