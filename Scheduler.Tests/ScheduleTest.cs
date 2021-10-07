using System;
using Xunit;

namespace Scheduler.Tests
{
    public class ScheduleTest
    {
        [Fact]
        public void Generated_execution_date_is_correct_once()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2020, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2020, 1, 1)
            };
            Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = new(2020, 1, 1, 15, 45, 0);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
        }

        [Fact]
        public void DateTime_less_than_current_date_should_throw_exception()
        {            
            Assert.Throws<ScheduleException>(() =>
            {
                ScheduleConfiguration TestConfiguration = new()
                {
                    CurrentDate = new DateTime(2021, 1, 1),
                    DateTime = new DateTime(2020, 1, 1, 15, 45, 0)
                };
            }
            );
        }

        [Fact]
        public void Negative_Frequency_should_throw_exception()
        {
            Assert.Throws<ScheduleException>(() =>
            {
                ScheduleConfiguration TestConfiguration = new()
                {
                    Frequency = -5
                };
            }
            );
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Hourly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
        Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = DateTime.Now.AddHours(5);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
        }
        [Fact]
        public void Generated_execution_date_is_correct_daily()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = DateTime.Now.AddDays(5);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
        }
        [Fact]
        public void Generated_execution_date_is_correct_weekly()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = DateTime.Now.AddDays(5*7);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
        }
        [Fact]
        public void Generated_execution_date_is_correct_mounthly()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Mounthly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = DateTime.Now.AddMonths(5);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);           
        }
        [Fact]
        public void Generated_execution_date_is_correct_yearly()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            DateTime ExpectedDateTime = DateTime.Now.AddYears(5);
            ScheduleOutputData OutputData = TestSchedule.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
        }

        private static void AssertEqualDates(DateTime ExpectedDateTime, DateTime GeneratedExecutionDate)
        {
            Assert.Equal(ExpectedDateTime.Year, GeneratedExecutionDate.Year);
            Assert.Equal(ExpectedDateTime.Month, GeneratedExecutionDate.Month);
            Assert.Equal(ExpectedDateTime.Day, GeneratedExecutionDate.Day);
            Assert.Equal(ExpectedDateTime.Hour, GeneratedExecutionDate.Hour);
            Assert.Equal(ExpectedDateTime.Minute, GeneratedExecutionDate.Minute);
            Assert.Equal(ExpectedDateTime.Second, GeneratedExecutionDate.Second);
        }

        [Fact]
        public void Current_date_less_than_start_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 1, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2022, 1, 1, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            Assert.Throws<ScheduleException>(() => TestSchedule.GetNextExecutionDate());            
        }

        [Fact]
        public void Current_date_grater_than_end_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 3, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 15, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            Assert.Throws<ScheduleException>(() => TestSchedule.GetNextExecutionDate());
        }
        [Fact]
        public void Generated_date_grater_than_end_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 14, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 15, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            Assert.Throws<ScheduleException>(() => TestSchedule.GetNextExecutionDate());
        }

        [Fact]
        public void Generated_date_greater_than_max_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.MaxValue,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Yearly,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 15, 15, 30, 0)
            };
            Schedule TestSchedule = new(TestConfiguration);
            Assert.Throws<ScheduleException>(() => TestSchedule.GetNextExecutionDate());
        }
    }
}
