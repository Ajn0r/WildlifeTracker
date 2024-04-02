using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WildlifeTracker.Birds;
using WildlifeTracker.Food;
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

        // Create a new animal manager to manage the animals
        AnimalManager animalManager = new AnimalManager();

        // A dictionary to hold the different fooditems and animals
        Dictionary<string, ListManager<Animal>> foodItemsDict = new Dictionary<string, ListManager<Animal>>();

        // A Dictionary to keep track of the click count for each column header
        Dictionary<string, int> clickCountDict = new Dictionary<string, int>();

        // A food schedule object to hold the temporary food schedule
        FoodSchedule foodSchedule = new FoodSchedule();

        // A Dictionary to hold the foodschedule and animals
        Dictionary<FoodSchedule, ListManager<Animal>> foodScheduleDict = new Dictionary<FoodSchedule, ListManager<Animal>>();

        // FoodScheduleManager to manage the food schedules
        FoodScheduleManager foodScheduleManager = new FoodScheduleManager();

        // String to hold the filename
        private string filename;

        public MainWindow()
        {
            Test(); // Call the test method to add some test animals to the list
            InitializeComponent();
            PopulateComboBoxes();
            btnAddAnimal.IsEnabled = false; // Disable the add animal button until a species is selected
            viewAnimalBtn.IsEnabled = false; // Disable the view animal button until an animal is created
            this.Title += " by Ronja Sjögren"; // Add my name to the title of the window
            this.Title += " - Version 4.0"; // add the version number
            FillAnimalList();
            PopulateScheduleListCombo();
        }

        private void UpdateGUI()
        {
            UpdateAddAnimalButton();
            // Enable the view animal button if the data context is not null
            if (DataContext != null)
                viewAnimalBtn.IsEnabled = true;
            FillFoodSchedule();
        }

        /// <summary>
        /// Method to fill the animal list view with the animals from the animal manager, based on the animal info string
        /// </summary>
        private void FillAnimalList()
        {
            // Get the animal info string array from the animal manager, containing the animal info
            string[] animalList = animalManager.ToStringArray();

            // Clear the list view
            animalListView.Items.Clear();

            // Loop through the animal list and add the animals to the list view, seperating the strings by the comma
            foreach (string animal in animalList)
            {
                animalListView.Items.Add(animal.Split(','));
            }

        }

        /// <summary>
        /// Method to fill the food schedule listbox with the food schedule of the selected animal
        /// </summary>
        private void FillFoodSchedule()
        {
            // Get the selected animal from the data context
            Animal animal = (Animal)this.DataContext;
            // Check if the animal is not null
            if (animal != null)
            {
                // Get the food schedule of the selected animal
                FoodSchedule foodSchedule;
                if (foodScheduleManager.GetFoodScheduleForAnimal(animal) == null)
                {
                    foodScheduleListBox.Items.Clear();
                    scheduleComboBox.SelectedItem = null;
                    return;
                }
                foodSchedule = foodScheduleManager.GetFoodScheduleForAnimal(animal); // Get the food schedule for the selected animal
                scheduleComboBox.SelectedItem = foodSchedule.ScheduleTitle; // Set the selected item in the combo box to the food schedule title
                FillFoodScheduleList(foodSchedule); // Fill the food schedule list box with the food schedule
            }
        }

        /// <summary>
        /// Method to populate the schedule list box with the food schedule items
        /// </summary>
        /// <param name="selectedSchedule"></param>
        private void FillFoodScheduleList(FoodSchedule selectedSchedule)
        {
            // Clear the list box
            foodScheduleListBox.Items.Clear();
            // Get the food list info strings from the selected schedule
            string[] strings = selectedSchedule.GetFoodListInfoStrings();
            foodScheduleListBox.Items.Add(selectedSchedule.ToString() + ":"); // Add the food schedule title to the list box
            foodScheduleListBox.Items.Add("-----------------");
            foreach (string foodItem in strings) // for each food item in the strings array, add the food item to the list box
            {
                foodScheduleListBox.Items.Add(foodItem);
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
            CategoryType selectedCategory;
            if (cmbCategory.SelectedItem == null)
            {
                errorList.Add("Category is required");
                return null;
            }
            else
                selectedCategory = ReadCategory();

            Animal animal = null;
            switch (selectedCategory)
            {
                case CategoryType.Mammal:
                    animal = CreateMammal();
                    animal.Category = CategoryType.Mammal;
                    break;
                case CategoryType.Bird:
                    animal = CreateBird();
                    animal.Category = CategoryType.Bird;
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
            // Check that a species is selected
            if (listSpecies.SelectedItem == null)
            {
                errorList.Add("Species is required");
            }
            // Get the selected species from the list view
            string strSpecies = listSpecies.SelectedItem.ToString();
            // Convert the string to a MammalSpecies enum
            MammalSpecies species = (MammalSpecies)Enum.Parse(typeof(MammalSpecies), strSpecies);

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
            // Get the selected species from the list view as a string
            if (listSpecies.SelectedItem == null)
            {
                errorList.Add("Species is required");
            }
            string strSpecies = listSpecies.SelectedItem.ToString();
            // convert to the enum
            BirdSpecies species = (BirdSpecies)Enum.Parse(typeof(BirdSpecies), strSpecies);
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
            if (cmbCategory.SelectedItem == null)
            {
                errorList.Add("Category is required");
            }
            return (CategoryType)cmbCategory.SelectedItem;
        }

        /// <summary>
        /// Method to handle the category combo box selection changed event.
        /// Updates the UI based on the selected category, either Mammal or Bird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void cmbCategoryChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedItem == null)
                return;
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
                listSpecies.Items.Add(species.ToString());
            }
        }

        /// <summary>
        /// Method to fill the list view with the Mammal species
        private void fillListMammalSpecies()
        {
            // Loop through the ENUM of Mammal species
            foreach (MammalSpecies species in Enum.GetValues(typeof(MammalSpecies)))
            {
                listSpecies.Items.Add(species.ToString());
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
            {
                btnAddAnimal.IsEnabled = false; // Disable the add animal button if no species is selected
                return;
            }
            // Check if the selected species is a mammal based on the selected item, since it is handled as a string now, i use the Enum.IsDefined method to check if the string is a valid enum value
            if (Enum.IsDefined(typeof(MammalSpecies), listSpecies.SelectedItem.ToString()))
                cmbCategory.SelectedItem = CategoryType.Mammal; // Set the category to Mammal
            else if (Enum.IsDefined(typeof(BirdSpecies), listSpecies.SelectedItem.ToString())) // Do the same check for the bird species
                cmbCategory.SelectedItem = CategoryType.Bird;

            // Set the selected species to the selected item in the list view cast to a string to be able to use it in the switch statement and make it work with the AnimalType property
            selectedSpecies = (string)listSpecies.SelectedItem;
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
                animalManager.AddAnimalWithID(animal); // Add the animal to the animal manager to store it in the list
                // Set the data context of the animal view to the animal object to display the animal details
                this.DataContext = animal;
                ClearTextBoxes(); // Clear the text boxes
                FillAnimalList(); // Update the list view with the new animal
                UpdateGUI(); // Update the GUI
            }
        }

        /// <summary>
        /// Method that updates the add animal button based on if a species is selected or not
        /// to make sure the user can't add an animal without selecting a species
        /// </summary>
        private void UpdateAddAnimalButton()
        {
            if (listSpecies.SelectedItem == null || animalListView.SelectedItem != null) // Check if a species is selected, if not, disable the add animal button
                btnAddAnimal.IsEnabled = false;
        }

        private void ViewAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the animalInfo window if the data context is not null, extra check in case the button enable state is not updated
            if (this.DataContext != null)
            {
                AnimalInfoWindow animalInfoWindow = new AnimalInfoWindow((Animal)this.DataContext);
                animalInfoWindow.Show();
            }
            else // If the data context is null, display an error message
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
            if (animalListView.SelectedIndex == -1)
                return;
            // Set the add animal button to disabled to prevent users from adding a new animal when an animal is selected
            btnAddAnimal.IsEnabled = false;
            // Get the index of the selected animal in the list view
            int index = animalListView.SelectedIndex;
            // Get the selected animal from the animal manager based on the index
            Animal selectedAnimal = animalManager.GetAt(index);
            if (selectedAnimal != null) // If the selected animal is not null, set the data context of the window to the selected animal
            {
                cmbCategory.SelectedItem = (CategoryType)selectedAnimal.Category; // Manually set the category combo box to the category of the selected animal
                listSpecies.SelectedItem = selectedAnimal.AnimalType; // Manually set the species list view to the species of the selected animal
                this.DataContext = selectedAnimal;
            }
            UpdateGUI(); // Update the GUI
        }

        /// <summary>
        /// Method to sort the list of registered animals by the column header that was clicked. 
        /// The method will sort the list in ascending order if the click count is odd, and in descending order if the click count is even.
        /// A dictionary is used to keep track of the click count for each column header where the key is the property name of the animal object and the value is the click count.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            // Get the coloumn header that was clicked
            GridViewColumnHeader column = (sender as GridViewColumnHeader); // sender is the column header that was clicked and cast it to a GridViewColumnHeader
            string sortBy = column.Tag.ToString(); // Get the tag of the column header, which is the same property name of the animal object
            // Check if the click count dictionary contains the property name
            if (!clickCountDict.ContainsKey(sortBy))
                clickCountDict.Add(sortBy, 0); // If not, add the property name to the dictionary with a click count of 0
            int clickCount = clickCountDict[sortBy]; // Get the click count from the dictionary based on the property name and store it in a variable
            clickCount++; // Increment the click count
            clickCountDict[sortBy] = clickCount; // Set the click count in the dictionary to the new click count
            if (clickCount % 2 == 0) // If the click count is even, sort the list in descending order
                animalManager.SortListDesc(sortBy); // Call the sortlistdesc method to sort the list in descending order based on the property name
            else // If the click count is odd, sort the list in ascending order
                animalManager.SortList(sortBy); // Call the sort list method to sort the list in ascending order based on the property name
            FillAnimalList(); // Update the list view with the sorted list
        }

        /// <summary>
        /// Method to handle the change animal button clicked event, changes the selected animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeAnimalClicked(object sender, RoutedEventArgs e)
        {
            // Get and check the index of the selected animal in the list view
            int index = animalListView.SelectedIndex;
            if (index >= 0)
            {
                Animal newAnimal = ReadInput();
                // Get the selected animal from the data context
                Animal animal = (Animal)this.DataContext;
                // Validate the name and age input
                if (!InputValidator.IsStringValid(txtName.Text))
                    errorList.Add("Name is required");
                if (!InputValidator.IsNumberValid(txtAge.Text))
                    errorList.Add("Age is required and must be a positive number");
                // Check if the animal object is null or if there are any errors
                if (errorList.Count > 0 || newAnimal == null)
                {
                    DisplayErrorMessage();
                }
                else // if the animal is not null and there are no errors, change the animal in the list with the new values
                {
                    newAnimal.Id = animal.Id;
                    animalManager.ChangeAt(index, newAnimal);
                    FillAnimalList();
                    UpdateGUI();
                }
            }
            else
            {
                InputValidator.DisplayErrorMessage("You must select a animal first");
            }
        }

        /// <summary>
        /// Method that deletes the selected animal from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void deleteAnimalClicked(object sender, RoutedEventArgs e)
        {
            // Get the index of the selected animal in the list view
            int index = animalListView.SelectedIndex;
            Animal animal = (Animal)this.DataContext;
            // Check the index and that the animal is not null
            if (index >= 0 && animal != null)
            {
                // Ask the user if they are sure they want to delete the animal
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + animal.Name + "?", "Delete animal", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) // If the user clicks yes, delete the animal
                {
                    animalManager.DeleteAt(index);// Call the delete animal method of the animal manager
                    this.DataContext = null; // Set the data context to null
                    UpdateGUI(); // Update the GUI
                    FillAnimalList();
                }
            }
            else
            {
                InputValidator.DisplayErrorMessage("You must select a animal to delete first");
            }
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new and open the FoodForm window
            FoodForm foodForm = new FoodForm();
            foodForm.ShowDialog();
            // if the user clicks ok, add the food item to the list
            if (foodForm.DialogResult == true)
            {
                foodItemList.Items.Add(foodForm.FoodItem.ToString());
            }
        }

        /// <summary>
        /// Method to add some test animals and food schedules to the list
        /// </summary>
        private void Test()
        {
            // test animal 1
            Animal testAnimal = new Cat(21, true);
            testAnimal.Name = "Pelle";
            testAnimal.Age = 5;
            testAnimal.Color = "Black";
            testAnimal.Gender = GenderType.Female;
            testAnimal.Category = CategoryType.Mammal;
            testAnimal.IsDomesticated = true;
            ((Cat)testAnimal).Breed = "Siamese";
            ((Cat)testAnimal).FavoriteToy = "Mouse";
            ((Cat)testAnimal).IsHouseTrained = true;
            animalManager.AddAnimalWithID(testAnimal);

            // test animal 2
            Animal testAnimal2 = new Dog(22, true);
            testAnimal2.Name = "Doggo";
            testAnimal2.Age = 3;
            testAnimal2.Color = "White";
            testAnimal2.Gender = GenderType.Male;
            testAnimal2.Category = CategoryType.Mammal;
            testAnimal2.IsDomesticated = true;
            ((Dog)testAnimal2).Breed = "Golden Retriever";
            ((Dog)testAnimal2).TailLength = 30;
            ((Dog)testAnimal2).IsSpecialTrained = true;
            ((Dog)testAnimal2).SpecialTrainingType = SpecialTrainingType.Guide;
            animalManager.AddAnimalWithID(testAnimal2);

            // test animal 3
            Animal testAnimal3 = new Penguin(false, false, 23);
            testAnimal3.Name = "Pingu";
            testAnimal3.Age = 2;
            testAnimal3.Color = "Black and white";
            testAnimal3.Category = CategoryType.Bird;
            testAnimal3.Gender = GenderType.Male;
            testAnimal3.IsDomesticated = false;
            ((Penguin)testAnimal3).CanSwim = true;
            ((Penguin)testAnimal3).FavoriteFish = "Herring";
            animalManager.AddAnimalWithID(testAnimal3);

            // food schedule for cats
            FoodSchedule catFoodSchedule = new FoodSchedule();
            catFoodSchedule.ScheduleTitle = "Basic cat food schedule";
            catFoodSchedule.Add("Breakfast: Milk and wet food");
            catFoodSchedule.Add("Lunch: Water and dry food");
            catFoodSchedule.Add("Dinner: Water and wet food");

            // food schedule for dogs
            FoodSchedule dogFoodSchedule = new FoodSchedule();
            dogFoodSchedule.ScheduleTitle = "Basic dog food schedule";
            dogFoodSchedule.Add("Breakfast: Water and dry food");
            dogFoodSchedule.Add("Lunch: Water and wet food");
            dogFoodSchedule.Add("Dinner: Water and dry food");

            // food schedule for carnivores
            FoodSchedule carnivoreFoodSchedule = new FoodSchedule();
            carnivoreFoodSchedule.ScheduleTitle = "Basic carnivore food schedule";
            carnivoreFoodSchedule.Add("Breakfast: Meat");
            carnivoreFoodSchedule.Add("Lunch: Chicken");
            carnivoreFoodSchedule.Add("Dinner: Meat");

            // a food schedule for herbivores
            FoodSchedule herbivoreFoodSchedule = new FoodSchedule();
            herbivoreFoodSchedule.ScheduleTitle = "Basic herbivore food schedule";
            herbivoreFoodSchedule.Add("Breakfast: Water and Grass");
            herbivoreFoodSchedule.Add("Lunch: Water and leaves");
            herbivoreFoodSchedule.Add("Dinner: Grass");

            // a food schedule for omnivores
            FoodSchedule omnivoreFoodSchedule = new FoodSchedule();
            omnivoreFoodSchedule.ScheduleTitle = "Basic omnivore food schedule";
            omnivoreFoodSchedule.Add("Breakfast: Water and meat");
            omnivoreFoodSchedule.Add("Lunch: Water and vegetables");
            omnivoreFoodSchedule.Add("Dinner: Fish and vegetables");
            
            /*
            // Add food schedules to the food schedule dictionary
            foodScheduleManager.AddFoodSchedule(catFoodSchedule);
            foodScheduleManager.AddFoodSchedule(dogFoodSchedule);
            foodScheduleManager.AddFoodSchedule(carnivoreFoodSchedule);
            foodScheduleManager.AddFoodSchedule(herbivoreFoodSchedule);
            foodScheduleManager.AddFoodSchedule(omnivoreFoodSchedule);
            */
        }

        /// <summary>
        /// Method that handles the new animal button clicked event, clears the text boxes and sets the data context to null to be able to add a new animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newAnimalClicked(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            this.DataContext = null;
            animalListView.SelectedIndex = -1;
        }

        /// <summary>
        /// Method to handle the connect food button clicked event, that conncets to fooditem to the animal in a dictionary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectFoodBtn_Clicked(object sender, RoutedEventArgs e)
        {
            // Check that a animal is chosen
            if (this.DataContext == null)
            {
                errorList.Add("You must select a animal first");
            }
            // Check that a food item is chosen
            if (foodItemList.SelectedItem == null)
            {
                errorList.Add("You must select a food item first");
            }
            if (errorList.Count > 0)
            {
                DisplayErrorMessage();
                return;
            }
            // Get the selected animal from the data context
            Animal animal = (Animal)this.DataContext;
            // Get the selected food item from the list view
            string foodItem = foodItemList.SelectedItem.ToString();

            // Check if the animal is already connected to one food item
            bool animalInDict = CheckIfAnimalIsInDict(animal);
            // If it is
            if (animalInDict)
            { // Ask the user if they want to change the food item
                AskToChangeFoodItem(foodItem, animal);
            }
            else // If it is not, add it to the dictionary with the selected food item
            {
                if (AddToFoodItemsDict(foodItem, animal))
                    MessageBox.Show(animal.Name + " connected to " + foodItem);
            }
            UpdateGUI(); // Update the GUI
        }

        /// <summary>
        /// Methood that hanldes adding fooditems and animals to a dictionary
        /// </summary>
        /// <param name="foodItem"></param>
        /// <param name="animal"></param>
        private bool AddToFoodItemsDict(string foodItem, Animal animal)
        {
            bool ok = false;
            if (!foodItemsDict.ContainsKey(foodItem)) // Check if the food item is not already in the dictionary
            {
                // If it is not, create a new list manager and add the animal to the list
                ListManager<Animal> anmList = new ListManager<Animal>();
                anmList.Add(animal);
                foodItemsDict.Add(foodItem, anmList);
                ok = true;
            }
            else // If the food item is already in the dictionary
            {
                // Get the list manager from the dictionary and add the animal to the list
                ListManager<Animal> anmList = foodItemsDict[foodItem];
                anmList.Add(animal);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// MEthod that checks if the selected animal is already connected to a food item
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        private bool CheckIfAnimalIsInDict(Animal animal)
        {
            foreach (KeyValuePair<string, ListManager<Animal>> entry in foodItemsDict)
            {
                ListManager<Animal> anmList = entry.Value;
                if (anmList.Contains(animal))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// MEthod that asks the user if they want to change the food item the animal is connected to
        /// </summary>
        /// <param name="foodItem"></param>
        /// <param name="animal"></param>
        private void AskToChangeFoodItem(string foodItem, Animal animal)
        {
            // Create a message box to ask the user if they want to connect the animal to the fooditem, and thereby removing it from the other fooditem
            MessageBoxResult result = MessageBox.Show("The animal is already connected to a food item, do you want to connect it to this food item instead?", "Connect animal to food item", MessageBoxButton.YesNo);
            // If the user clicks yes, remove the animal from the other food item and connect it to the new food item
            if (result == MessageBoxResult.Yes)
            {
                // Loop through the dictionary
                foreach (KeyValuePair<string, ListManager<Animal>> entry in foodItemsDict)
                {
                    // If the food item is not the same as the selected food item
                    if (entry.Key != foodItem)
                    {
                        // Get the list manager from the dictionary
                        ListManager<Animal> anmList = entry.Value;
                        // Check if the animal is in the list
                        if (anmList.Contains(animal))
                        {
                            // Get the index of the animal in the list
                            int index = anmList.IndexOf(animal);
                            anmList.DeleteAt(index); // If it is, remove the animal from the list
                            AddToFoodItemsDict(foodItem, animal); // Add the animal to the new food item
                            break;
                        }
                    }
                    else // If the food item is the same as the selected food item, let the user know that the animal is already connected to the food item
                    {
                        MessageBox.Show("The animal is already connected to this food item");
                    }
                }
            }
            else // If the user clicks no, do nothing
            {
                return;
            }
        }
        /// <summary>
        /// Method to open a new window with the list of animals connected to the selected food item
        /// </summary>
        private void DisplayAnimalsWithFoodItem()
        {
            // Get the selected food item from the list view
            string foodItem = foodItemList.SelectedItem.ToString();
            // Open a new window with the list of animals connected to the selected food item
            if (foodItemsDict.ContainsKey(foodItem))
            {
                FoodItemWindow foodItemWindow = new FoodItemWindow(foodItemsDict[foodItem]);
                foodItemWindow.Show();
            } else // If the food item is not in the dictionary, display an error message
            {
                MessageBox.Show("No animals connected to this food item");
            }
        }

        /// <summary>
        /// Method to handle the view animals with food item button clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewAnimalsWithFoodItem_Clicked(object sender, RoutedEventArgs e)
        {
            if (foodItemList.SelectedIndex == -1) // Check that a food item is selected
            {
                InputValidator.DisplayErrorMessage("You must select a food item first");
                return;
            }
            DisplayAnimalsWithFoodItem();
        }

        /// <summary>
        /// Method to handle the new food schedule button clicked event, is not implemented yet
        /// </summary>
        /// <param name="foodSchedule"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        private bool AddScheduleToDict(FoodSchedule foodSchedule, Animal animal)
        {
            bool ok = false;
            if (!foodScheduleDict.ContainsKey(foodSchedule)) // Check if the food item is not already in the dictionary
            {
                // If it is not, create a new list manager and add the animal to the list
                ListManager<Animal> anmList = new ListManager<Animal>();
                anmList.Add(animal);
                foodScheduleDict.Add(foodSchedule, anmList);
                ok = true;
            }
            else // If the food item is already in the dictionary
            {
                // Get the list manager from the dictionary and add the animal to the list
                ListManager<Animal> anmList = foodScheduleDict[foodSchedule];
                anmList.Add(animal);
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method to handle when the user clicks the connect schedule and animal button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectScheduleAndAnimal_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.DataContext == null)
            {
                InputValidator.DisplayErrorMessage("You must select a animal first");
                return;
            }
            // Get the animal
            Animal animal = (Animal)this.DataContext;
            // Get the selected food schedule from the combo box
            FoodSchedule foodSchedule; // variable to store the selected food schedule
            if (scheduleComboBox.SelectedItem != null) // check if a food schedule is selected
                // Set the foodschedule variable to the selected food schedule
                foodSchedule = foodScheduleManager.GetSelectedFoodSchedule(scheduleComboBox.SelectedItem.ToString());
            else
            { // If not, display an error message
                InputValidator.DisplayErrorMessage("You must select a food schedule first");
                return;
            }
            // Add the animal to the food schedule dictionary in the food schedule manager
            if (foodScheduleManager.AddAnimalToFoodSchedule(animal, foodSchedule))
                // Display a message to let the user know that the animal is connected to the food schedule if it was successful
                MessageBox.Show(animal.Name + " connected to " + foodSchedule.ToString());
            UpdateGUI(); // update gui
        }

        /// <summary>
        /// Method to populate the combo box with the food schedules
        /// </summary>
        private void PopulateScheduleListCombo()
        {
            scheduleComboBox.Items.Clear();
            // Get the food schedules as a list
            ListManager<FoodSchedule> list = foodScheduleManager.GetFoodScheduleAsList();
            foreach (FoodSchedule foodSchedule in list) // for each list, add the food schedule to the combo box
            {
                scheduleComboBox.Items.Add(foodSchedule.ScheduleTitle);
            }
        }

        /// <summary>
        /// Method to handle when the user selects a food schedule from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fill the list view with the details of the food schedule, eg the strings in the list
            // First get the selected food schedule from the combo box
            if (scheduleComboBox.SelectedItem == null)
                return;
            // Get the food schedule from the combo box
            FoodSchedule selectedSchedule = foodScheduleManager.GetSelectedFoodSchedule(scheduleComboBox.SelectedItem.ToString());

            // Clear the list view
            foodScheduleListBox.Items.Clear();
            // Fill the list view with the food schedule details
            FillFoodScheduleList(selectedSchedule);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|Json files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            { // get the selected file name
                filename = openFileDialog.FileName;
                // if the file is opened successfully, display a message to the user and fill the list view with the animals
                if (OpenFromType(filename))
                {
                    MessageBox.Show("File opened");
                    FillAnimalList();
                    PopulateScheduleListCombo();
                }
                else // else, display an error message
                {
                    MessageBox.Show("Could not open file, please try again or select another file");
                }

            }
        }

        /// <summary>
        /// Method to handle the save file button clicked event, if a filepath is already know it will save to that,
        /// otherwise it will open a save file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            // Save the file to the previously selected file path if it is not null
            if (filename != null)
            {
                SaveToType(filename);
            } // If the file path is null, call the SaveNewFile method to open a save file dialog
            else
            {
                SaveNewFile();
            }
        }

        /// <summary>
        /// Method to handle opening a file based on the file type, either xml or json. Calls the animal manager load methods
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool OpenFromType(string filename)
        {
            CheckForNewAnimals();
            bool ok = false;
            // Check that the file type is xml before trying to load the file
            if (filename.Contains(".xml"))
            {
                if (filename.Contains("-foodschedule.xml"))
                    ok = foodScheduleManager.LoadFromXML(filename);
                
                else
                {
                    List<string> fooditems = UtilitiesLibrary.XmlDeserialize(filename);
                    if (fooditems != null)
                    {
                        foreach (string foodItem in fooditems)
                        {
                            foodItemList.Items.Add(foodItem);
                        }
                        ok = true;
                    }
                }
                   
            }
            // or if the file type is json
            else if (filename.Contains(".json"))
            {
                ok = animalManager.LoadFromJson(filename);
            } // return the result of the load method
            return ok;
        }

        /// <summary>
        /// Method to handle saving a new file, opens a save file dialog and saves the file based on the file type
        /// </summary>
        private void SaveNewFile()
        {
            // Open a save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // Set the filter to only allow xml, json and text files
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|Json files (*.json)|*.json|Text files (*.txt)|*.txt";
            // If the user clicks ok
            if (saveFileDialog.ShowDialog() == true)
            {
                filename = saveFileDialog.FileName; // Set the filename to the selected file path
                SaveToType(filename); // Save the file to the selected file path
            }
        }

        /// <summary>
        /// Method to save the file based on the file type, either json, xml or txt
        /// </summary>
        /// <param name="filename"></param>
        private void SaveToType(string filename)
        {
            bool ok = false;
            // Check if the file type is xml
            if (filename.Contains(".xml"))
            {
                // Save the file as xml
                ok = animalManager.SaveToXML(filename);
            }
            else if (filename.Contains(".txt")) // Check if the file type is txt
            {
                // Save the file as txt
                ok = animalManager.SaveToText(filename);
            }
            else if (filename.Contains(".json")) // Check if the file type is json
            {
                // Save the file as json
                ok = animalManager.SaveToJson(filename);
            }
            else // If the file type is not xml or txt, display an error message
            {
                MessageBox.Show("Invalid file type");
            }
            if (ok) // if the file was saved successfully, display a message to the user
                MessageBox.Show("File saved");
        }

        /// <summary>
        /// Method to handle the save as json menu option clicked event, opens a save file dialog and saves the file as json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsJson_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog and set the filter to only allow json files
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            { // if the user clicks ok, save the file as json
                filename = saveFileDialog.FileName;
                if (animalManager.SaveToJson(filename))
                    MessageBox.Show("File saved");
            }
        }

        /// <summary>
        /// Method that handles when the user clicks on "new" in the menu, checks if there are any unsaved changes and asks the user if they want to save them
        /// before clearing the filename and setting the data context to null and updating the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            CheckForNewAnimals();
            // Clear everything and set the data context to null
            filename = null;
            this.DataContext = null;
            animalManager.DeleteAll();  // clear the animal list
            FillAnimalList();
            UpdateGUI();
        }

        private void SaveAsXML_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog and set the filter to only allow xml files
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            
            if (saveFileDialog.ShowDialog() == true)
            {
                filename = saveFileDialog.FileName;
                List<string> xml = new List<string>();
                foreach (string foodItem in foodItemList.Items)
                {
                    xml.Add(foodItem);
                }
                UtilitiesLibrary.XmlSerialize(filename, xml);
                filename = filename.Replace(".xml", "-foodschedule.xml");
                foodScheduleManager.SaveToXML(filename);
            }
        } 


        /// <summary>
        /// Method that checks if there are animals in the animalmanger and asks the user if they want to save before any other actions are carried out.
        /// </summary>
        private void CheckForNewAnimals()
        {
            if (animalManager.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save before starting on a new file?", "Save", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    SaveFile_Click(this, new RoutedEventArgs());
                }
            }
        }
    }
}
