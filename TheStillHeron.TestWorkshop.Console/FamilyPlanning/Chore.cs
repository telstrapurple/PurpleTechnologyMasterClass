using System;

namespace TheStillHeron.TestWorkshop.Console.FamilyPlanning
{
    public enum ChoreCadence
    {
        Daily,
        Weekly,
        Monthly,
    }
    public class Chore
    {
        public string Name { get; set; }

        public ChoreCadence Cadence { get; set; }

        /// <summary>
        /// The date from which we measure to decide if the chore is due again.
        /// E.g. If the cadence is weekly, and the origin date is a Tuesday, the chore will be due on Tuesday
        /// </summary>
        public DateTime OriginDate { get; set; }

        public bool IsDue()
        {
            // ex.2
            switch (Cadence)
            {
                case ChoreCadence.Daily:
                    return true;
                case ChoreCadence.Weekly:
                    return OriginDate.DayOfWeek == DateTime.Today.DayOfWeek;
                case ChoreCadence.Monthly:
                    return OriginDate.Day == DateTime.Today.Day;
                default:
                    return false;
            }
        }
    }
}