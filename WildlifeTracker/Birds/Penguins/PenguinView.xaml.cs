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

namespace WildlifeTracker.Birds.Penguins
{
    /// <summary>
    /// Interaction logic for PenguinView.xaml
    /// </summary>
    public partial class PenguinView : UserControl
    {
        public PenguinView()
        {
            InitializeComponent();
        }

        public static void AddPenguinSpecificAttributes(Penguin animal, StackPanel animalInfoStack)
        {
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Can swim", animal.CanSwim ? "Yes" : "No");
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Favorite fish", animal.FavoriteFish);
        }

        /// <summary>
        /// Method that returns if the penguin can swim or not
        /// </summary>
        /// <returns></returns>
        public bool ReadCanSwim()
        {
            return chkCanSwim.IsChecked == true ? true : false;
        }

        /// <summary>
        /// Method to read the favorite fish from the text box, if no value is entered, return a default value
        /// </summary>
        /// <returns></returns>
        public string ReadFavoriteFish()
        {
            if (InputValidator.IsStringValid(txtFavveFish.Text))
            {
                return txtFavveFish.Text;
            }
            else
            {
                return "Loves all fish";
            }
        }

        internal void ClearFields()
        {
            txtFavveFish.Text = "";
            chkCanSwim.IsChecked = false;
        }
    }
}
