using Scheduler.Extenders;
using Scheduler.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Scheduler.Tests
{
    public class ScheduleTest
    {
        [Fact]
        public void Generated_execution_date_is_correct_once_en_GB()
        {
            Scheduler TestConfiguration = new()
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
            string ExpectedDescription = "Occurs once, schedule will be used on 01/01/2020 15:45:00 starting on 01/01/2020 00:00:00.";
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_once_es_ES()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2020, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2020, 1, 1),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2020, 1, 1, 15, 45, 0);
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            string ExpectedDescription = "Ocurre una vez, la próxima fecha de ejecución será 1/1/2020 15:45:00, empezando el 1/1/2020 0:00:00.";
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_once_en_US()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2020, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2020, 1, 1),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2020, 1, 1, 15, 45, 0);
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            string ExpectedDescription = "Occurs once, schedule will be used on 1/1/2020 3:45:00 PM starting on 1/1/2020 12:00:00 AM.";
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void DateTime_less_than_current_date_should_throw_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2019, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2019, 1, 1)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("DateTimeCanNotBeLessThanCurrentDate"), Exception.Message);
        }

        [Fact]
        public void DateTime_less_than_current_date_should_throw_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                DateTime = new DateTime(2019, 1, 1, 15, 45, 0),
                StartDate = new DateTime(2019, 1, 1),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("DateTimeCanNotBeLessThanCurrentDate"), Exception.Message);
        }

        [Fact]
        public void EndTime_less_than_StartTime_should_throw_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                EndDate = new DateTime(2018, 1, 1),
                StartDate = new DateTime(2019, 1, 1)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("StartDateGreaterThanEndDate"), Exception.Message);
        }

        [Fact]
        public void EndTime_less_than_StartTime_should_throw_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2020, 1, 1),
                ScheduleType = ScheduleTypes.Once,
                EndDate = new DateTime(2018, 1, 1),
                StartDate = new DateTime(2019, 1, 1),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("StartDateGreaterThanEndDate"), Exception.Message);
        }

        [Fact]
        public void Negative_Frequency_should_throw_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = -5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("FrequencyMustBeGraterThanZero"), Exception.Message);
        }

        [Fact]
        public void Negative_Frequency_should_throw_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = DateTime.Now,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = -5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("FrequencyMustBeGraterThanZero"), Exception.Message);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1,1,1,4, 0, 0),
                EndTime = new DateTime(1, 1, 1,8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime ExpectedDateTime = new(2021, 5, 15, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 15/05/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime ExpectedDateTime = new(2021, 5, 15, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Día(s), cada 2 horas, entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 15/5/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime ExpectedDateTime = new(2021, 5, 15, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 4:00:00 AM and 8:00:00 AM, schedule will be used on 5/15/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 15, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), schedule will be used on 15/05/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 5, 15, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Día(s), la próxima fecha de ejecución será 15/5/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 5, 15, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), schedule will be used on 5/15/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_daily_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 10, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 10/05/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_daily_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 5, 10, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Día(s), cada 2 horas, entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 10/5/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_daily_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 10, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 5, 10, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Day(s), every 2 hours between 4:00:00 AM and 8:00:00 AM, schedule will be used on 5/10/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_en_GB()
        {
            DateTime CurrentTime = new(2021, 10, 5, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 10, 8, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 04:00:00 and 08:00:00, schedule will be used on 08/10/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_es_ES()
        {
            DateTime CurrentTime = new(2021, 10, 5, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 10, 8, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: lunes, viernes, cada 2 horas, " +
                "entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 8/10/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_en_US()
        {
            DateTime CurrentTime = new(2021, 10, 5, 3, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 10, 8, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 4:00:00 AM and 8:00:00 AM, schedule will be used on 10/8/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
               "between 04:00:00 and 08:00:00, schedule will be used on 14/05/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 5, 14, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: lunes, viernes, cada 2 horas, " +
               "entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 14/5/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 10, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 5, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
               "between 4:00:00 AM and 8:00:00 AM, schedule will be used on 5/14/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_different_week_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 14, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 04:00:00 and 08:00:00, schedule will be used on 14/06/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_different_week_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 14, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 14, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: lunes, viernes, cada 2 horas, " +
                "entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 14/6/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_different_week_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 14, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Friday, DayOfWeek.Monday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 14, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Monday, Friday, every 2 hours " +
                "between 4:00:00 AM and 8:00:00 AM, schedule will be used on 6/14/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_all_weekdays_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 01/06/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_all_weekdays_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: domingo, lunes, martes, miércoles, jueves, viernes, sábado, " +
                "cada 2 horas, entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 1/6/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_hourly_same_week_all_weekdays_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 8, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 4:00:00 AM and 8:00:00 AM, schedule will be used on 6/1/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_same_week_all_weekdays_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 6, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 04:00:00 and 08:00:00, schedule will be used on 31/05/2021 06:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_same_week_all_weekdays_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 6, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: domingo, lunes, martes, miércoles, jueves, viernes, sábado, " +
                "cada 2 horas, entre las 4:00:00 y las 8:00:00, la próxima fecha de ejecución será 31/5/2021 6:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_hourly_weekly_same_week_all_weekdays_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 6, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "every 2 hours between 4:00:00 AM and 8:00:00 AM, schedule will be used on 5/31/2021 6:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_same_week_all_weekdays_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
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
                "schedule will be used on 01/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_same_week_all_weekdays_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: domingo, lunes, martes, miércoles, jueves, viernes, sábado, " +
                "la próxima fecha de ejecución será 1/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_same_week_all_weekdays_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, " +
                "schedule will be used on 6/1/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_different_week_one_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
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
                "schedule will be used on 04/07/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_different_week_one_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[1]
                    { DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 7, 4, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Semana(s), en los siguientes días: domingo, " +
                "la próxima fecha de ejecución será 4/7/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_different_week_one_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[1]
                    { DayOfWeek.Sunday },
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 7, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Week(s), on the following days: Sunday, " +
                "schedule will be used on 7/4/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_hourly_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 2, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Monday, " +
                "every 2 hours between 02:00:00 and 16:00:00, " +
                "schedule will be used on 31/05/2021 04:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_hourly_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 2, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 4, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Primer lunes, " +
                "cada 2 horas, entre las 2:00:00 y las 16:00:00, " +
                "la próxima fecha de ejecución será 31/5/2021 4:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_hourly_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 2, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 5, 31, 4, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Monday, " +
                "every 2 hours between 2:00:00 AM and 4:00:00 PM, " +
                "schedule will be used on 5/31/2021 4:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 10, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Monday, " +
                "schedule will be used on 04/10/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 10, 4, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Primer lunes, " +
                "la próxima fecha de ejecución será 4/10/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_monday_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 10, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Monday, " +
                "schedule will be used on 10/4/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_monday_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 10, 11, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Monday, " +
                "schedule will be used on 11/10/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_monday_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 10, 11, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Segundo lunes, " +
                "la próxima fecha de ejecución será 11/10/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_monday_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 10, 11, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Monday, " +
                "schedule will be used on 10/11/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_monday_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 10, 18, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Monday, " +
                "schedule will be used on 18/10/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_monday_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 10, 18, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Tercer lunes, " +
                "la próxima fecha de ejecución será 18/10/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_monday_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 10, 18, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Monday, " +
                "schedule will be used on 10/18/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_monday_en_GB()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 10, 25, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Monday, " +
                "schedule will be used on 25/10/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_monday_es_ES()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 10, 25, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Cuarto lunes, " +
                "la próxima fecha de ejecución será 25/10/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_monday_en_US()
        {
            DateTime CurrentTime = new(2021, 5, 31, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 10, 25, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Monday, " +
                "schedule will be used on 10/25/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_monday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 11, 29, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Monday, " +
                "schedule will be used on 29/11/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_monday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 11, 29, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Último lunes, " +
                "la próxima fecha de ejecución será 29/11/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_monday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Monday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 11, 29, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Monday, " +
                "schedule will be used on 11/29/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Weekday, " +
                "schedule will be used on 01/11/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Primer Día entre semana, " +
                "la próxima fecha de ejecución será 1/11/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Weekday, " +
                "schedule will be used on 11/1/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Weekday, " +
                "schedule will be used on 02/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Segundo Día entre semana, " +
                "la próxima fecha de ejecución será 2/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Weekday, " +
                "schedule will be used on 6/2/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Weekday, " +
                "schedule will be used on 03/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Tercer Día entre semana, " +
                "la próxima fecha de ejecución será 3/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Weekday, " +
                "schedule will be used on 6/3/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Weekday, " +
                "schedule will be used on 04/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Cuarto Día entre semana, " +
                "la próxima fecha de ejecución será 4/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Weekday, " +
                "schedule will be used on 6/4/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekday_en_GB()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 7, 30, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Weekday, " +
                "schedule will be used on 30/07/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekday_es_ES()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 7, 30, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Último Día entre semana, " +
                "la próxima fecha de ejecución será 30/7/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekday_en_US()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Weekday,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 7, 30, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Weekday, " +
                "schedule will be used on 7/30/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekendday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 11, 6, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First WeekendDay, " +
                "schedule will be used on 06/11/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekendday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 11, 6, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Primer Día del fin de semana, " +
                "la próxima fecha de ejecución será 6/11/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_weekendday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 11, 6, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First WeekendDay, " +
                "schedule will be used on 11/6/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekendday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 6, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second WeekendDay, " +
                "schedule will be used on 06/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekendday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 6, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Segundo Día del fin de semana, " +
                "la próxima fecha de ejecución será 6/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_weekendday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 6, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second WeekendDay, " +
                "schedule will be used on 6/6/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekendday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 12, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third WeekendDay, " +
                "schedule will be used on 12/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekendday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 12, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Tercer Día del fin de semana, " +
                "la próxima fecha de ejecución será 12/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_weekendday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 12, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third WeekendDay, " +
                "schedule will be used on 6/12/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekendday_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 13, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth WeekendDay, " +
                "schedule will be used on 13/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekendday_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 13, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Cuarto Día del fin de semana, " +
                "la próxima fecha de ejecución será 13/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_weekendday_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 13, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth WeekendDay, " +
                "schedule will be used on 6/13/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekendday_en_GB()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last WeekendDay, " +
                "schedule will be used on 31/07/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekendday_es_ES()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Último Día del fin de semana, " +
                "la próxima fecha de ejecución será 31/7/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_weekendday_en_US()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.WeekendDay,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last WeekendDay, " +
                "schedule will be used on 7/31/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_day_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Day, " +
                "schedule will be used on 01/11/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_day_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Primer Día, " +
                "la próxima fecha de ejecución será 1/11/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_first_day_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 30, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 11, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the First Day, " +
                "schedule will be used on 11/1/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_day_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Day, " +
                "schedule will be used on 02/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_day_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Segundo Día, " +
                "la próxima fecha de ejecución será 2/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_second_day_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Second,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 2, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Second Day, " +
                "schedule will be used on 6/2/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_day_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Day, " +
                "schedule will be used on 03/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_day_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Tercer Día, " +
                "la próxima fecha de ejecución será 3/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_third_day_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Third,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 3, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Third Day, " +
                "schedule will be used on 6/3/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_day_en_GB()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Day, " +
                "schedule will be used on 04/06/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_day_es_ES()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Cuarto Día, " +
                "la próxima fecha de ejecución será 4/6/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_fourth_day_en_US()
        {
            DateTime CurrentTime = new(2021, 6, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Fourth,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 6, 4, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Fourth Day, " +
                "schedule will be used on 6/4/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_day_en_GB()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Day, " +
                "schedule will be used on 31/07/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_day_es_ES()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el Último Día, " +
                "la próxima fecha de ejecución será 31/7/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_last_day_en_US()
        {
            DateTime CurrentTime = new(2021, 7, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.Last,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 7, 31, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on the Last Day, " +
                "schedule will be used on 7/31/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }
        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_31_en_GB()
        {
            DateTime CurrentTime = new(2021, 9, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 31,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2021, 9, 30, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on day 31, " +
                "schedule will be used on 30/09/2021 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_31_es_ES()
        {
            DateTime CurrentTime = new(2021, 9, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 31,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2021, 9, 30, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el día 31, " +
                "la próxima fecha de ejecución será 30/9/2021 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_31_en_US()
        {
            DateTime CurrentTime = new(2021, 9, 1, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 31,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2021, 9, 30, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on day 31, " +
                "schedule will be used on 9/30/2021 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_1_en_GB()
        {
            DateTime CurrentTime = new(2021, 9, 2, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 5,                
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0)
            };
            DateTime ExpectedDateTime = new(2022, 2, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on day 1, " +
                "schedule will be used on 01/02/2022 00:00:00 starting on 05/05/2021 01:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_1_es_ES()
        {
            DateTime CurrentTime = new(2021, 9, 2, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };
            DateTime ExpectedDateTime = new(2022, 2, 1, 0, 0, 0);
            string ExpectedDescription = "Ocurre cada 5 Mes(es), el día 1, " +
                "la próxima fecha de ejecución será 1/2/2022 0:00:00, empezando el 5/5/2021 1:00:00.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_day_1_en_US()
        {
            DateTime CurrentTime = new(2021, 9, 2, 4, 0, 0);
            Scheduler TestConfiguration = new()
            {
                CurrentDate = CurrentTime,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 5,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };
            DateTime ExpectedDateTime = new(2022, 2, 1, 0, 0, 0);
            string ExpectedDescription = "Occurs every 5 Mounth(s), on day 1, " +
                "schedule will be used on 2/1/2022 12:00:00 AM starting on 5/5/2021 1:00:00 AM.";
            ScheduleOutputData OutputData = TestConfiguration.GetNextExecutionDate();
            DateTime? GeneratedExecutionDate = OutputData.OutputDateTime;
            Assert.NotNull(GeneratedExecutionDate);
            AssertEqualDates(ExpectedDateTime, GeneratedExecutionDate.Value);
            Assert.Equal(ExpectedDescription, OutputData.OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_6_series()
        { 
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-GB"
            };
            
            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Mounth(s), on day 1, every 4 hours between 02:00:00 and 16:00:00, " +
                "schedule will be used on {0} starting on 05/05/2021 01:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021,10, 1, 2, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 6, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 10, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 14, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 2, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 6, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_6_series_en_US()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Mounth(s), on day 1, every 4 hours between 2:00:00 AM and 4:00:00 PM, " +
                "schedule will be used on {0} starting on 5/5/2021 1:00:00 AM.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 2, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 6, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 10, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 14, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 2, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 6, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_monthly_6_series_es_ES()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 1,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Ocurre cada 1 Mes(es), el día 1, cada 4 horas, entre las 2:00:00 y las 16:00:00, " +
                "la próxima fecha de ejecución será {0}, empezando el 5/5/2021 1:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 2, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 6, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 10, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 10, 1, 14, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 2, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 11, 1, 6, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_6_series_en_GB()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-GB"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, every 4 hours between 02:00:00 and 16:00:00, " +
                "schedule will be used on {0} starting on 05/05/2021 01:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_6_series_es_ES()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Ocurre cada 1 Semana(s), en los siguientes días: domingo, lunes, martes, miércoles, jueves, viernes, sábado, cada 4 horas, " +
                "entre las 2:00:00 y las 16:00:00, " +
                "la próxima fecha de ejecución será {0}, empezando el 5/5/2021 1:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", 
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_weekly_6_series_en_US()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[7]
                    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday,
                            DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Week(s), on the following days: Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, every 4 hours " +
                "between 2:00:00 AM and 4:00:00 PM, " +
                "schedule will be used on {0} starting on 5/5/2021 1:00:00 AM.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G",
                CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_6_series_en_GB()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-GB"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Day(s), every 4 hours between 02:00:00 and 16:00:00, " +
                "schedule will be used on {0} starting on 05/05/2021 01:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Not_supported_culture_returns_english_resource()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "fr-FR"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Day(s), every 4 hours between 02:00:00 and 16:00:00, " +
                "schedule will be used on {0} starting on 05/05/2021 01:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_6_series_en_GB()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-GB"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Day(s), " +
                "schedule will be used on {0} starting on 05/05/2021 01:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 0, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 4, 0, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 5, 0, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 6, 0, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 7, 0, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 8, 0, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_6_series_en_US()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Day(s), " +
                "schedule will be used on {0} starting on 5/5/2021 1:00:00 AM.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 0, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 4, 0, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 5, 0, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 6, 0, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 7, 0, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 8, 0, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_6_series_es_ES()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Ocurre cada 1 Día(s), " +
                "la próxima fecha de ejecución será {0}, empezando el 5/5/2021 1:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 0, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 4, 0, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 5, 0, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 6, 0, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 7, 0, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 8, 0, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_6_series_en_US()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "en-US"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Occurs every 1 Day(s), every 4 hours between 2:00:00 AM and 4:00:00 PM, " +
                "schedule will be used on {0} starting on 5/5/2021 1:00:00 AM.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
        }

        [Fact]
        public void Generated_execution_date_is_correct_daily_hourly_6_series_es_ES()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 9, 2, 4, 0, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 1,
                StartTime = new DateTime(1, 1, 1, 2, 0, 0),
                EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                HourlyFrequency = 4,
                StartDate = new DateTime(2021, 5, 5, 1, 0, 0),
                Language = "es-ES"
            };

            ScheduleOutputData[] Outputs = TestConfiguration.GetNextExecutionDateSeries(6);
            string ExpectedDescriptionBase = "Ocurre cada 1 Día(s), cada 4 horas, " +
                "entre las 2:00:00 y las 16:00:00, " +
                "la próxima fecha de ejecución será {0}, empezando el 5/5/2021 1:00:00.";
            string ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[0].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 6, 0, 0), Outputs[0].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[0].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[1].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 10, 0, 0), Outputs[1].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[1].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[2].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 2, 14, 0, 0), Outputs[2].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[2].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[3].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 2, 0, 0), Outputs[3].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[3].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[4].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 6, 0, 0), Outputs[4].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[4].OutputDescription);
            ExpectedDescription = string.Format(ExpectedDescriptionBase, Outputs[5].OutputDateTime.Value.ToString("G", CultureInfo.GetCultureInfo(TestConfiguration.Language)));
            AssertEqualDates(new DateTime(2021, 9, 3, 10, 0, 0), Outputs[5].OutputDateTime.Value);
            Assert.Equal(ExpectedDescription, Outputs[5].OutputDescription);
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
        public void Current_date_less_than_start_date_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 1, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2022, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("CurrentDateOutLimits"), Exception.Message);
        }

        [Fact]
        public void Current_date_less_than_start_date_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 1, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2022, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("CurrentDateOutLimits"), Exception.Message);
        }

        [Fact]
        public void Current_date_grater_than_end_date_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
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
            Assert.Equal(SchedulerResourcesManager.GetResource("CurrentDateOutLimits"), Exception.Message);
        }
        [Fact]
        public void Current_date_grater_than_end_date_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 3, 1, 15, 30, 0),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 15, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("CurrentDateOutLimits"), Exception.Message);
        }
        [Fact]
        public void Generated_date_grater_than_end_date_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
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
            Assert.Equal(SchedulerResourcesManager.GetResource("NotPossibleToGenerateExecDate"), Exception.Message);
        }

        [Fact]
        public void Generated_date_grater_than_end_date_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("NotPossibleToGenerateExecDate"), Exception.Message);
        }

        [Fact]
        public void DayOfWeek_not_set_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
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
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetAtLeastOneDayWeek"), Exception.Message);
        }

        [Fact]
        public void DayOfWeek_not_set_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetAtLeastOneDayWeek"), Exception.Message);
        }

        [Fact]
        public void DayOfWeek_repeated_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
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
            Assert.Equal(SchedulerResourcesManager.GetResource("DaysOfWeekCanNotBeRepeated"), Exception.Message);
        }

        [Fact]
        public void DayOfWeek_repeated_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Weekly,
                DaysOfWeek = new DayOfWeek[2] { DayOfWeek.Monday, DayOfWeek.Monday },
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("DaysOfWeekCanNotBeRepeated"), Exception.Message);
        }

        [Fact]
        public void Start_End_Times_not_set_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
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
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetStartEndTimesWhenFrequency"), Exception.Message);
        }
        [Fact]
        public void Start_End_Times_not_set_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetStartEndTimesWhenFrequency"), Exception.Message);
        }
        [Fact]
        public void End_time_less_than_start_time_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 3, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("EndTimeCanNotBeLessStartTime"), Exception.Message);
        }

        [Fact]
        public void End_time_less_than_start_time_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 2,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 3, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("EndTimeCanNotBeLessStartTime"), Exception.Message);
        }

        /// <summary>
        /// Difference beetween start and end times is less than hourly frequency
        /// </summary>
        [Fact]
        public void Invalid_time_frequency_configuration_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 5,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 5, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("TimeFrequencyConfigurationIsNotValid"), Exception.Message);
        }

        [Fact]
        public void Invalid_time_frequency_configuration_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                HourlyFrequency = 5,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 5, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("TimeFrequencyConfigurationIsNotValid"), Exception.Message);
        }

        [Fact]
        public void Hourly_frequency_not_set_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetHourlyFrequencyWhenStartEndTimes"), Exception.Message);
        }

        [Fact]
        public void Hourly_frequency_not_set_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new DateTime(2021, 2, 15),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartTime = new DateTime(1, 1, 1, 4, 0, 0),
                EndTime = new DateTime(1, 1, 1, 8, 0, 0),
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                EndDate = new DateTime(2021, 2, 16),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("MustSetHourlyFrequencyWhenStartEndTimes"), Exception.Message);
        }

        [Fact]
        public void Generated_date_greater_than_max_date_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = DateTime.MaxValue,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("GeneratedDateTimeNotRepresentable"), Exception.Message);
        }

        [Fact]
        public void Generated_date_greater_than_max_date_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = DateTime.MaxValue,
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Daily,
                Frequency = 5,
                StartDate = new DateTime(2021, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal(SchedulerResourcesManager.GetResource("GeneratedDateTimeNotRepresentable"), Exception.Message);
        }

        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("You must set a monthly configuration when recurring type is set to monthly", Exception.Message);
        }

        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("Se debe establecer una configuración mensual cuando el tipo de recurrencia está establecido a mensualmente", Exception.Message);
        }
        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception2_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("You must set a monthly configuration when recurring type is set to monthly", Exception.Message);
        }
        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception2_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlyFirstOrderConfiguration = MonthlyFirstOrderConfiguration.First,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("Se debe establecer una configuración mensual cuando el tipo de recurrencia está establecido a mensualmente", Exception.Message);
        }

        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception3_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("You must set a monthly configuration when recurring type is set to monthly", Exception.Message);
        }

        [Fact]
        public void Mounthly_Configuration_not_set_throws_exception3_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                MonthlySecondOrdenConfiguration = MonthlySecondOrderConfiguration.Day,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("Se debe establecer una configuración mensual cuando el tipo de recurrencia está establecido a mensualmente", Exception.Message);
        }

        [Fact]
        public void Mounthly_Configuration_invalid_day_en()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 0,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0)
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("The day of mounth must be between 1 and 31", Exception.Message);
        }
        [Fact]
        public void Mounthly_Configuration_invalid_day_es()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 0,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "es-ES"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("El día del mes debe estar entre 1 y 31", Exception.Message);
        }

        [Fact]
        public void Invalid_Culture_Throws_Exception()
        {
            Scheduler TestConfiguration = new()
            {
                CurrentDate = new(2021, 1, 1),
                ScheduleType = ScheduleTypes.Recurring,
                RecurringType = RecurringTypes.Monthly,
                DayOfMonth = 0,
                Frequency = 5,
                StartDate = new DateTime(2020, 1, 1, 15, 30, 0),
                Language = "X"
            };
            ScheduleException Exception =
                Assert.Throws<ScheduleException>(() => TestConfiguration.GetNextExecutionDate());
            Assert.Equal("The culture is not valid", Exception.Message);
        }
    }
}
