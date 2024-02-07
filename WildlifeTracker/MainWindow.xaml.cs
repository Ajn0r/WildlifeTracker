using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace WildlifeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        /// <summary>
        /// Method to populate all the combo boxes and set the default values
        /// </summary>
        private void PopulateComboBoxes()
        {
            // Populate the combo box with the categories
            cmbCategory.ItemsSource = Enum.GetValues(typeof(CategoryType));
            // Populate the gender combo box with the gender types
            cmbGender.ItemsSource = Enum.GetValues(typeof(GenderType));
            // Populate the Special trained combo box with the special trained types
            cmbTrainingType.ItemsSource = Enum.GetValues(typeof(SpecialTrainingType));
            // Set the default value of the combo box to none and make it disabled, will be enabled when the special trained checkbox is checked
            cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            cmbTrainingType.IsEnabled = false;
        }

        /// <summary>
        /// Method to handle the checkbox for special trained in the UI for dogs.
        /// Enable the special trained combo box when the checkbox is checked.
        /// Disable the special trained combo box when the checkbox is unchecked and set value to none
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void specTrainedCBoxChanged(object sender, RoutedEventArgs e)
        {
            if (chkSpecialTrained.IsChecked == true)
            { // If the checkbox is checked, enable the combo box to let the user choose the special training type
                cmbTrainingType.IsEnabled = true;
            }
            else
            {
                cmbTrainingType.IsEnabled = false; // If the checkbox is unchecked, disable the combo box and set the value to none
                cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            }
        }
    }
}
