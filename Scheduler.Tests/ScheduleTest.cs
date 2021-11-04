using System;
using System.Collections;
using System.Collections.Generic;
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
            DateTime ExpectedDateTime = new(2020, 1, 1, 15, 45, 0);
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            string ExpectedDescription = Resources.Global.ScheduleDescriptionOccursOnce
                         + string.Format(Resources.Global.ScheduleDescription, OutputData.OutputDateTime, TestConfiguration.StartDate);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void DateTime_less_than_current_date_should_throw_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2019, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2019, 1, 1)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.DateTimeCanNotBeLessThanCurrentDate, Exception.Message);
        }

        [Fact]
        public void EndTime_less_than_StartTime_should_throw_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                EndDate = new DateTime(2018, 1, 1),
                StartDate = new DateTime(2019, 1, 1)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.StartDateGreaterThanEndDate, Exception.Message);
        }

        [Fact]
        public void Negative_Frequency_should_throw_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = -5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.FrequencyMustBeGraterThanZero, Exception.Message);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime ExpectedDateTime = new(2021, 5, 15, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 15/05/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 15, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), schedule will be used on 15/05/2021 0:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_daily()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 10, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 10/05/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 10, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 04:00:00 and 08:00:00, schedule will be used on 10/05/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
               "between 04:00:00 and 08:00:00, schedule will be used on 14/05/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_different_week()
        {
            DateTime CurrentTime = new(2021, 5, 14, 8, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 04:00:00 and 08:00:00, schedule will be used on 14/06/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_all_weekdays()
        {
            DateTime CurrentTime = new(2021, 5, 31, 8, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 01/06/2021 4:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_same_week_all_weekdays()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 6, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 31/05/2021 6:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_same_week_all_weekdays()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "schedule will be used on 01/06/2021 0:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_different_week_one_weekday()
        {
            DateTime CurrentTime = new(2021, 5, 30, 4, 0, 0);
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[1]
                    { DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 7, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, " +
                "schedule will be used on 04/07/2021 0:00:00 starting on 05/05/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
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
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2022, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.CurrentDateOutLimits, Exception.Message);
        }

        [Fact]
        public void Current_date_grater_than_end_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 3, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 15, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.CurrentDateOutLimits, Exception.Message);
        }
        [Fact]
        public void Generated_date_grater_than_end_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.NotPossibleToGenerateExecDate, Exception.Message);
        }

        [Fact]
        public void DayOfWeek_not_set_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.MustSetAtLeastOneDayWeek, Exception.Message);
        }

        [Fact]
        public void DayOfWeek_repeated_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Monday, DayOfWeek.Monday },
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.DaysOfWeekCanNotBeRepeated, Exception.Message);
        }

        [Fact]
        public void Start_End_Times_not_set_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.MustSetStartEndTimesWhenFrequency, Exception.Message);
        }
        [Fact]
        public void End_time_less_than_start_time_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(3, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.EndTimeCanNotBeLessStartTime, Exception.Message);
        }

        /// <summary>
        /// Difference beetween start and end times is less than hourly frequency
        /// </summary>
        [Fact]
        public void Invalid_time_frequency_configuration_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 5,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(5, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.TimeFrequencyConfigurationIsNotValid, Exception.Message);
        }


        [Fact]
        public void Hourly_frequency_not_set_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.MustSetHourlyFrequencyWhenStartEndTimes, Exception.Message);
        }


        [Fact]
        public void Generated_date_greater_than_max_date_throws_exception()
        {
            ScheduleConfiguration TestConfiguration = new()
            {
                CurrentDate = DateTime.MaxValue,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(Resources.Global.GeneratedDateTimeNotRepresentable, Exception.Message);
        }
    }
}
