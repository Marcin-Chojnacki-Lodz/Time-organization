using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Time_organization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Activity _activity;
        DispatcherTimer activityTimer = new DispatcherTimer();
        DispatcherTimer pauseTimer = new DispatcherTimer();
        private ObservableCollection<Activity> _history = new ObservableCollection<Activity>();
        private bool pause = false;
        private int pauseTime;

        /*
         * Constructors
         */

        public MainWindow()
        {
            InitializeComponent();

            activityName_label.Content = "";
            durationTime_label.Content = "";
            progress_progressBar.Value = 0;
            pauseContinue_button.Visibility = Visibility.Hidden;

            pauseTime = 0;

            DataContext = _history;
        }

        public MainWindow(Activity activity, ObservableCollection<Activity> history)
        {
            InitializeComponent();

            pauseContinue_button.Visibility = Visibility.Visible;

            _activity = activity;
            _history = history;
            activityName_label.Content = _activity.Name;
            progress_progressBar.Maximum = _activity.PlannedMinutesDuration * 60;

            pauseTime = 0;

            DataContext = _history;

            updateTimes();
        }

        /*
         * Window methods
         */

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            if (_activity != null)
            {
                activityTimer.Tick += new EventHandler(activityTimer_Tick);
                activityTimer.Interval = new TimeSpan(0, 0, 1);
                activityTimer.Start();

                pauseTimer.Tick += new EventHandler(pauseTimer_Tick);
                pauseTimer.Interval = new TimeSpan(0, 0, 1);
            }
        }

        /*
         * Button click methods
         */

        private void pauseContinue_button_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                pauseTimer.Stop();

                //activityTimer.Tick += new EventHandler(activityTimer_Tick);
                //activityTimer.Interval = new TimeSpan(0, 0, 1);
                activityTimer.Start();

                pauseContinue_button.Content = "|| Pauza";
                newActivity_button.IsEnabled = true;
                pause = false;
            }
            else
            {
                activityTimer.Stop();

                //pauseTimer.Tick += new EventHandler(pauseTimer_Tick);
                //pauseTimer.Interval = new TimeSpan(0, 0, 1);
                pauseTimer.Start();

                pauseContinue_button.Content = "> Kontynuuj";
                newActivity_button.IsEnabled = false;
                pause = true;
            }
            
        }

        private void newActivity_button_Click(object sender, RoutedEventArgs e)
        {
            if (_activity != null)
            {
                _activity.ActualMinutesDuration = _activity.secondsInProgress(pauseTime) / 60;
                _history.Add(_activity);
            }

            NewActivityWindow newActivityWindow = new NewActivityWindow(_history);

            activityTimer.Stop();
            this.Close();
            newActivityWindow.Show();
        }

        /*
         * Additional methods
         */

        private void activityTimer_Tick(object sender, EventArgs e)
        {
            updateTimes();

            CommandManager.InvalidateRequerySuggested();
        }

        private void pauseTimer_Tick(object sender, EventArgs e)
        {
            pauseTime += 1;

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Updates time in progress bar, window title and in content of window
        /// </summary>
        private void updateTimes()
        {
            int secondsInProgress = _activity.secondsInProgress(pauseTime);
            int hours, minutes, seconds;

            hours = secondsInProgress / 3600;
            minutes = (secondsInProgress - hours * 3600) / 60;
            seconds = (secondsInProgress - hours * 3600 - minutes * 60);

            durationTime_label.Content = $"    {hours} h {minutes} min {seconds} s";
            progress_progressBar.Value = secondsInProgress;

            if (secondsInProgress / 60 >= _activity.PlannedMinutesDuration)
            {
                this.Title = $"Koniec. {hours}:{minutes}:{seconds}";
            }
            else
            {
                this.Title = $"{hours}:{minutes}:{seconds}";
            }
        }
    }
}
