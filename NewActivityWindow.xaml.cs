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
using System.Windows.Shapes;

namespace Time_organization
{
    /// <summary>
    /// Interaction logic for NewActivityWindow.xaml
    /// </summary>
    public partial class NewActivityWindow : Window
    {
        private ObservableCollection<Activity> _history;
        private ObservableCollection<string> _optionsAvailable;
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

        /// <summary>
        /// Intialization of options available to be selected
        /// </summary>
        /// <returns>available options</returns>
        private ObservableCollection<string> initializeOptionsAvailable()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            options.Add("Śniadanie");
            options.Add("Obiad");
            options.Add("Naczynia");
            options.Add("Blender");
            options.Add("Fusion 360");
            options.Add("C# - nauka");
            options.Add("C# - projekt");
            options.Add("Cosinus");
            options.Add("Modelarstwo");
            options.Add("Gra komputerowa");
            options.Add("Pianino");
            options.Add("Ćwiczenia fizyczne");
            options.Add("Pomoc innym");
            options.Add("Inne");

            return options;
        }

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
        }
    }
}
