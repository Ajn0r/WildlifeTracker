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

namespace WildlifeTracker.Mammals.Cats
{
    /// <summary>
    /// Interaction logic for CatView.xaml
    /// </summary>
    public partial class CatView : UserControl
    {
        public CatView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to read the breed of the cat, a list of errors is passed as a reference to add the error message to the list
        /// </summary>
        /// <returns></returns>
        public string ReadBreed(ref List<string> errorList)
        {
            // Validate the input before returning the value with the help of the InputValidator class
            if (InputValidator.IsStringValid(txtCatBreed.Text))
                return txtCatBreed.Text;
            else // If the input is not valid, add the error message to the error list and return an empty string
                errorList.Add("Breed is required");
                return "";
        }

        /// <summary>
        /// Method to read the favorite toy of the cat
        /// </summary>
        /// <returns></returns>
        public string ReadFavoriteToy()
        {
            // Validate the input before returning the value
            if (InputValidator.IsStringValid(txtCatToy.Text))
                return txtCatToy.Text;
            else // Favorite toy is not required, so return null if the input is not valid and the value will be set to "Unknown" in the Cat class
                return null;
        }

        /// <summary>
        /// Method to read the house trained status of the cat
        /// </summary>
        /// <returns></returns>
        public bool ReadIsHouseTrained()
        {
            // The yes radio button is checked as default, so no need to check if the no radio button is checked
            if (rdoLitterYes.IsChecked == true)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Returns the house trained status of the cat as a string
        /// </summary>
        /// <returns></returns>
        public string HouseTrained()
        {
            return ReadIsHouseTrained() ? "Yes" : "No";
        }

        /// <summary>
        /// Method to clear the fields of the cat view
        /// </summary>
        public void ClearFields()
        {
            txtCatBreed.Text = "";
            txtCatToy.Text = "";
            rdoLitterYes.IsChecked = true;
        }
        public static void AddCatSpecificAttributes(Cat cat, StackPanel stackPanel)
        {
            // Lägg till rad för Breed
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Breed:", cat.Breed);

            // Lägg till rad för FavoriteToy
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Favorite Toy:", cat.FavoriteToy);

            // Lägg till rad för IsHouseTrained
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Is House Trained:", cat.IsHouseTrained ? "Yes" : "No");
        }
    }
}
