
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
            this.dtpNextExecutionDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerateNextExecutionDate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbCurrentDate = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbRecurringTypes = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.nudFrequency = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpCurrentDate
            // 
            this.dtpCurrentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrentDate.Location = new System.Drawing.Point(118, 12);
            this.dtpCurrentDate.Name = "dtpCurrentDate";
            this.dtpCurrentDate.Size = new System.Drawing.Size(200, 23);
            this.dtpCurrentDate.TabIndex = 0;
            // 
            // dtpNextExecutionDate
            // 
            this.dtpNextExecutionDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpNextExecutionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNextExecutionDate.Location = new System.Drawing.Point(0, 38);
            this.dtpNextExecutionDate.Name = "dtpNextExecutionDate";
            this.dtpNextExecutionDate.Size = new System.Drawing.Size(200, 23);
            this.dtpNextExecutionDate.TabIndex = 1;
            // 
            // btnGenerateNextExecutionDate
            // 
            this.btnGenerateNextExecutionDate.Location = new System.Drawing.Point(324, 12);
            this.btnGenerateNextExecutionDate.Name = "btnGenerateNextExecutionDate";
            this.btnGenerateNextExecutionDate.Size = new System.Drawing.Size(326, 23);
            this.btnGenerateNextExecutionDate.TabIndex = 2;
            this.btnGenerateNextExecutionDate.Text = "Calculate next date";
            this.btnGenerateNextExecutionDate.UseVisualStyleBackColor = true;
            this.btnGenerateNextExecutionDate.Click += new System.EventHandler(this.btnGenerateNextExecutionDate_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbCurrentDate);
            this.panel1.Controls.Add(this.dtpCurrentDate);
            this.panel1.Controls.Add(this.btnGenerateNextExecutionDate);
            this.panel1.Location = new System.Drawing.Point(12, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 100);
            this.panel1.TabIndex = 3;
            // 
            // lbCurrentDate
            // 
            this.lbCurrentDate.AutoSize = true;
            this.lbCurrentDate.Location = new System.Drawing.Point(34, 19);
            this.lbCurrentDate.Name = "lbCurrentDate";
            this.lbCurrentDate.Size = new System.Drawing.Size(38, 15);
            this.lbCurrentDate.TabIndex = 3;
            this.lbCurrentDate.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpNextExecutionDate);
            this.panel2.Location = new System.Drawing.Point(12, 338);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(638, 100);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.nudFrequency);
            this.panel3.Controls.Add(this.cbRecurringTypes);
            this.panel3.Controls.Add(this.cbType);
            this.panel3.Location = new System.Drawing.Point(12, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(638, 100);
            this.panel3.TabIndex = 5;
            // 
            // cbRecurringTypes
            // 
            this.cbRecurringTypes.FormattingEnabled = true;
            this.cbRecurringTypes.Location = new System.Drawing.Point(10, 41);
            this.cbRecurringTypes.Name = "cbRecurringTypes";
            this.cbRecurringTypes.Size = new System.Drawing.Size(190, 23);
            this.cbRecurringTypes.TabIndex = 1;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(10, 11);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(190, 23);
            this.cbType.TabIndex = 0;
            // 
            // nudFrequency
            // 
            this.nudFrequency.Location = new System.Drawing.Point(324, 41);
            this.nudFrequency.Name = "nudFrequency";
            this.nudFrequency.Size = new System.Drawing.Size(120, 23);
            this.nudFrequency.TabIndex = 2;
            // 
            // ScheduleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ScheduleFrm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCurrentDate;
        private System.Windows.Forms.DateTimePicker dtpNextExecutionDate;
        private System.Windows.Forms.Button btnGenerateNextExecutionDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbCurrentDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbRecurringTypes;
        private System.Windows.Forms.NumericUpDown nudFrequency;
    }
}

