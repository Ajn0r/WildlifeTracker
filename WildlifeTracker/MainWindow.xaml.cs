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
using WildlifeTracker.Helper_classes;
using WildlifeTracker.Mammals.Cats;


namespace WildlifeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Get the list of Mammal and bird types, will refactor this later
        private string[] mammalList = Mammal.GetMammalTypes();
        private string[] birdList = Bird.GetBirdTypes();

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();

            Cat cat = new Cat("Maja", 10, true, GenderType.Female, CategoryType.Mammal, "multi color", 33, true, "Housecat", true, "Catnip");
            this.DataContext = cat;
        }

        /// <summary>
        /// Method to populate all the combo boxes and set the default values
        /// </summary>
        private void PopulateComboBoxes()
        {
            // Populate the combo box with the categories
            cmbCategory.ItemsSource = Enum.GetValues(typeof(CategoryType));
            // Set the default value of the category combo box to Mammal
            cmbCategory.SelectedItem = CategoryType.Mammal;
            // Populate the gender combo box with the gender types
            cmbGender.ItemsSource = Enum.GetValues(typeof(GenderType));
        }

        /// <summary>
        /// Method to read common attributes from the UI
        /// </summary>
        private void ReadCommonValues(ref Animal animal)
        {
        }

        /// <summary>
        /// Method to handle the category combo box selection changed event.
        /// Updates the UI based on the selected category, either Mammal or Bird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCategoryChanged(object sender, SelectionChangedEventArgs e)
        {
            // First, get the selected category
            CategoryType selectedCategory = (CategoryType)cmbCategory.SelectedItem;
            // Check if the selected category is Mammal
            if (selectedCategory == CategoryType.Mammal)
            {
                // If the selected category is Mammal, then show the Mammal spec box
                mammalSpec.Visibility = Visibility.Visible;
                // Hide the Bird specification box
                birdSpec.Visibility = Visibility.Hidden;
                
            }
            else
            {
                // If the selected category is not Mammal, then show the Bird spec box
                birdSpec.Visibility = Visibility.Visible;
                // Hide the Mammal spec box
                mammalSpec.Visibility = Visibility.Collapsed;
            }
            // Check if the "Show all animals" is checked, and only update the list if not.
            if (chkShowAll.IsChecked == false)
                fillListView(selectedCategory);
        }

        private void fillListView(CategoryType categoryType)
        {
            // Clear the list view
            listSpecies.Items.Clear();
            
            // Check if the selected category is Mammal, and populate the list view with the Mammal objects
            if (categoryType == CategoryType.Mammal)
            {
               fillListMammalSpecies();
            }
            else
            {
                fillListBirdSpecies();
            }
        }

        private void fillAllSpecies()
        {
            listSpecies.Items.Clear();
            fillListBirdSpecies();
            fillListMammalSpecies();
        }

        private void fillListBirdSpecies()
        {
            // Loop through the list of Birds
            foreach (string bird in birdList)
            {
                // Create a new list view item
                ListViewItem item = new ListViewItem();
                // Set the content of the list view item to the name of the bird
                item.Content = bird;
                // Add the list view item to the list view
                listSpecies.Items.Add(item);
            }
        }

        /// <summary>
        /// Method to fill the list view with the Mammal species
        private void fillListMammalSpecies()
        {
            // Loop through the list of Mammals
            foreach (string mammal in mammalList)
            {
                // Create a new list view item
                ListViewItem item = new ListViewItem();
                // Set the content of the list view item to the name of the mammal
                item.Content = mammal;
                // Add the list view item to the list view
                listSpecies.Items.Add(item);
            }
        }

        /// <summary>
        /// Method to handle the show all check box checked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowAllChecked(object sender, RoutedEventArgs e)
        {
            // If the show all check box is checked, then show all the species in the species list
            fillAllSpecies();
            // Grey out the categories box
            cmbCategory.IsEnabled = false;
        }

        /// <summary>
        /// Method to handle the show all check box unchecked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowAllUnchecked(object sender, RoutedEventArgs e)
        {
            // If the show all check box is unchecked, then show only the species of the selected category
            fillListView((CategoryType)cmbCategory.SelectedItem);
            // Enable the categories box
            cmbCategory.IsEnabled = true;
        }

        /// <summary>
        /// Method to handle when the user chooses a species from the list view
        /// Will need to refactor this later..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speciesSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSpecies;
            // Check that a item has been selected
            if (listSpecies.SelectedItem == null)
                return;
            else
            {
                ListViewItem item = (ListViewItem)listSpecies.SelectedItem;
                selectedSpecies = item.Content.ToString();
            }

            // Toggle the visibility of the species views
            toggleVisibilty(selectedSpecies);

            // Set the category combo box to the selected species category
            if (mammalList.Contains(selectedSpecies)) {
                cmbCategory.SelectedItem = CategoryType.Mammal;
            }
            else
            {
                cmbCategory.SelectedItem = CategoryType.Bird;
            }
        }

        /// <summary>
        /// Method to handle the different species views visibility
        /// based on which species is selected
        /// </summary>
        /// <param name="species"></param>
        private void toggleVisibilty(string species)
        {
            // First hide all the species views
            catView.Visibility = Visibility.Hidden;
            dogView.Visibility = Visibility.Hidden;
            donkeyView.Visibility = Visibility.Hidden;
            parrotView.Visibility = Visibility.Hidden;
            owlView.Visibility = Visibility.Hidden;
            penguinView.Visibility = Visibility.Hidden;

            // Then, based on the selected species, show the correct species view
            switch (species)
            {
                case "Cat":
                    catView.Visibility = Visibility.Visible;
                    break;
                case "Dog":
                    dogView.Visibility = Visibility.Visible;
                    break;
                case "Donkey":
                    donkeyView.Visibility = Visibility.Visible;
                    break;
                case "Parrot":
                    parrotView.Visibility = Visibility.Visible;
                    break;
                case "Owl":
                    owlView.Visibility = Visibility.Visible;
                    break;
                case "Penguin":
                    penguinView.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
