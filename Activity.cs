using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_organization
{
    public class Activity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int PlannedMinutesDuration { get; set; }
        public int ActualMinutesDuration { get; set; }
        public string Note { get; set; }

        public Activity()
        {
            StartTime = DateTime.Now;
        }

        public override string ToString()
        {
            if (Note != null && Note != "")
                return $"{Name} - {StartTime.ToString("HH:mm")}\n Czas trwania: {ActualMinutesDuration} min\n {Note}";
            else
                return $"{Name} - {StartTime.ToString("HH:mm")}\n Czas trwania: {ActualMinutesDuration} min";
        }

        /// <summary>
        /// Calculates how long activity takes place
        /// </summary>
        /// <returns>Amount of time activity takes place until now</returns>
        public int secondsInProgress()
        {
            return (int)(DateTime.Now - StartTime).TotalSeconds;
        }
    }
}
