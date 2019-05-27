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
using Firebase.Auth;
using System.Data;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountSettings : Page
    {
        public AccountSettings()
        {

            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {

        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClickAccountSettingsUpdate(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ClickPasswordUpdateAsync(object sender, RoutedEventArgs e)
        {
            var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBqJIWuT5yhbX9Ra0MVMqMDrL6Buict_QQ"));
                var firebase = firebaseAuth.SendPasswordResetEmailAsync(Reset_Password.Text);
            catchHandle.Text = "Password reset email sent please check inbox";

        }
    }
}

