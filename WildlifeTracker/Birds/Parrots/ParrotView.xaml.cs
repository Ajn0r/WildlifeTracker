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

namespace WildlifeTracker.Birds.Parrots
{
    /// <summary>
    /// Interaction logic for ParrotView.xaml
    /// </summary>
    public partial class ParrotView : UserControl
    {
        public ParrotView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// MEthod to read the can speak property of the parrot
        /// </summary>
        /// <returns></returns>
        public bool ReadCanSpeak()
        {
            return chkCanTalk.IsChecked == true ? true : false;
        }

        /// <summary>
        /// Method to read the favorite phrase of the parrot
        /// </summary>
        /// <returns></returns>
        public string ReadFavoritePhrase()
        {
            // Check if the parrot can speak
            if (chkCanTalk.IsChecked == true)
            { // it it can, check if the favorite phrase is valid, e.g. not null or empty
                if (InputValidator.IsStringValid(txtFavvePhrase.Text))
                    return txtFavvePhrase.Text;
                else // if not valid, return a default message for the favorite phrase
                    return "Favorite phrase not known";
            }
            else
            { // if the parrot cannot speak, return a default message for the favorite phrase
                return "Cannot speak";
            }
        }

        /// <summary>
        /// Method to read the species of the parrot
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public string ReadSpecies(ref List<string> errorList)
        {
            if (InputValidator.IsStringValid(txtParrotSpecies.Text))
            {
                return txtParrotSpecies.Text;
            }
            else
            {
                errorList.Add("Species is required");
                return null;
            }
        }

        public void ClearFields()
        {
            txtFavvePhrase.Text = "";
            txtParrotSpecies.Text = "";
            chkCanTalk.IsChecked = false;
        }

        /// <summary>
        /// Method to handle the CanTalk check box checked event
        /// Makes the favorite phrase text box and label visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canTalkChecked(object sender, RoutedEventArgs e)
        {
            txtFavvePhrase.Visibility = Visibility.Visible;
            lblFavvePhrase.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method to handle the CanTalk check box unchecked event
        /// Hides the favorite phrase text box and label and clears the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanTalkUnchecked(object sender, RoutedEventArgs e)
        {
            txtFavvePhrase.Visibility = Visibility.Hidden;
            lblFavvePhrase.Visibility = Visibility.Hidden;
            txtFavvePhrase.Text = "";
        }


    }
}
