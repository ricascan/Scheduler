using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler.Windows
{
    public partial class ScheduleFrm : Form
    {
        Schedule schedule;
        public ScheduleFrm()
        {
            this.InitializeComponent();
        }

        private void InitializeForm()
        {
            this.schedule = new Schedule();
            this.AssignDataBindings();
            this.AssignLabelTexts();
            this.SetControlsState();
        }

        private void AssignDataBindings()
        {
            this.dtpDateTime.DataBindings.Add(new Binding("Value", this.schedule, "Configuration.DateTime", false, DataSourceUpdateMode.OnPropertyChanged));
            this.dtpCurrentDate.DataBindings.Add(new Binding("Value", this.schedule, "Configuration.CurrentDate", false, DataSourceUpdateMode.OnPropertyChanged));           
            this.dtpStartDate.DataBindings.Add(new Binding("Value", this.schedule, "Configuration.StartDate", false, DataSourceUpdateMode.OnPropertyChanged));
            var EndDateBinding = new Binding("Value", this.schedule, "Configuration.EndDate", true, DataSourceUpdateMode.OnPropertyChanged);
            EndDateBinding.Parse += this.dtEndDateNullable_Parse;
            EndDateBinding.Format += this.dtEndDateNullable_Format;
            this.dtpEndDate.DataBindings.Add(EndDateBinding);
            this.dtpEndDate.Checked = false;
            this.cbType.DataSource = Enum.GetValues(typeof(ScheduleTypes));
            this.cbType.DataBindings.Add(new Binding("SelectedItem", this.schedule, "Configuration.ScheduleType", false, DataSourceUpdateMode.OnPropertyChanged));
            this.cbRecurringTypes.DataSource = Enum.GetValues(typeof(RecurringTypes));
            this.cbRecurringTypes.DataBindings.Add(new Binding("SelectedItem", this.schedule, "Configuration.RecurringType", false, DataSourceUpdateMode.OnPropertyChanged));
            this.nudFrequency.DataBindings.Add(new Binding("Value", this.schedule, "Configuration.Frequency", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void AssignLabelTexts()
        {
            this.lbCurrentDate.Text = Resources.Details.shl_CurrentDate;
            this.lbStartDate.Text = Resources.Details.shl_StartDate;
            this.lbEndDate.Text = Resources.Details.shl_EndDate;
            this.lbFrequencyUnit.Text = EnumerationsDescriptionManager.GetRecurringTypeUnitDescription(this.schedule.Configuration.RecurringType);
            this.lbRecurringType.Text = Resources.Details.shl_Occurs;
            this.lbType.Text = Resources.Details.shl_type;
            this.lbDateTime.Text = Resources.Details.shl_DateTime;
            this.lbNextExecutionDate.Text = Resources.Details.shl_NextExecutionDate;
        }

        private void btnGenerateNextExecutionDate_Click(object sender, EventArgs e)
        {
            ScheduleOutputData OutputData = null;
            try
            {
                OutputData = this.schedule.GetNextExecutionDate();
            }
            catch (ScheduleException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (OutputData != null && OutputData.OutputDateTime.HasValue)
            {
                this.txNextExecutionDate.Text = OutputData.OutputDateTime.Value.ToString("G");
                this.tbDescription.Text = OutputData.OutputDescription;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetControlsState();
        }

        private void SetControlsState()
        {
            ScheduleTypes SelectedType = (ScheduleTypes)this.cbType.SelectedItem;
            this.cbRecurringTypes.Enabled = this.nudFrequency.Enabled = SelectedType == ScheduleTypes.Recurring;
            this.dtpDateTime.Enabled = SelectedType == ScheduleTypes.Once;
        }

        private void cbRecurringTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Binding DataBinding in ((ComboBox)sender).DataBindings)
            {
                DataBinding.WriteValue();
            }
            this.lbFrequencyUnit.Text = EnumerationsDescriptionManager
                .GetRecurringTypeUnitDescription(this.schedule.Configuration.RecurringType);
        }

        private void ScheduleFrm_Load(object sender, EventArgs e)
        {
            this.InitializeForm();
        }

        void dtEndDateNullable_Format(object sender, ConvertEventArgs e)
        {
            Binding b = sender as Binding;
            if (b != null)
            {
                DateTimePicker dtp = (b.Control as DateTimePicker);
                if (dtp != null)
                {
                    if (e.Value == null)
                    {
                        dtp.ShowCheckBox = true;
                        dtp.Checked = false;
                        e.Value = dtp.Value;
                    }
                    else
                    {
                        dtp.ShowCheckBox = true;
                        dtp.Checked = true;
                    }
                }
            }
        }


        void dtEndDateNullable_Parse(object sender, ConvertEventArgs e)
        {
            Binding b = sender as Binding;
            if (b != null)
            {
                DateTimePicker dtp = (b.Control as DateTimePicker);
                if (dtp != null)
                {
                    if (dtp.Checked == false)
                    {
                        dtp.ShowCheckBox = true;
                        dtp.Checked = false;
                        e.Value = new DateTime?();
                    }
                    else
                    {
                        DateTime val = Convert.ToDateTime(e.Value);
                        e.Value = new DateTime?(val);
                    }
                }
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.dtpEndDate.MinDate = this.dtpCurrentDate.MinDate = dtpStartDate.Value;          
        }

        private void dtpCurrentDate_ValueChanged(object sender, EventArgs e)
        {
            this.dtpDateTime.MinDate = this.dtpCurrentDate.Value;
        }
    }
}
