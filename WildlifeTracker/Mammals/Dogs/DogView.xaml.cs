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
using WildlifeTracker.Helper_classes;

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

        /// <summary>
        /// Check if the special trained checkbox is checked or not and enable or disable the combo box accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Read the dog breed from the text box
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public string ReadDogBreed(ref List<string> errors)
        {
            if (InputValidator.IsStringValid(txtDogBreed.Text))
            {
                return txtDogBreed.Text;
            }
            else
            {
                errors.Add("Breed is required");
                return null;
            }
        }

        /// <summary>
        /// Read the tail length from the text box
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public double ReadTailLength(ref List<string> errors)
        {
            if (InputValidator.IsDoubleValid(txtTailLength.Text))
            {
                return double.Parse(txtTailLength.Text);
            }
            else
            {
                errors.Add("Tail length is required and must be a number");
                return 0;
            }
        }

        /// <summary>
        /// Read the special trained checkbox
        /// </summary>
        /// <returns></returns>
        public bool ReadSpecialTrained()
        {
            return chkSpecialTrained.IsChecked == true;
        }

        /// <summary>
        /// Read the special trained type from the combo box if the special trained checkbox is checked,
        ///  otherwise return none
        /// </summary>
        /// <returns></returns>
        public SpecialTrainingType ReadTrainingType()
        {
            if (chkSpecialTrained.IsChecked == false)
            {
                return SpecialTrainingType.None;
            } else
            {
                return (SpecialTrainingType)cmbTrainingType.SelectedItem;
            }
        }

        public void ClearFields()
        {
            txtDogBreed.Text = "";
            txtTailLength.Text = "";
            chkSpecialTrained.IsChecked = false;
            cmbTrainingType.SelectedItem = SpecialTrainingType.None;
            cmbTrainingType.IsEnabled = false;
        }
    }

}
   

