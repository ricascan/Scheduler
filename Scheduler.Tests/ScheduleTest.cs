using System;
using Xunit;

namespace Scheduler.Tests
{
    public class ScheduleTest
    {
        [Fact]
        public void RecurringTest()
        {
            ScheduleConfiguration TestConfiguration = new ScheduleConfiguration()
            {
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Mounthly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new Schedule(TestConfiguration);
            TestSchedule.CurrentDate = DateTime.Now;
            DateTime ExpectedDateTime = DateTime.Now.AddMonths(5);
            DateTime? GeneratedExecutionDate = TestSchedule.GetNextExecutionDate().OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            Assert.Equal(ExpectedDateTime.Year, GeneratedExecutionDate.Value.Year);
            Assert.Equal(ExpectedDateTime.Month, GeneratedExecutionDate.Value.Month);
            Assert.Equal(ExpectedDateTime.Day, GeneratedExecutionDate.Value.Day);
            Assert.Equal(ExpectedDateTime.Hour, GeneratedExecutionDate.Value.Hour);
            Assert.Equal(ExpectedDateTime.Minute, GeneratedExecutionDate.Value.Minute);
            Assert.Equal(ExpectedDateTime.Second, GeneratedExecutionDate.Value.Second);
        }

        [Fact]
        public void RecurringTest2()
        {
            ScheduleConfiguration TestConfiguration = new ScheduleConfiguration()
            {
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2022, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new Schedule(TestConfiguration);
            TestSchedule.CurrentDate = DateTime.Now;
            Assert.Throws<ScheduleException>(() => TestSchedule.GetNextExecutionDate());            
        }
    }
}
