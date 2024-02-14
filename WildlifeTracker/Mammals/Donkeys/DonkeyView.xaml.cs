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
using WildlifeTracker.Helper_classes;

namespace WildlifeTracker.Mammals.Donkeys
{
    /// <summary>
    /// Interaction logic for DonkeyView.xaml
    /// </summary>
    public partial class DonkeyView : UserControl
    {
        public DonkeyView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to clear the text boxes and check box
        /// </summary>
        public void ClearFields()
        {
            txtHeight.Text = "";
            txtWeight.Text = "";
            txtMaxLoad.Text = "";
            chkCarriesLoad.IsChecked = false;
        }

        private void checkBClicked(object sender, RoutedEventArgs e)
        {
            if (chkCarriesLoad.IsChecked == true)
            {
                // make the load weight text box and label visible
                lblMaxLoad.Visibility = Visibility.Visible;
                txtMaxLoad.Visibility = Visibility.Visible;
            }
            else
            {
                // make the load weight text box and label invisible
                lblMaxLoad.Visibility = Visibility.Hidden;
                txtMaxLoad.Visibility = Visibility.Hidden;
                // Also set the value of the text box to empty
                txtMaxLoad.Text = "";
            }
        }

        /// <summary>
        /// Method to read the height of the donkey, a list of errors is passed as a reference to add the error message to the list
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public double ReadHeight(ref List<string> errorList)
        {
            if (InputValidator.IsDoubleValid(txtHeight.Text))
                return double.Parse(txtHeight.Text);
            else
            {
                errorList.Add("Height is required and must be a positive number");
                return 0;
            }
        }

        /// <summary>
        /// Methdo to read the weight of the donkey, a list of errors is passed as a reference to add the error message to the list
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public double ReadWeight(ref List<string> errorList)
        {
            string weight = txtWeight.Text;
            if (!InputValidator.IsStringValid(weight)) // first check if the weight textbox is empty
            {
                errorList.Add("Weight is required");
                return 0;
            }
            else
            { // if it is not empty, check if it is a valid number
                if (InputValidator.IsDoubleValid(weight))
                    return double.Parse(weight); // if it is a valid number, return the value
                else
                { // if not valid, add an error message to the list that it should be a positive number
                    errorList.Add("Weight must be a positive number");
                    return 0;
                }
            }
        }

        /// <summary>
        /// Method to read if the donkey is used as a pack animal
        /// </summary>
        /// <returns></returns>
        public bool ReadIsUsedAsPackAnimal()
        {
            return chkCarriesLoad.IsChecked == true;
        }

        /// <summary>
        /// Method to read the max load of the donkey, a list of errors is passed as a reference to add the error message to the list.
        /// If the donkey is not used as a pack animal, the max load is not required and the value will be set to 0
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public double ReadMaxLoad(ref List<string> errorList)
        {
            if (chkCarriesLoad.IsChecked == true)
            {
                if (InputValidator.IsDoubleValid(txtMaxLoad.Text))
                    return double.Parse(txtMaxLoad.Text);
                else
                {
                    errorList.Add("Max load is required and must be a positive number");
                    return 0;
                }
            }
            else
                return 0;
        }
    }
}
