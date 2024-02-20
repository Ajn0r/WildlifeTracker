using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
        // Variable to hold the image path string, to be used when adding an image to the animal before an animal is created
        string imgPath;
        AnimalManager animalManager = new AnimalManager();

        // Variables to hold the click count for each of the column headers for sorting, to sort in ascending or descending order
        int idClick = 0;
        int ageClick = 0;
        int nameClick = 0;
        int colorClick = 0;
        int speciesCliked = 0;

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            btnAddAnimal.IsEnabled = false; // Disable the add animal button until a species is selected
            viewAnimalBtn.IsEnabled = false; // Disable the view animal button until an animal is created
            this.Title += " by Ronja Sjögren"; // Add my name to the title of the window
            this.Title += " - Version 2.0"; // add the version number
        }


        private void UpdateGUI()
        {
            ClearTextBoxes();
            UpdateAddAnimalButton();
            // Enable the view animal button if the data context is not null
            if (DataContext != null)
                viewAnimalBtn.IsEnabled = true;
            GetAnimalList();
            FillFoodSchedule();
        }

        /// <summary>
        /// Method that gets a copy of the animal list from the animal manager and populates the list view
        /// with the animals, the list populates the given list view columns with the animals properties based on DisplayMemberBinding in the xaml
        /// </summary>
        private void GetAnimalList()
        {
            // Clear the list view
            animalListView.Items.Clear();
            // Get a copy of the animal list from the animal manager
            List<Animal> animalsList = animalManager.GetAnimalListCopy();
            // Check that the list is not null or empty
            if (animalsList == null || animalsList.Count == 0)
                return;
            else
            { // If the list is not null or empty, loop through the list and add the animals to the list view
                foreach (Animal animal in animalsList)
                {
                    animalListView.Items.Add(animal);
                }
            }
        }

        /// <summary>
        /// Method to fill the food schedule listbox with the food schedule of the selected animal
        /// </summary>
        private void FillFoodSchedule()
        {
            // Check if the selected index of the animal list view is not -1 or if the data context is null, in that case, return because there is no animal to display
            if (this.DataContext == null)
                return;

            Animal animal = (Animal)this.DataContext; // Set the animal
 
            if (animal != null) // Check that the animal is not null
            {
                foodScheduleListBox.Items.Clear(); // clear the list box
                eaterHeader.Text = ((Animal)this.DataContext).GetFoodSchedule().Title(); // set the title of the food schedule to the eater type of the animal
                string[] foodList = animal.GetFoodSchedule().GetFoodListInfoStrings(); // get the food list from the animal
                foreach (string food in foodList) // loop through the food list and add the food to the list box
                {
                    foodScheduleListBox.Items.Add(food);
                }               
            }
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
            imgAnimal.Source = null;
            imgPath = "";

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
                    animal.Category = CategoryType.Mammal;
                    break;
                case CategoryType.Bird:
                    animal = CreateBird();
                    // animal.Category = Category.Bird;
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
            // local variables to store the number of teeth and if the mammal has fur or hair
            int numOfTeeth = 0;
            bool hasFurOrHair;
            if (InputValidator.IsNumberValid(txtTeeth.Text)) // Validate the number of teeth with the input validator class
                numOfTeeth = int.Parse(txtTeeth.Text); // If valid, parse the number of teeth to an integer
            else // If not valid, add an error message to the error list
                errorList.Add("Number of teeth is required and must be a positive number");
            if (rdoMamYes.IsChecked == true)
                hasFurOrHair = true;
            else
                hasFurOrHair = false;
            return (numOfTeeth, hasFurOrHair); // Return the number of teeth and if the mammal has fur or hair
        }

        /// <summary>
        /// Method that reads the specific values for birds from the UI
        /// </summary>
        /// <returns></returns>
        private (bool, bool, int) ReadBirdSpec()
        {
            // local variables to store if the bird sings, if it can fly and the wing span
            // sings and canFly are set to false by default
            bool sings = false; 
            bool canFly = false;
            int wingSpan = 0;
            
            if (chkSings.IsChecked == true) // Check if the bird sings, if so, set the sings variable to true
                sings = true;
            if (rdoBirdYes.IsChecked == true) // Check if the bird can fly, if so, set the canFly variable to true
                canFly = true;
            if (InputValidator.IsNumberValid(txtWingSpan.Text)) // Validate the wing span with the input validator class
                wingSpan = int.Parse(txtWingSpan.Text); // If valid, parse the wing span to an integer
            else
                errorList.Add("Wing span is required and must be a positive number");

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
                    ReadParrotValues(ref animal);
                    break;
                case BirdSpecies.Owl:
                    ReadOwlValues(ref animal);
                    break;
                case BirdSpecies.Penguin:
                    ReadPenguinValues(ref animal);
                    break;
            }
            return animal;
        }

        /// <summary>
        /// Method to read the penguin specific values from the UI
        /// </summary>
        /// <param name="animal"></param>
        private void ReadPenguinValues(ref Animal animal)
        {
            ((Penguin)animal).CanSwim = penguinView.ReadCanSwim();
            ((Penguin)animal).FavoriteFish = penguinView.ReadFavoriteFish();
        }

        /// <summary>
        /// Method to read the owl specific values from the UI
        /// </summary>
        /// <param name="animal"></param>
        private void ReadOwlValues(ref Animal animal)
        {
            ((Owl)animal).IsNocturnal = owlView.ReadIsNocturnal();
            ((Owl)animal).Species = owlView.ReadSpecies(ref errorList);
        }

        /// <summary>
        /// Method to read the parrot specific values from the UI
        /// </summary>
        /// <param name="animal"></param>
        private void ReadParrotValues(ref Animal animal)
        {
            ((Parrot)animal).FavoritePhrase = parrotView.ReadFavoritePhrase();
            ((Parrot)animal).CanSpeak = parrotView.ReadCanSpeak();
            ((Parrot)animal).Species = parrotView.ReadSpecies(ref errorList);
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
                errorList.Add("Age is required and must be a positive number");
            if (cmbGender.SelectedItem != null)
                animal.Gender = (GenderType)cmbGender.SelectedIndex;
            else
                animal.Gender = GenderType.Unknown;
            if (chkDomesticated.IsChecked == true)
                animal.IsDomesticated = true;
            else
                animal.IsDomesticated = false;
            animal.Color = txtColor.Text;
            animal.ImagePath = imgPath;
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
            if (listSpecies.SelectedItem is MammalSpecies)
                cmbCategory.SelectedItem = CategoryType.Mammal;
            else if (listSpecies.SelectedItem is BirdSpecies)
                cmbCategory.SelectedItem = CategoryType.Bird;

            selectedSpecies = listSpecies.SelectedItem.ToString();
            btnAddAnimal.IsEnabled = true; // Enable the add animal button

            // Toggle the visibility of the species views
            toggleVisibilty(selectedSpecies);
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
                animalManager.Add(animal); // Add the animal to the animal manager to store it in the list
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
            if (listSpecies.SelectedItem == null) // Check if a species is selected, if not, disable the add animal button
                btnAddAnimal.IsEnabled = false;
        }

        private void ViewAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the animalInfo window if the data context is not null, extra check in case the button enable state is not updated
            if (this.DataContext != null)
            {
                AnimalInfoWindow animalInfoWindow = new AnimalInfoWindow((Animal)this.DataContext);
                animalInfoWindow.Show();
            } else // If the data context is null, display an error message
            {
                MessageBox.Show("Error: No animal selected");
            }
        }

        /// <summary>
        /// Method to handle the add image button clicked event and save the image path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addImage_Clicked(object sender, RoutedEventArgs e)
        {
            // Create a new open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Uri filePath = null; // Create a new Uri to hold the image path to convert to a bitmap image
            // Set the filter to only allow image files
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            // When the user selects a file and clicks ok
            if (openFileDialog.ShowDialog() == true)
            {
                // Set the image path to the selected file path
                imgPath = openFileDialog.FileName;
                filePath = new Uri(imgPath); // Create a new Uri with the image path
            }
            // If the image path is not null or empty, set the image source of the image control to the image path
            if (!string.IsNullOrEmpty(imgPath))
                imgAnimal.Source = new BitmapImage(filePath); // Create a new bitmap image with the Uri converted image path
           
        }

        /// <summary>
        /// Method to handle the listview item selection changed event
        /// Sets the data context of the window to the selected animal to display the animal details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimalListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // Check if an animal is selected in the list view
            if (animalListView.SelectedItem == null)
                return; 
            Animal selectedAnimal = (Animal)animalListView.SelectedItem; // Get the selected animal
            if (selectedAnimal != null) // If the selected animal is not null, set the data context of the window to the selected animal
            {
                this.DataContext = selectedAnimal;
            }
            UpdateGUI(); // Update the GUI
        }

        /// <summary>
        /// Method to sort the list, not used currently, want to refactor this later to only have one method for sorting
        /// and not one for each column header...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            // Get the coloumn header that was clicked
            GridViewColumnHeader column = (sender as GridViewColumnHeader); // sender is the column header that was clicked and cast it to a GridViewColumnHeader
            string sortBy = column.Tag.ToString(); // Get the tag of the column header, which is the same property name of the animal object


            animalManager.SortList(sortBy); // Call the sort list method of the animal manager to sort the list based on the property name
            GetAnimalList(); // Update the list view with the sorted list
        }

        /// <summary>
        /// Method to handle the id column header clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeaderId_Click(object sender, RoutedEventArgs e)
        {
            idClick++; // Increment the click count
            // If the click count is even, sort the list in descending order
            if (idClick % 2 == 0)
                animalManager.SortListDesc("Id");
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList("Id");
            GetAnimalList(); // Update the list view with the sorted list
        } 

        /// <summary>
        /// Method to handle the age column header clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeaderAge_Click(object sender, RoutedEventArgs e)
        {
            ageClick++; // Increment the click count
            // If the click count is even, sort the list in descending order
            if (ageClick % 2 == 0)
                animalManager.SortListDesc("Age");
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList("Age");
            GetAnimalList(); // Update the list view with the sorted list
        }

        private void SortByColumnHeaderName_Click(object sender, RoutedEventArgs e)
        {
            nameClick++; // Increment the click count
            // If the click count is even, sort the list in descending order
            if (nameClick % 2 == 0)
                animalManager.SortListDesc("Name");
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList("Name");
            GetAnimalList(); // Update the list view with the sorted list
        }

        /// <summary>
        /// Method to handle the color column header clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeaderColor_Click(object sender, RoutedEventArgs e)
        {
            colorClick++; // Increment the click count
            // If the click count is even, sort the list in descending order
            if (colorClick % 2 == 0)
                animalManager.SortListDesc("Color");
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList("Color");
            GetAnimalList(); // Update the list view with the sorted list
        }

        /// <summary>
        /// Method to handle the species column header clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeaderSpecies_Click(object sender, RoutedEventArgs e)
        {
            speciesCliked++; // Increment the click count
            // If the click count is even, sort the list in descending order
            if (speciesCliked % 2 == 0)
                animalManager.SortListDesc("Species");
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList("Species");
            GetAnimalList(); // Update the list view with the sorted list
        }
    }


}
