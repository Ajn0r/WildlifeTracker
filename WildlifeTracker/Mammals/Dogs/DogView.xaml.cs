using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WildlifeTracker.Mammals.Dogs
{
    /// <summary>
    /// Interaction logic for DogView.xaml
    /// Handles all user interactions with the dog view and objects
    /// </summary>
    public partial class DogView : UserControl
    {
        public DogView()
        {
            InitializeComponent();
            // Populate the Special trained combo box with the special trained types
            cmbTrainingType.ItemsSource = Enum.GetValues(typeof(SpecialTrainingType));
            // Set the default value of the combo box to none and make it disabled, will be enabled when the special trained checkbox is checked
            cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            cmbTrainingType.IsEnabled = false;
        }

        private void checkBClicked(object sender, RoutedEventArgs e)
        {
            // If the checkbox is checked, enable the combo box and set the default value to none
            if (chkSpecialTrained.IsChecked == true)
            {
                cmbTrainingType.IsEnabled = true;
                cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            }
            else // If the checkbox is not checked, disable the combo box and set the default value to none
            {
                cmbTrainingType.IsEnabled = false;
                cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            }
        }
    }

}
   

