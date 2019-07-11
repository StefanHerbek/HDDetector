using System;
using System.Collections.Generic;
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

namespace Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int CheckParametersValue()
        {
            // check if values are integer
            if (Int32.TryParse(PercentageBox1.Text, out int x) && Int32.TryParse(PercentageBox2.Text, out x) && Int32.TryParse(PercentageBox3.Text, out x) && Int32.TryParse(PercentageBox4.Text, out x))
            {
                // check if values are greater than 0
                if (Int32.Parse(PercentageBox1.Text) < 1 || Int32.Parse(PercentageBox2.Text) < 1 || Int32.Parse(PercentageBox3.Text) < 1 || Int32.Parse(PercentageBox4.Text) < 1)
                {
                    return -1;
                }
                // sum parameters value
                int percentageSum = Int32.Parse(PercentageBox1.Text) + Int32.Parse(PercentageBox2.Text)
                              + Int32.Parse(PercentageBox3.Text) + Int32.Parse(PercentageBox4.Text);
                // check if parameter sum is equal 100
                if (percentageSum == 100)
                {
                    return 1;
                }
                else
                {
                    return -3;
                }
            }
            else
            {
                return -2;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int ParametersValidation = CheckParametersValue();

            // if parameters value is equal 100
            if (ParametersValidation == 1)
            {
                //code this part if parameters are OK
            }
            // display error message if some parameter is lower than zero
            else if (ParametersValidation == -1)
            {
                MessageBox.Show("Parameter must be greater than zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // display error message if parameters value is not int
            else if (ParametersValidation == -2)
            {
                MessageBox.Show("Parameter must be integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // display error message if sum is not equal 100
            else if (ParametersValidation == -3)
            {
                MessageBox.Show("Parameters sum is not equal 100", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
