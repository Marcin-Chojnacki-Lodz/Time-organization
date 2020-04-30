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
        public int PauseSecondsDuration { get; set; }
        public string Note { get; set; }

        public Activity()
        {
            StartTime = DateTime.Now;
            PauseSecondsDuration = 0;
        }

        public override string ToString()
        {
            if (Note != null && Note != "")
                return $"{Name} - {StartTime.ToString("HH:mm")}\n Czas trwania: {ActualMinutesDuration} min / {PauseSecondsDuration/60} s pauzy\n {Note}";
            else
                return $"{Name} - {StartTime.ToString("HH:mm")}\n Czas trwania: {ActualMinutesDuration} min / {PauseSecondsDuration/60} s pauzy";
        }

        /// <summary>
        /// Calculates how long activity takes place
        /// </summary>
        /// <returns>Amount of time activity takes place until now</returns>
        public int secondsInProgress(int secondsOfPause)
        {
            PauseSecondsDuration += secondsOfPause;
            return (int)(DateTime.Now - StartTime).TotalSeconds - PauseSecondsDuration;
        }
    }
}
