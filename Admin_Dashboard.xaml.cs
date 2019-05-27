using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///  this.InitializeComponent();
          
    public sealed partial class Admin_Dashboard : Page
    {

        public Admin_Dashboard()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;
            MyFrame.Navigate(typeof(events));
            TitleTextBlock.Text = "Societies";
            
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Admin_Dashboard));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             if (Results.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Results));
                TitleTextBlock.Text = "Results";
            }
            else if (Candidate_Form.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Candidate_Form));
                TitleTextBlock.Text = "Add Candidate";
            }
            else if (AccountSettings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(AccountSettings));
                TitleTextBlock.Text = "Account Settings";
            }
            else if (ElectionControl.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Election_Controls));
                TitleTextBlock.Text = "Account Settings";
            }
            else if (Logout.IsSelected)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
