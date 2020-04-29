using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Time_organization
{
    /// <summary>
    /// Interaction logic for NewActivityWindow.xaml
    /// </summary>
    public partial class NewActivityWindow : Window
    {
        private ObservableCollection<Activity> _history;
        private ObservableCollection<string> _optionsAvailable;

        /*
         * Constructor
         */

        public NewActivityWindow(ObservableCollection<Activity> history)
        {
            InitializeComponent();
            _history = history;
            _optionsAvailable = initializeOptionsAvailable();

            DataContext = _optionsAvailable;

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        /*
         * Button click methods
         */

        private void startActivity_button_Click(object sender, RoutedEventArgs e)
        {
            int timeDuration = 0;

            if (activityName_comboBox.SelectedIndex >= 0 && int.TryParse(time_textbox.Text, out timeDuration) && timeDuration > 0)
            {
                Activity activity = new Activity()
                {
                    Name = activityName_comboBox.Text,
                    PlannedMinutesDuration = timeDuration,
                    Note = note_textbox.Text
                };

                MainWindow mainWindow = new MainWindow(activity, _history);
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Wybierz aktywność i podaj zakładany czas.");
            }
        }

        /*
         * Additional methods
         */

        /// <summary>
        /// Intialization of options available to be selected
        /// </summary>
        /// <returns>available options</returns>
        private ObservableCollection<string> initializeOptionsAvailable()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            string fileName = "availableOptions.txt";
            string filePath = $"{Environment.CurrentDirectory}\\{fileName}";
            if (File.Exists(filePath))
            {
                string[] optionsInFile = File.ReadAllLines(filePath);
                foreach (var line in optionsInFile)
                {
                    options.Add(line);
                }
            }
            else
            {
                MessageBox.Show("Plik z opcjami nie istnieje. Ładuję domyślne");

                options.Add("Rzecz pozytywna");
                options.Add("Rzecz neutralna");
                options.Add("Rzecz negatywna");
            }

            return options;
        }
    }
}
