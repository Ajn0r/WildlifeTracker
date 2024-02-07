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

namespace WildlifeTracker.Mammals.Donkeys
{
    /// <summary>
    /// Interaction logic for DonkeyView.xaml
    /// </summary>
    public partial class DonkeyView : UserControl
    {
        public DonkeyView()
        {
            InitializeComponent();
        }

        private void checkBClicked(object sender, RoutedEventArgs e)
        {
            if (chkCarriesLoad.IsChecked == true)
            {
                // make the load weight text box and label visible
                lblMaxLoad.Visibility = Visibility.Visible;
                txtMaxLoad.Visibility = Visibility.Visible;
            }
            else
            {
                // make the load weight text box and label invisible
                lblMaxLoad.Visibility = Visibility.Hidden;
                txtMaxLoad.Visibility = Visibility.Hidden;
                // Also set the value of the text box to empty
                txtMaxLoad.Text = "";
            }
        }
    }
}
