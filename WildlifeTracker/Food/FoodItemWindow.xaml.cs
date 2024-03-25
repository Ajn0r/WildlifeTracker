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
            FoodItemListView.ItemsSource = animals;
        }

    }
}
