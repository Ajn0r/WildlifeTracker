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
using System.Windows.Shapes;
using WildlifeTracker.Birds.Owls;
using WildlifeTracker.Birds.Parrots;
using WildlifeTracker.Birds.Penguins;
using WildlifeTracker.Mammals.Cats;
using WildlifeTracker.Mammals.Dogs;
using WildlifeTracker.Mammals.Donkeys;

namespace WildlifeTracker
{
    /// <summary>
    /// Interaction logic for AnimalInfoWindow.xaml
    /// </summary>
    public partial class AnimalInfoWindow : Window
    {
        public Animal Animal { get; set; } // Property to hold the animal object
        public AnimalInfoWindow(Animal animal)
        {
            InitializeComponent();
            DataContext = this;
            Animal = animal;
            AddAnimalInfo();
        }

        /// <summary>
        /// Method that adds the animal specific attributes to the window,
        /// calls different methods in the species spefic classes based on the animal type to add to information to the stackpanel.
        /// The main stackpanel is sent as a parameter to the methods in the species specific classes and the information
        /// is then added to the main stackpanel. Each animal object is cast to the specific species type before being passed to the methods.
        /// </summary>
        public void AddAnimalInfo()
        {
            // Check that the animal is not null
            if (Animal != null)
            {
                // First set the common attributes of the animal for each category type.
                // Might refactor this later on, but for now, I will add the common attributes to the window here
                SetCategorySpecificInfo();
                // Set the species specific attributes of the animal
                SetSpeciesSpecificInfo();
            }
            else // If the animal is null, display an error message
            {
                MessageBox.Show("Error: No animal selected");
            }
        }

        /// <summary>
        /// Method that sets the species specific attributes of the animal in the information window
        /// </summary>
        private void SetSpeciesSpecificInfo()
        {
            switch (Animal.AnimalType) // Switch based on the animal type
            {
                case "Cat":
                    { // If the animal is a cat, call the AddCatSpecificAttributes method in the Cat class
                        Cat.AddCatSpecificAttributes((Cat)Animal, AnimalInfoStack);
                        break;
                    }
                case "Dog":
                    { // If the animal is a dog, call the AddDogSpecificAttributes method in the Dog class
                        Dog.AddDogSpecificAttributes((Dog)Animal, AnimalInfoStack);
                        break;
                    }
                case "Donkey":
                    { // If the animal is a donkey, call the AddDonkeySpecificAttributes method in the Donkey class
                        Donkey.AddDonkeySpecificAttributes((Donkey)Animal, AnimalInfoStack);
                        break;
                    }
                case "Parrot":
                    { // If the animal is a parrot, call the AddParrotSpecificAttributes method in the Parrot class
                        Parrot.AddParrotSpecificAttributes((Parrot)Animal, AnimalInfoStack);
                        break;
                    }
                case "Penguin":
                    { // If the animal is a penguin, call the AddPenguinSpecificAttributes method in the Penguin class
                        Penguin.AddPenguinSpecificAttributes((Penguin)Animal, AnimalInfoStack);
                        break;
                    }
                case "Owl":
                    { // If the animal is an owl, call the AddOwlSpecificAttributes method in the Owl class
                        Owl.AddOwlSpecificAttributes((Owl)Animal, AnimalInfoStack);
                        break;
                    }
            }
        }

        /// <summary>
        /// Set the common attributes of the animal for each category type
        /// </summary>
        private void SetCategorySpecificInfo()
        {
            if (Animal is Mammal mammal)
            {
                AddAttributeRow(AnimalInfoStack, "Number of teeth: ", mammal.NumOfTeeth.ToString());
                AddAttributeRow(AnimalInfoStack, "Has fur: ", mammal.HasFurOrHair ? "Yes" : "No");
            }
            if (Animal.Category == CategoryType.Mammal)
            {
                // Cast the animal to a mammal object and add the common attributes to the window
                AddAttributeRow(AnimalInfoStack, "Number of teeth: ", ((Mammal)Animal).NumOfTeeth.ToString());
                AddAttributeRow(AnimalInfoStack, "Has fur: ", ((Mammal)Animal).HasFurOrHair ? "Yes" : "No"); // If the animal has fur, display "Yes", otherwise "No"
            }
            else if (Animal.Category == CategoryType.Bird)
            {
                // Cast the animal to a bird object and add the common attributes to the window
                AddAttributeRow(AnimalInfoStack, "Wingspan: ", ((Bird)Animal).WingSpan.ToString());
                AddAttributeRow(AnimalInfoStack, "Can fly: ", ((Bird)Animal).CanFly ? "Yes" : "No"); // If the bird can fly, display "Yes", otherwise "No"
                AddAttributeRow(AnimalInfoStack, "Sings: ", ((Bird)Animal).Sings ? "Yes" : "No");
            }
        }

        /// <summary>
        /// Static method to add a row to the stackpanel with a label and a textblock containing the information
        /// Each subclass/species specific class has a method to add the specific attributes to the window, and since the 
        /// method is static, it can be called without creating an instance of the class. The method takes the main stackpanel,
        /// label name and the text content as parameters and adds a row to the stackpanel with the label and textblock.
        /// I created this method here to avoid repetive code in the species specific classes, all stackpanels are created in the same way.
        /// </summary>
        /// <param name="parentPanel"></param>
        /// <param name="labelContent"></param>
        /// <param name="textContent"></param>
        public static void AddAttributeRow(StackPanel parentPanel, string labelContent, string textContent)
        {
            StackPanel rowStackPanel = new StackPanel(); // Create a new stackpanel for the row
            rowStackPanel.Orientation = Orientation.Horizontal; // Set the orientation to horizontal

            Label label = new Label(); // Create a new label
            label.Content = labelContent; // Set the content of the label
            label.Width = 120; // Set the width of the label

            TextBlock textBlock = new TextBlock(); // Create a new textblock
            textBlock.VerticalAlignment = VerticalAlignment.Center; // Set the vertical and horisontional alignment of the textblock
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Text = textContent; // Set the content of the textblock

            // add the label and textblock to the row stackpanel
            rowStackPanel.Children.Add(label); 
            rowStackPanel.Children.Add(textBlock);

            // add the row stackpanel to the main stackpanel
            parentPanel.Children.Add(rowStackPanel);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }
    }
}
