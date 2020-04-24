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
        Stack<string> _history;

        public NewActivityWindow(Stack<string> history)
        {
            InitializeComponent();
            _history = history;
        }

        private void startActivity_button_Click(object sender, RoutedEventArgs e)
        {
            int timeDuration = 0;

            if (activityName_comboBox.SelectedIndex > 0 && int.TryParse(time_textbox.Text, out timeDuration) && timeDuration > 0)
            {
                Activity activity = new Activity()
                {
                    Name = activityName_comboBox.Text,
                    MinutesDuration = timeDuration
                };

                MainWindow mainWindow = new MainWindow(activity, _history);
                mainWindow.Show();

                this.Close();
            }
        }
    }
}
