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
using WildlifeTracker.Helper_classes;

namespace WildlifeTracker.Food
{
    /// <summary>
    /// Interaction logic for FoodForm.xaml
    /// </summary>
    public partial class FoodForm : Window
    {

        public FoodItem FoodItem { get; set; }
        public FoodForm()
        {
            InitializeComponent();
            FoodItem = new FoodItem();
        }

        private void UpdateGUI()
        {
            txtIngredient.Text = "";
            lstIngredients.Items.Clear();
            foreach (string ingredient in FoodItem.Ingredients.ToStringList())
            {
                lstIngredients.Items.Add(ingredient);
            }
        }


        private void addbtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.IsStringValid(txtIngredient.Text)) // check if the ingredient is empty
            {
                InputValidator.DisplayErrorMessage("Ingredient name cannot be empty");
                return;
            }
            FoodItem.Ingredients.Add(txtIngredient.Text.ToString());
            UpdateGUI();
            // Set the cursor to the ingredient textbox
            txtIngredient.Focus();
        }

        private void changebtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.IsStringValid(txtIngredient.Text)) // check if the ingredient is empty
            {
                InputValidator.DisplayErrorMessage("Ingredient name cannot be empty");
                return;
            }

            if (!FoodItem.Ingredients.ChangeAt(lstIngredients.SelectedIndex, txtIngredient.Text))
            {
                InputValidator.DisplayErrorMessage("Please select an ingredient to change");
                return;
            }   
            UpdateGUI();
        }

        private void removeBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!FoodItem.Ingredients.DeleteAt(lstIngredients.SelectedIndex))
            {
                InputValidator.DisplayErrorMessage("Please select an ingredient to remove");
                return;
            }
            UpdateGUI();
        }

        private void okBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (InputValidator.IsStringValid(txtName.Text)) 
            {
                FoodItem.Name = txtName.Text;
                this.DialogResult = true;
            }
            else
            {
                InputValidator.DisplayErrorMessage("Please enter a valid name");
            }
            

        }

        private void cancelBtn_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
