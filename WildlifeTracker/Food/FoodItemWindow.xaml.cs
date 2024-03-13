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

namespace WildlifeTracker.Food
{
    /// <summary>
    /// Interaction logic for FoodItemWindow.xaml
    /// </summary>
    public partial class FoodItemWindow : Window
    {
        public FoodItemWindow(ListManager<Animal> animals)
        {
            InitializeComponent();
            StackPanel sp;
            int row = 0;
            foreach (Animal animal in animals)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                
                // for each animal, create a stackpanel with the info to the grid
                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(new TextBlock() { Text = animal.Name });
                sp.Children.Add(new TextBlock() { Text = animal.AnimalType });
                // Set the row of the stackpanel
                Grid.SetRow(sp, row++);
                // Add the stackpanel to the grid

                MainGrid.Children.Add(sp); 

            }

        }
    }
}
