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
            this.schedule = new Schedule();
            this.dtpCurrentDate.DataBindings.Add(new Binding("Value", this.schedule, "CurrentDate", false, DataSourceUpdateMode.OnPropertyChanged));            
            this.cbType.DataSource = Enum.GetValues(typeof(ScheduleTypes));
            this.cbType.DataBindings.Add(new Binding("SelectedItem", this.schedule, "Configuration.ScheduleType", false, DataSourceUpdateMode.OnPropertyChanged));
            this.cbRecurringTypes.DataSource = Enum.GetValues(typeof(RecurringTypes));
            this.cbRecurringTypes.DataBindings.Add(new Binding("SelectedItem", this.schedule, "Configuration.RecurringType", false, DataSourceUpdateMode.OnPropertyChanged));
            this.nudFrequency.DataBindings.Add(new Binding("Value", this.schedule, "Configuration.Frequency", false, DataSourceUpdateMode.OnPropertyChanged));
            this.lbCurrentDate.Text = Resources.Details.shl_CurrentDate;
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
                this.dtpNextExecutionDate.Value = OutputData.OutputDateTime.Value;
            }
        }
    }
}
