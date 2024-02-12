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

namespace WildlifeTracker.Birds.Owls
{
    /// <summary>
    /// Interaction logic for OwlView.xaml
    /// </summary>
    public partial class OwlView : UserControl
    {
        public OwlView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to read the if the owl is nocturnal oe not
        /// </summary>
        /// <returns></returns>
        public bool ReadIsNocturnal()
        {
            return chkNocturnal.IsChecked == true ? true : false;
        }

        /// <summary>
        /// Method to read the species of the owl
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public string ReadSpecies(ref List<string> errorList)
        {
            if (InputValidator.IsStringValid(txtOwlSpecies.Text))
                return txtOwlSpecies.Text;
            else
            {
                errorList.Add("Species cannot be empty");
                return null;
            }
        }

        public void ClearFields()
        {
            txtOwlSpecies.Text = "";
            chkNocturnal.IsChecked = false;
        }

        internal static void AddOwlSpecificAttributes(Owl animal, StackPanel animalInfoStack)
        {
            throw new NotImplementedException();
        }
    }
}
