using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Time_organization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Activity _activity;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ObservableCollection<string> _history = new ObservableCollection<string>();
        private bool msgBxShow;

        public MainWindow()
        {
            InitializeComponent();

            activityName_label.Content = "";
            durationTime_label.Content = "";
            progress_progressBar.Value = 0;

            DataContext = _history;
        }

        public MainWindow(Activity activity, ObservableCollection<string> history)
        {
            InitializeComponent();

            _activity = activity;
            _history = history;
            activityName_label.Content = _activity.Name;
            progress_progressBar.Maximum = _activity.PlannedMinutesDuration * 60;

            DataContext = _history;

            msgBxShow = true;

            updateTimes();
        }

        private void newActivity_button_Click(object sender, RoutedEventArgs e)
        {
            if (_activity != null)
            {
                _activity.ActualMinutesDuration = _activity.secondsInProgress() / 60;
                _history.Add($"{_activity.Name} - {_activity.StartTime.ToString("HH:mm")}\n Czas trwania: {_activity.ActualMinutesDuration} min");
            }

            NewActivityWindow newActivityWindow = new NewActivityWindow(_history);

            dispatcherTimer.Stop();
            this.Close();
            newActivityWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            if (_activity != null)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateTimes();

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Updates time in progress bar, window title and in content of window
        /// </summary>
        private void updateTimes()
        {
            int secondsInProgress = _activity.secondsInProgress();
            int hours, minutes, seconds;

            hours = secondsInProgress / 3600;
            minutes = (secondsInProgress - hours * 3600) / 60;
            seconds = (secondsInProgress - hours * 3600 - minutes * 60);

            durationTime_label.Content = $"    {hours} h {minutes} min {seconds} s";
            progress_progressBar.Value = secondsInProgress;

            if (secondsInProgress / 60 == _activity.PlannedMinutesDuration && msgBxShow)
            {
                MessageBox.Show("Założony czas dobiegł końca");
            }

            if (secondsInProgress / 60 >= _activity.PlannedMinutesDuration)
            {
                this.Title = "Następna aktywność. :)";
                msgBxShow = false;
            }
            else
            {
                this.Title = $"{hours}:{minutes}:{seconds}";
            }
        }
    }
}
