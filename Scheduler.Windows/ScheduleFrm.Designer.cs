
namespace Scheduler.Windows
{
    partial class ScheduleFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.txNextExecutionDate = new System.Windows.Forms.TextBox();
            this.btnGenerateNextExecutionDate = new System.Windows.Forms.Button();
            this.lbCurrentDate = new System.Windows.Forms.Label();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.lbNextExecutionDate = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.RichTextBox();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.lbEvery = new System.Windows.Forms.Label();
            this.lbDateTime = new System.Windows.Forms.Label();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.lbRecurringType = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbFrequencyUnit = new System.Windows.Forms.Label();
            this.nudFrequency = new System.Windows.Forms.NumericUpDown();
            this.cbRecurringTypes = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.gbLimits = new System.Windows.Forms.GroupBox();
            this.lbEndDate = new System.Windows.Forms.Label();
            this.lbStartDate = new System.Windows.Forms.Label();
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.gbOutput.SuspendLayout();
            this.gbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).BeginInit();
            this.gbLimits.SuspendLayout();
            this.gbInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpCurrentDate
            // 
            this.dtpCurrentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrentDate.Location = new System.Drawing.Point(101, 40);
            this.dtpCurrentDate.Name = "dtpCurrentDate";
            this.dtpCurrentDate.Size = new System.Drawing.Size(200, 23);
            this.dtpCurrentDate.TabIndex = 0;
            this.dtpCurrentDate.ValueChanged += new System.EventHandler(this.dtpCurrentDate_ValueChanged);
            // 
            // dtpNextExecutionDate
            // 
            this.txNextExecutionDate.ReadOnly = true;
            this.txNextExecutionDate.Location = new System.Drawing.Point(354, 20);
            this.txNextExecutionDate.Name = "txNextExecutionDate";
            this.txNextExecutionDate.Size = new System.Drawing.Size(200, 23);
            this.txNextExecutionDate.TabIndex = 1;
            // 
            // btnGenerateNextExecutionDate
            // 
            this.btnGenerateNextExecutionDate.Location = new System.Drawing.Point(354, 40);
            this.btnGenerateNextExecutionDate.Name = "btnGenerateNextExecutionDate";
            this.btnGenerateNextExecutionDate.Size = new System.Drawing.Size(200, 23);
            this.btnGenerateNextExecutionDate.TabIndex = 2;
            this.btnGenerateNextExecutionDate.Text = "Calculate next date";
            this.btnGenerateNextExecutionDate.UseVisualStyleBackColor = true;
            this.btnGenerateNextExecutionDate.Click += new System.EventHandler(this.btnGenerateNextExecutionDate_Click);
            // 
            // lbCurrentDate
            // 
            this.lbCurrentDate.AutoSize = true;
            this.lbCurrentDate.Location = new System.Drawing.Point(10, 44);
            this.lbCurrentDate.Name = "lbCurrentDate";
            this.lbCurrentDate.Size = new System.Drawing.Size(86, 15);
            this.lbCurrentDate.TabIndex = 3;
            this.lbCurrentDate.Text = "lb_CurrentDate";
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.lbNextExecutionDate);
            this.gbOutput.Controls.Add(this.tbDescription);
            this.gbOutput.Controls.Add(this.txNextExecutionDate);
            this.gbOutput.Location = new System.Drawing.Point(13, 338);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(562, 100);
            this.gbOutput.TabIndex = 4;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // lbNextExecutionDate
            // 
            this.lbNextExecutionDate.AutoSize = true;
            this.lbNextExecutionDate.Location = new System.Drawing.Point(10, 24);
            this.lbNextExecutionDate.Name = "lbNextExecutionDate";
            this.lbNextExecutionDate.Size = new System.Drawing.Size(123, 15);
            this.lbNextExecutionDate.TabIndex = 4;
            this.lbNextExecutionDate.Text = "lb_NextExecutionDate";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(10, 46);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(544, 50);
            this.tbDescription.TabIndex = 3;
            this.tbDescription.Text = "";
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.lbEvery);
            this.gbConfiguration.Controls.Add(this.lbDateTime);
            this.gbConfiguration.Controls.Add(this.dtpDateTime);
            this.gbConfiguration.Controls.Add(this.lbRecurringType);
            this.gbConfiguration.Controls.Add(this.lbType);
            this.gbConfiguration.Controls.Add(this.lbFrequencyUnit);
            this.gbConfiguration.Controls.Add(this.nudFrequency);
            this.gbConfiguration.Controls.Add(this.cbRecurringTypes);
            this.gbConfiguration.Controls.Add(this.cbType);
            this.gbConfiguration.Location = new System.Drawing.Point(13, 123);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(562, 100);
            this.gbConfiguration.TabIndex = 5;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration";
            // 
            // lbEvery
            // 
            this.lbEvery.AutoSize = true;
            this.lbEvery.Location = new System.Drawing.Point(381, 72);
            this.lbEvery.Name = "lbEvery";
            this.lbEvery.Size = new System.Drawing.Size(35, 15);
            this.lbEvery.TabIndex = 8;
            this.lbEvery.Text = "Every";
            // 
            // lbDateTime
            // 
            this.lbDateTime.AutoSize = true;
            this.lbDateTime.Location = new System.Drawing.Point(6, 28);
            this.lbDateTime.Name = "lbDateTime";
            this.lbDateTime.Size = new System.Drawing.Size(72, 15);
            this.lbDateTime.TabIndex = 7;
            this.lbDateTime.Text = "lb_DateTime";
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTime.Location = new System.Drawing.Point(84, 22);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(200, 23);
            this.dtpDateTime.TabIndex = 6;
            // 
            // lbRecurringType
            // 
            this.lbRecurringType.AutoSize = true;
            this.lbRecurringType.Location = new System.Drawing.Point(192, 72);
            this.lbRecurringType.Name = "lbRecurringType";
            this.lbRecurringType.Size = new System.Drawing.Size(59, 15);
            this.lbRecurringType.TabIndex = 5;
            this.lbRecurringType.Text = "lb_Occurs";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(6, 72);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(46, 15);
            this.lbType.TabIndex = 4;
            this.lbType.Text = "lb_Type";
            // 
            // lbFrequencyUnit
            // 
            this.lbFrequencyUnit.AutoSize = true;
            this.lbFrequencyUnit.Location = new System.Drawing.Point(501, 72);
            this.lbFrequencyUnit.Name = "lbFrequencyUnit";
            this.lbFrequencyUnit.Size = new System.Drawing.Size(44, 15);
            this.lbFrequencyUnit.TabIndex = 3;
            this.lbFrequencyUnit.Text = "lb_Unit";
            // 
            // nudFrequency
            // 
            this.nudFrequency.Location = new System.Drawing.Point(444, 68);
            this.nudFrequency.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFrequency.Name = "nudFrequency";
            this.nudFrequency.Size = new System.Drawing.Size(50, 23);
            this.nudFrequency.TabIndex = 2;
            this.nudFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbRecurringTypes
            // 
            this.cbRecurringTypes.FormattingEnabled = true;
            this.cbRecurringTypes.Location = new System.Drawing.Point(257, 68);
            this.cbRecurringTypes.Name = "cbRecurringTypes";
            this.cbRecurringTypes.Size = new System.Drawing.Size(96, 23);
            this.cbRecurringTypes.TabIndex = 1;
            this.cbRecurringTypes.SelectedIndexChanged += new System.EventHandler(this.cbRecurringTypes_SelectedIndexChanged);
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(58, 68);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(128, 23);
            this.cbType.TabIndex = 0;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(6, 64);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 23);
            this.dtpStartDate.TabIndex = 6;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(354, 64);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(200, 23);
            this.dtpEndDate.TabIndex = 7;
            // 
            // gbLimits
            // 
            this.gbLimits.Controls.Add(this.lbEndDate);
            this.gbLimits.Controls.Add(this.lbStartDate);
            this.gbLimits.Controls.Add(this.dtpStartDate);
            this.gbLimits.Controls.Add(this.dtpEndDate);
            this.gbLimits.Location = new System.Drawing.Point(13, 234);
            this.gbLimits.Name = "gbLimits";
            this.gbLimits.Size = new System.Drawing.Size(562, 93);
            this.gbLimits.TabIndex = 8;
            this.gbLimits.TabStop = false;
            this.gbLimits.Text = "Limits";
            // 
            // lbEndDate
            // 
            this.lbEndDate.AutoSize = true;
            this.lbEndDate.Location = new System.Drawing.Point(354, 28);
            this.lbEndDate.Name = "lbEndDate";
            this.lbEndDate.Size = new System.Drawing.Size(66, 15);
            this.lbEndDate.TabIndex = 9;
            this.lbEndDate.Text = "lb_EndDate";
            // 
            // lbStartDate
            // 
            this.lbStartDate.AutoSize = true;
            this.lbStartDate.Location = new System.Drawing.Point(6, 28);
            this.lbStartDate.Name = "lbStartDate";
            this.lbStartDate.Size = new System.Drawing.Size(70, 15);
            this.lbStartDate.TabIndex = 8;
            this.lbStartDate.Text = "lb_StartDate";
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.lbCurrentDate);
            this.gbInput.Controls.Add(this.dtpCurrentDate);
            this.gbInput.Controls.Add(this.btnGenerateNextExecutionDate);
            this.gbInput.Location = new System.Drawing.Point(13, 12);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(562, 100);
            this.gbInput.TabIndex = 3;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Input";
            // 
            // ScheduleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.gbLimits);
            this.Controls.Add(this.gbConfiguration);
            this.Controls.Add(this.gbOutput);
            this.Controls.Add(this.gbInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ScheduleFrm";
            this.Text = "Schedule";
            this.Load += new System.EventHandler(this.ScheduleFrm_Load);
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.gbConfiguration.ResumeLayout(false);
            this.gbConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).EndInit();
            this.gbLimits.ResumeLayout(false);
            this.gbLimits.PerformLayout();
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCurrentDate;
        private System.Windows.Forms.TextBox txNextExecutionDate;
        private System.Windows.Forms.Button btnGenerateNextExecutionDate;
        private System.Windows.Forms.Label lbCurrentDate;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbRecurringTypes;
        private System.Windows.Forms.NumericUpDown nudFrequency;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox gbLimits;
        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.Label lbNextExecutionDate;
        private System.Windows.Forms.RichTextBox tbDescription;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.Label lbRecurringType;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label lbFrequencyUnit;
        private System.Windows.Forms.Label lbEndDate;
        private System.Windows.Forms.Label lbStartDate;
        private System.Windows.Forms.Label lbDateTime;
        private System.Windows.Forms.Label lbEvery;
    }
}

