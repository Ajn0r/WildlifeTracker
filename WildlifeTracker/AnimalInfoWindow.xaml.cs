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
using WildlifeTracker.Mammals.Cats;
using WildlifeTracker.Mammals.Dogs;

namespace WildlifeTracker
{
    /// <summary>
    /// Interaction logic for AnimalInfoWindow.xaml
    /// </summary>
    public partial class AnimalInfoWindow : Window
    {
        public Animal Animal { get; set; }
        public AnimalInfoWindow(Animal animal)
        {
            InitializeComponent();
            DataContext = this;
            Animal = animal;
            AddAnimalInfo();
        }

        // Method to add animal information depending on the type of animal
        public void AddAnimalInfo()
        {
            // Check that the animal is not null
            if (Animal != null)
            {
                switch (Animal.AnimalType)
                {
                    case "Cat":
                        {
                            CatView.AddCatSpecificAttributes((Cat)Animal, AnimalInfoStack);
                            break;
                        }
                    case "Dog":
                        {
                            DogView.AddDogSpecificAttributes((Dog)Animal, AnimalInfoStack);
                            break;
                        }
                }
            }
            else // If the animal is null, display an error message
            {
                MessageBox.Show("Error: There is no infomation to be shown");
            }
            
        }

        public static void AddAttributeRow(StackPanel parentPanel, string labelContent, string textContent)
        {
            StackPanel rowStackPanel = new StackPanel();
            rowStackPanel.Orientation = Orientation.Horizontal;

            Label label = new Label();
            label.Content = labelContent;
            label.Width = 120;

            TextBlock textBlock = new TextBlock();
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Text = textContent;

            rowStackPanel.Children.Add(label);
            rowStackPanel.Children.Add(textBlock);

            parentPanel.Children.Add(rowStackPanel);
        }
        public static StackPanel AddAttributeRow(string labelContent, string textContent)
        {
            StackPanel rowStackPanel = new StackPanel();
            rowStackPanel.Orientation = Orientation.Horizontal;

            Label label = new Label();
            label.Content = labelContent;
            label.Width = 120;

            TextBlock textBlock = new TextBlock();
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Text = textContent;

            rowStackPanel.Children.Add(label);
            rowStackPanel.Children.Add(textBlock);

            return rowStackPanel;
        }
    }
}
