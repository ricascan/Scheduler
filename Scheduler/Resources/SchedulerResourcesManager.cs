using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Resources
{
    public static class SchedulerResourcesManager
    {
        private static Dictionary<string, Dictionary<string, string>> resources;

        private const string englishLanguage = "en";
        private const string spanishLanguage = "es";

        private static CultureInfo culture;

        public static void InitializeResourcesManager(string LanguageCode)
        {
            try
            {
                culture = CultureInfo.GetCultureInfo(LanguageCode);
            }
            catch (CultureNotFoundException)
            {
                culture = CultureInfo.GetCultureInfo("en-GB");
                throw new ScheduleException(GetResource("InvalidCulture"));
            }
            if (resources == null)
            {
                InitializeDictionary();
            }
        }

        private static void InitializeDictionary()
        {
            resources = new Dictionary<string, Dictionary<string, string>>()
            {
                { "CurrentDateOutLimits", new Dictionary<string, string>()
                {
                    { englishLanguage, "Current date is out of the limits set" },
                    { spanishLanguage, "La fecha actual está fuera de los limites establecidos" }
                }
                },
                { "DateTimeCanNotBeLessThanCurrentDate", new Dictionary<string, string>()
                {
                    { englishLanguage, "Proposed date can not be less than current date" },
                    { spanishLanguage, "La fecha prpuesta no puede ser menor que la fecha actual" }
                }
                },
                { "DaysOfWeekCanNotBeRepeated", new Dictionary<string, string>()
                {
                    { englishLanguage, "Days of week can not be repeated" },
                    { spanishLanguage, "Los días de la semana no se pueden repetir" }
                }
                },
                { "EndTimeCanNotBeLessStartTime", new Dictionary<string, string>()
                {
                    { englishLanguage, "End time can not be less than start time" },
                    { spanishLanguage, "La fecha fin no puede ser menor que la fecha inicio" }
                }
                },
                { "FrequencyMustBeGraterThanZero", new Dictionary<string, string>()
                {
                    { englishLanguage, "Frequency must be greater than 0" },
                    { spanishLanguage, "La frecuencia tiene que ser mayor que cero" }
                }
                },
                { "GeneratedDateTimeNotRepresentable", new Dictionary<string, string>()
                {
                    { englishLanguage, "It is not possible to generate a representable execution date" },
                    { spanishLanguage, "No es posible generar un fecha de ejecución representable" }
                }
                },
                { "MustSetAtLeastOneDayWeek", new Dictionary<string, string>()
                {
                    { englishLanguage, "You must set at least one day of week when recurring type is weekly" },
                    { spanishLanguage, "Se debe establecer al menos un día de la semana cuando tipo de rcurrencia está establecido a semanal" }
                }
                },
                { "MustSetHourlyFrequencyWhenStartEndTimes", new Dictionary<string, string>()
                {
                    { englishLanguage, "You must set a hourly frequency when start and end times are set" },
                    { spanishLanguage, "Se debe establecer una frecuencia horaria cuando la hora inicio y la hora fin están establecidas" }
                }
                },
                { "MustSetStartEndTimesWhenFrequency", new Dictionary<string, string>()
                {
                    { englishLanguage, "You must set the start and end times when hourly frequency has a value" },
                    { spanishLanguage, "Se debe establecer una hora inicio y una hora fin cuando la frecuencia horaria está establecida" }
                }
                },
                { "MustSetStartTimeWhenEndTimeIsSet", new Dictionary<string, string>()
                {
                    { englishLanguage, "You must set the start time when end time is set and vice versa" },
                    { spanishLanguage, "Se debe establecer una hora inicio cuando se ha establecido una fecha fin y vice versa" }
                }
                },
                { "NotPossibleToGenerateExecDate", new Dictionary<string, string>()
                {
                    { englishLanguage, "It is not possible to generate a execution date within the current date and limits set" },
                    { spanishLanguage, "No es posible generar una fecha de ejecución dentro de los límites establecidos" }
                }
                },
                { "RecurringTypes_Invalid", new Dictionary<string, string>()
                {
                    { englishLanguage, "The recurring type is invalid" },
                    { spanishLanguage, "El tipo de recurrencia no es válido" }
                }
                },
                { "StartDateGreaterThanEndDate", new Dictionary<string, string>()
                {
                    { englishLanguage, "End date must be grater than start date" },
                    { spanishLanguage, "La fecha fin tiene que ser mayor que la fecha inicio" }
                }
                },
                { "TimeFrequencyConfigurationIsNotValid", new Dictionary<string, string>()
                {
                    { englishLanguage, "Time frequency configuration is not valid" },
                    { spanishLanguage, "La configuracion horaria no es válida" }
                }
                },
                { "MustSetMonthlyConfiguration", new Dictionary<string, string>()
                {
                    { englishLanguage, "You must set a monthly configuration when recurring type is set to monthly" },
                    { spanishLanguage, "Se debe establecer una configuración mensual cuando el tipo de recurrencia está establecido a mensualmente" }
                }
                },
                { "InvalidDayOfMounth", new Dictionary<string, string>()
                {
                    { englishLanguage, "The day of mounth must be between 1 and 31" },
                    { spanishLanguage, "El día del mes debe estar entre 1 y 31" }
                }
                },
                { "InvalidCulture", new Dictionary<string, string>()
                {
                    { englishLanguage, "The culture is not valid" },
                    { spanishLanguage, "La cultura no es válida" }
                }
                },
                { "RecurringTypesUnits_2", new Dictionary<string, string>()
                {
                    { englishLanguage, "Day(s)" },
                    { spanishLanguage, "Día(s)" }
                }
                },
                { "RecurringTypesUnits_3", new Dictionary<string, string>()
                {
                    { englishLanguage, "Week(s)" },
                    { spanishLanguage, "Semana(s)" }
                }
                },
                { "RecurringTypesUnits_4", new Dictionary<string, string>()
                {
                    { englishLanguage, "Mounth(s)" },
                    { spanishLanguage, "Mes(es)" }
                }
                },
                { "RecurringTypes_2", new Dictionary<string, string>()
                {
                    { englishLanguage, "Daily" },
                    { spanishLanguage, "Diariamente" }
                }
                },
                { "RecurringTypes_3", new Dictionary<string, string>()
                {
                    { englishLanguage, "Weekly" },
                    { spanishLanguage, "Semanalmente" }
                }
                },
                { "RecurringTypes_4", new Dictionary<string, string>()
                {
                    { englishLanguage, "Mounthly" },
                    { spanishLanguage, "Mensualmente" }
                }
                },
                { "SecondOrderConfig_7", new Dictionary<string, string>()
                {
                    { englishLanguage, "Day" },
                    { spanishLanguage, "Día" }
                }
                },
                { "SecondOrderConfig_8", new Dictionary<string, string>()
                {
                    { englishLanguage, "Weekday" },
                    { spanishLanguage, "Día entre semana" }
                }
                },
                { "SecondOrderConfig_9", new Dictionary<string, string>()
                {
                    { englishLanguage, "WeekendDay" },
                    { spanishLanguage, "Día del fin de semana" }
                }
                },
                { "FirstOrderConfig_1", new Dictionary<string, string>()
                {
                    { englishLanguage, "First" },
                    { spanishLanguage, "Primer" }
                }
                },
                { "FirstOrderConfig_2", new Dictionary<string, string>()
                {
                    { englishLanguage, "Second" },
                    { spanishLanguage, "Segundo" }
                }
                },
                { "FirstOrderConfig_3", new Dictionary<string, string>()
                {
                    { englishLanguage, "Third" },
                    { spanishLanguage, "Tercer" }
                }
                },
                { "FirstOrderConfig_4", new Dictionary<string, string>()
                {
                    { englishLanguage, "Fourth" },
                    { spanishLanguage, "Cuarto" }
                }
                },
                { "FirstOrderConfig_31", new Dictionary<string, string>()
                {
                    { englishLanguage, "Last" },
                    { spanishLanguage, "Último" }
                }
                },
                { "ScheduleDescription", new Dictionary<string, string>()
                {
                    { englishLanguage, "schedule will be used on {0} starting on {1}." },
                    { spanishLanguage, "la próxima fecha de ejecución será {0}, empezando el {1}." }
                }
                },
                { "ScheduleDescriptionHourly", new Dictionary<string, string>()
                {
                    { englishLanguage, "every {0} hours between {1} and {2}, " },
                    { spanishLanguage, "cada {0} horas, entre las {1} y las {2}, " }
                }
                },
                { "ScheduleDescriptionOccursOnce", new Dictionary<string, string>()
                {
                    { englishLanguage, "Occurs once, " },
                    { spanishLanguage, "Ocurre una vez, " }
                }
                },
                { "ScheduleDescriptionOccursRecurring", new Dictionary<string, string>()
                {
                    { englishLanguage, "Occurs every {0} {1}, " },
                    { spanishLanguage, "Ocurre cada {0} {1}, " }
                }
                },
                { "OnFollowingDays", new Dictionary<string, string>()
                {
                    { englishLanguage, "on the following days: {0}, " },
                    { spanishLanguage, "en los siguientes días: {0}, " }
                }
                },
                { "OnDay", new Dictionary<string, string>()
                {
                    { englishLanguage, "on day {0}, " },
                    { spanishLanguage, "el día {0}, " }
                }
                },
                { "OnThe", new Dictionary<string, string>()
                {
                    { englishLanguage, "on the {0} {1}, " },
                    { spanishLanguage, "el {0} {1}, " }
                }
                }
            };
        }

        public static string GetResource(string ResourceCode) => resources[ResourceCode][culture.Name.Split('-')[0]];

        public static string GetRecurringTypeUnitDescription(RecurringTypes Type)
            => resources[("RecurringTypesUnits_" + (int)Type)][culture.Name.Split('-')[0]];

        public static string FormatDateTime(DateTime DateTime)
        {
            return DateTime.ToString("G", culture);
        }

        public static string FormatTime(DateTime Time)
        {
            return Time.ToString("T", culture);
        }

        public static string GetSecondOrderConfigurationResource(MonthlySecondOrderConfiguration SecondOrdenConfiguration)
        {
            if (((int)SecondOrdenConfiguration) <= 6)
            {
                return culture.DateTimeFormat.GetDayName((DayOfWeek)SecondOrdenConfiguration);
            }
            return resources[("SecondOrderConfig_" + (int)SecondOrdenConfiguration)][culture.Name.Split('-')[0]];
        }

        public static string GetFirstOrderConfigurationResource(MonthlyFirstOrderConfiguration FirstOrdenConfiguration) 
            => resources[("FirstOrderConfig_" + (int)FirstOrdenConfiguration)][culture.Name.Split('-')[0]];

        public static string[] GetDaysOfWeekResources(DayOfWeek[] Days)
        {
            List<string> DaysTranslated = new();
            foreach(DayOfWeek Day in Days)
            {
                DaysTranslated.Add(GetSecondOrderConfigurationResource((MonthlySecondOrderConfiguration)(int)Day));
            }
            return DaysTranslated.ToArray();
        }
    }
}
