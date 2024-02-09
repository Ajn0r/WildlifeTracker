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
using WildlifeTracker.Birds;
using WildlifeTracker.Helper_classes;
using WildlifeTracker.Mammals;
using WildlifeTracker.Mammals.Cats;
using WildlifeTracker.Mammals.Dogs;
using WildlifeTracker.Mammals.Donkeys;


namespace WildlifeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // A list to store any errors that occur when creating an animal
        private List<string> errorList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            btnAddAnimal.IsEnabled = false; // Disable the add animal button until a species is selected
        }

        private void UpdateGUI()
        {
            ClearTextBoxes();
            UpdateAddAnimalButton();
        }

        /// <summary>
        /// MEthod to clear all the text boxes and reset the combo boxes once an animal has been added
        /// </summary>
        private void ClearTextBoxes()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtColor.Text = "";
            cmbGender.SelectedItem = GenderType.Unknown;
            chkDomesticated.IsChecked = false;
            txtTeeth.Text = "";
            chkSings.IsChecked = false;
            txtWingSpan.Text = "";

            // Clear the species views
            dogView.ClearFields();
            catView.ClearFields();
            donkeyView.ClearFields();
            parrotView.ClearFields();
            owlView.ClearFields();
            penguinView.ClearFields();
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
        /// Method to create a new animal based on the category selected
        /// </summary>
        /// 
        private Animal ReadInput()
        {
            CategoryType selectedCategory = ReadCategory();

            Animal animal = null;
            switch (selectedCategory)
            {
                case CategoryType.Mammal:
                    animal = CreateMammal();
                    break;
                case CategoryType.Bird:
                    animal = CreateBird();
                    break;
            }
            if (animal != null)
                ReadCommonValues(ref animal);
            return animal;
        }

        /// <summary>
        /// Reads the mammal specific values from the UI
        /// </summary>
        /// <returns></returns>
        private (int, bool) ReadMammalSpec()
        {
            int numOfTeeth = 0;
            bool hasFurOrHair;
            if (InputValidator.IsNumberValid(txtTeeth.Text))
                numOfTeeth = int.Parse(txtTeeth.Text);
            else
                errorList.Add("Number of teeth is required and must be a number");
            if (rdoMamYes.IsChecked == true)
                hasFurOrHair = true;
            else
                hasFurOrHair = false;
            return (numOfTeeth, hasFurOrHair);
        }

        /// <summary>
        /// Method that reads the specific values for birds from the UI
        /// </summary>
        /// <returns></returns>
        private (bool, bool, int) ReadBirdSpec()
        {
            bool sings = false;
            bool canFly = false;
            int wingSpan = 0;
            if (chkSings.IsChecked == true)
                sings = true;
            if (rdoBirdYes.IsChecked == true)
                canFly = true;
            if (InputValidator.IsNumberValid(txtWingSpan.Text))
                wingSpan = int.Parse(txtWingSpan.Text);
            else
                errorList.Add("Wing span is required and must be a number");

            return (sings, canFly, wingSpan);
        }

        /// <summary>
        /// Method that creates a new Mammal object based on the selected species 
        /// and the mammal specific values. Based on the species, it will also read the
        /// specific values for that species from the UI
        /// </summary>
        /// <returns>animal</returns>
        private Animal CreateMammal()
        {
            Animal animal = null;
            int numOfTeeth;
            bool hasFurOrHair;
            // Read the common mammal specific values
            (numOfTeeth, hasFurOrHair) = ReadMammalSpec();
            // Get the selected species from the list view
            MammalSpecies species = (MammalSpecies)listSpecies.SelectedItem;
            // Create a new mammal object based on the selected species with the mammalfactory
            animal = MammalFactory.CreateMammal(species, numOfTeeth, hasFurOrHair);

            // Read the different mammal specific values based on the species
            switch (species)
            {
                case MammalSpecies.Cat:
                    ReadCatValues(ref animal);
                    break;
                case MammalSpecies.Dog:
                    ReadDogValues(ref animal);
                    break;
                case MammalSpecies.Donkey:
                    ReadDonkeyValues(ref animal);
                    break;
            }
            return animal;
        }

        /// <summary>
        /// Method to create a new Bird object based on the selected species
        /// </summary>
        /// <returns></returns>
        private Animal CreateBird()
        {
            Animal animal = null;

            // Read the common bird specific values
            (bool sings, bool canFly, int wingSpan) = ReadBirdSpec();
            // Get the selected species from the list view
            BirdSpecies species = (BirdSpecies)listSpecies.SelectedItem;
            // Create a new bird object based on the selected species with the birdfactory
            animal = BirdFactory.CreateBird(species, sings, canFly, wingSpan);

            // Read the different bird specific values based on the species
            // Keeping them in this method for now, instead of seperate as it is for the mammals, might refactor later
            switch (species)
            {
                case BirdSpecies.Parrot:
                    ((Parrot)animal).FavoritePhrase = parrotView.ReadFavoritePhrase();
                    ((Parrot)animal).CanSpeak = parrotView.ReadCanSpeak();
                    ((Parrot)animal).Species = parrotView.ReadSpecies(ref errorList);
                    break;
                case BirdSpecies.Owl:
                    ((Owl)animal).IsNocturnal = owlView.ReadIsNocturnal();
                    ((Owl)animal).Species = owlView.ReadSpecies(ref errorList);
                    break;
                case BirdSpecies.Penguin:
                    ((Penguin)animal).CanSwim = penguinView.ReadCanSwim();
                    ((Penguin)animal).FavoriteFish = penguinView.ReadFavoriteFish();
                    break;
            }
            return animal;
        }

        /// <summary>
        /// Method that reads the cat specific values from the UI 
        /// if the animal is a cat
        /// </summary>
        /// <param name="animal"></param>
        private void ReadCatValues(ref Animal animal)
        {
            ((Cat)animal).Breed = catView.ReadBreed(ref errorList); // errorList is passed by reference incase of errors
            ((Cat)animal).FavoriteToy = catView.ReadFavoriteToy();
            ((Cat)animal).IsHouseTrained = catView.ReadIsHouseTrained();
        }

        /// <summary>
        /// Method to read the dog specific values from the UI
        /// </summary>
        /// <param name="animal"></param>
        private void ReadDogValues(ref Animal animal)
        {
            ((Dog)animal).Breed = dogView.ReadDogBreed(ref errorList); // errorList is passed by reference incase of errors
            ((Dog)animal).TailLength = dogView.ReadTailLength(ref errorList); // errorList is passed by reference incase of errors
            ((Dog)animal).IsSpecialTrained = dogView.ReadSpecialTrained();
            ((Dog)animal).SpecialTrainingType = dogView.ReadTrainingType();
        }

        /// <summary>
        /// Mwthod to read the donkey specific values from the UI
        /// </summary>
        /// <param name="animal"></param>
        private void ReadDonkeyValues(ref Animal animal)
        {
            ((Donkey)animal).Height = donkeyView.ReadHeight(ref errorList);
            ((Donkey)animal).Weight = donkeyView.ReadWeight(ref errorList);
            ((Donkey)animal).IsUsedAsPackAnimal = donkeyView.ReadIsUsedAsPackAnimal();
            ((Donkey)animal).MaxLoad = donkeyView.ReadMaxLoad(ref errorList);
        }   

        /// <summary>
        /// Method to read common attributes from the UI
        /// </summary>
        private void ReadCommonValues(ref Animal animal)
        {
            if (InputValidator.IsStringValid(txtName.Text))
                animal.Name = txtName.Text;
            else
                errorList.Add("Name is required");
            if (InputValidator.IsNumberValid(txtAge.Text))
                animal.Age = int.Parse(txtAge.Text);
            else
                errorList.Add("Age is required and must be a number");
            if (cmbGender.SelectedItem != null)
                animal.GenderType = (GenderType)cmbGender.SelectedIndex;
            else
                animal.GenderType = GenderType.Unknown;
            if (chkDomesticated.IsChecked == true)
                animal.IsDomesticated = true;
            else
                animal.IsDomesticated = false;
            animal.Color = txtColor.Text;
        }

        /// <summary>
        /// Method to read the selected category from the category combo box
        /// </summary>
        /// <returns></returns>
        private CategoryType ReadCategory()
        {
            return (CategoryType)cmbCategory.SelectedItem;
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

        /// <summary>
        /// A method to fill the list view with the species of the selected category
        /// </summary>
        /// <param name="categoryType"></param>
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

        /// <summary>
        /// Method to fill the list view with all the species of both Mammal and Bird
        /// </summary>
        private void fillAllSpecies()
        {
            listSpecies.Items.Clear();
            fillListBirdSpecies();
            fillListMammalSpecies();
        }

        /// <summary>
        /// Method to fill the list view with the Bird species
        /// </summary>
        private void fillListBirdSpecies()
        {
            // Loop through the enum of bird species
            foreach (BirdSpecies species in Enum.GetValues(typeof(BirdSpecies)))
            {
                listSpecies.Items.Add(species);
            }
        }

        /// <summary>
        /// Method to fill the list view with the Mammal species
        private void fillListMammalSpecies()
        {
            // Loop through the ENUM of Mammal species
            foreach (MammalSpecies species in Enum.GetValues(typeof(MammalSpecies)))
            {
                listSpecies.Items.Add(species);
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
            UpdateAddAnimalButton();
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
            UpdateAddAnimalButton();
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
                selectedSpecies = listSpecies.SelectedItem.ToString();
                btnAddAnimal.IsEnabled = true;
            }

            // Toggle the visibility of the species views
            toggleVisibilty(selectedSpecies);

            // Set the category combo box to the selected species category
            if (Enum.TryParse(selectedSpecies, out MammalSpecies mammalSpecies)) {
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
        
        /// <summary>
        /// Method to display an error message if the animal could not be created
        /// </summary>
        private void DisplayErrorMessage()
        {
            string errors = "The animal could not be created. \n";
            // Set the error message info based on the number of errors
            if (errorList.Count > 1)
                errors += "The following errors occured: \n";
            else
                errors += "The following error occured: \n";
            // Then loop through the error list and add the errors to the error message
            foreach (string error in errorList)
            {
                errors += "- " + error + "\n";
            }
            // Display the error message
            InputValidator.DisplayErrorMessage(errors);
            // Clear the error list
            errorList.Clear();
        }

        /// <summary>
        /// Method to handle the add new animal button clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewAnimalClicked(object sender, RoutedEventArgs e)
        {
            Animal animal = ReadInput();
            // Check if the animal object is null or if there are any errors
            if (errorList.Count > 0 || animal == null)
            {
                DisplayErrorMessage();
            }
            else
            { // Else the animal was created successfully
                MessageBox.Show("Animal added");
                // Set the data context of the animal view to the animal object to display the animal details
                this.DataContext = animal;
                UpdateGUI(); // Update the GUI
            }
        }

        /// <summary>
        /// Method that updates the add animal button based on if a species is selected or not
        /// to make sure the user can't add an animal without selecting a species
        /// </summary>
        private void UpdateAddAnimalButton()
        {
            if (listSpecies.SelectedItem == null)
                btnAddAnimal.IsEnabled = false;
        }
    }
}
