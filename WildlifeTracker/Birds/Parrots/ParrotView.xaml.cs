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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WildlifeTracker.Birds.Parrots
{
    /// <summary>
    /// Interaction logic for ParrotView.xaml
    /// </summary>
    public partial class ParrotView : UserControl
    {
        public ParrotView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to handle the CanTalk check box checked event
        /// Makes the favorite phrase text box and label visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canTalkChecked(object sender, RoutedEventArgs e)
        {
            txtFavvePhrase.Visibility = Visibility.Visible;
            lblFavvePhrase.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method to handle the CanTalk check box unchecked event
        /// Hides the favorite phrase text box and label and clears the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanTalkUnchecked(object sender, RoutedEventArgs e)
        {
            txtFavvePhrase.Visibility = Visibility.Hidden;
            lblFavvePhrase.Visibility = Visibility.Hidden;
            txtFavvePhrase.Text = "";
        }
    }
}
