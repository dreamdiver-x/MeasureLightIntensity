namespace MeasureLightIntensity
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.comboBoxTimeSpan = new System.Windows.Forms.ComboBox();
            this.labelTimeSpan = new System.Windows.Forms.Label();
            this.checkBoxCsv = new System.Windows.Forms.CheckBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.checkBoxAutoExposure = new System.Windows.Forms.CheckBox();
            this.trackBarExposure = new System.Windows.Forms.TrackBar();
            this.labelExposure = new System.Windows.Forms.Label();
            this.labelExposureValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Helligkeit";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(760, 250);
            this.chart1.TabIndex = 0;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 280);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxLog.Size = new System.Drawing.Size(760, 220);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // labelTimeSpan
            // 
            this.labelTimeSpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTimeSpan.AutoSize = true;
            this.labelTimeSpan.Location = new System.Drawing.Point(12, 523);
            this.labelTimeSpan.Name = "labelTimeSpan";
            this.labelTimeSpan.Size = new System.Drawing.Size(68, 15);
            this.labelTimeSpan.TabIndex = 6;
            this.labelTimeSpan.Text = "Zeitspanne:";
            // 
            // comboBoxTimeSpan
            // 
            this.comboBoxTimeSpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxTimeSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeSpan.FormattingEnabled = true;
            this.comboBoxTimeSpan.Location = new System.Drawing.Point(85, 520);
            this.comboBoxTimeSpan.Name = "comboBoxTimeSpan";
            this.comboBoxTimeSpan.Size = new System.Drawing.Size(100, 23);
            this.comboBoxTimeSpan.TabIndex = 5;
            this.comboBoxTimeSpan.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeSpan_SelectedIndexChanged);
            // 
            // labelInterval
            // 
            this.labelInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(200, 523);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(67, 15);
            this.labelInterval.TabIndex = 3;
            this.labelInterval.Text = "Intervall (s):";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownInterval.Location = new System.Drawing.Point(275, 520);
            this.numericUpDownInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownInterval.TabIndex = 2;
            this.numericUpDownInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDownInterval.ValueChanged += new System.EventHandler(this.numericUpDownInterval_ValueChanged);
            // 
            // checkBoxCsv
            // 
            this.checkBoxCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCsv.AutoSize = true;
            this.checkBoxCsv.Location = new System.Drawing.Point(345, 522);
            this.checkBoxCsv.Name = "checkBoxCsv";
            this.checkBoxCsv.Size = new System.Drawing.Size(73, 19);
            this.checkBoxCsv.TabIndex = 7;
            this.checkBoxCsv.Text = "CSV-Log";
            this.checkBoxCsv.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoExposure
            // 
            this.checkBoxAutoExposure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAutoExposure.AutoSize = true;
            this.checkBoxAutoExposure.Location = new System.Drawing.Point(12, 562);
            this.checkBoxAutoExposure.Name = "checkBoxAutoExposure";
            this.checkBoxAutoExposure.Size = new System.Drawing.Size(111, 19);
            this.checkBoxAutoExposure.TabIndex = 9;
            this.checkBoxAutoExposure.Text = "Auto-Belichtung";
            this.checkBoxAutoExposure.UseVisualStyleBackColor = true;
            this.checkBoxAutoExposure.CheckedChanged += new System.EventHandler(this.checkBoxAutoExposure_CheckedChanged);
            // 
            // labelExposure
            // 
            this.labelExposure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExposure.AutoSize = true;
            this.labelExposure.Location = new System.Drawing.Point(145, 563);
            this.labelExposure.Name = "labelExposure";
            this.labelExposure.Size = new System.Drawing.Size(65, 15);
            this.labelExposure.TabIndex = 11;
            this.labelExposure.Text = "Belichtung:";
            // 
            // trackBarExposure
            // 
            this.trackBarExposure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarExposure.Location = new System.Drawing.Point(215, 557);
            this.trackBarExposure.Maximum = -1;
            this.trackBarExposure.Minimum = -13;
            this.trackBarExposure.Name = "trackBarExposure";
            this.trackBarExposure.Size = new System.Drawing.Size(140, 45);
            this.trackBarExposure.TabIndex = 10;
            this.trackBarExposure.Value = -6;
            this.trackBarExposure.Scroll += new System.EventHandler(this.trackBarExposure_Scroll);
            // 
            // labelExposureValue
            // 
            this.labelExposureValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExposureValue.AutoSize = true;
            this.labelExposureValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelExposureValue.Location = new System.Drawing.Point(360, 563);
            this.labelExposureValue.Name = "labelExposureValue";
            this.labelExposureValue.Size = new System.Drawing.Size(33, 15);
            this.labelExposureValue.TabIndex = 12;
            this.labelExposureValue.Text = "Auto";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonStartStop.Location = new System.Drawing.Point(540, 535);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(100, 35);
            this.buttonStartStop.TabIndex = 8;
            this.buttonStartStop.Text = "Start";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(660, 535);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(110, 35);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Log leeren";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 601);
            this.Controls.Add(this.labelExposureValue);
            this.Controls.Add(this.labelExposure);
            this.Controls.Add(this.trackBarExposure);
            this.Controls.Add(this.checkBoxAutoExposure);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.checkBoxCsv);
            this.Controls.Add(this.labelTimeSpan);
            this.Controls.Add(this.comboBoxTimeSpan);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.chart1);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "MainForm";
            this.Text = "Helligkeitsmessung mit OpenCV";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBoxTimeSpan;
        private System.Windows.Forms.Label labelTimeSpan;
        private System.Windows.Forms.CheckBox checkBoxCsv;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.CheckBox checkBoxAutoExposure;
        private System.Windows.Forms.TrackBar trackBarExposure;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.Label labelExposureValue;
    }
}