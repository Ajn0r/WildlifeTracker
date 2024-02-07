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

        }
    }
}
