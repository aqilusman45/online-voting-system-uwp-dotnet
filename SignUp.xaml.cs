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
using Firebase.Auth;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public string uid;
        string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBqJIWuT5yhbX9Ra0MVMqMDrL6Buict_QQ"));

            if (email.Text == "" || address.Text == "" || password.Password == "" || society.SelectedItem.ToString() == "" || confirm.Password == "")
            {
                catchHandle.Text = "All feilds are required!";

            }
            else if (password.Password != confirm.Password)
            {
                catchHandle.Text = "Your Passwords don't Match!";


            }
            else
            {
                try
                {
                    FirebaseAuthLink value = await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email.Text, password.Password);
                    this.uid = value.User.LocalId;
                    App.uid = value.User.LocalId;
                    if (this.uid != null)
                    {

                        try
                        {
                            MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);
                            string query = "INSERT INTO users_data(uid,email,studentid,society,address,isadmin) values( '"+uid+"','"+email.Text+"','"+studentid.Text+"','"+society.SelectedValue.ToString()+"','"+address.Text+"','"+0+ "')";
                            databaseConnection.Open();
                            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                            commandDatabase.ExecuteNonQuery();
                            commandDatabase.CommandTimeout = 60;
                            databaseConnection.Close();
                            this.Frame.Navigate(typeof(MainPage));

                        }
                        catch (Exception ex)

                        {
                            
                            //catchHandle.Text = "Database not Responding";
                            catchHandle.Text = ex.Message.ToString();

                        }
                         

                        
                    }

                }
                catch (Exception ex)
                {
                    catchHandle.Text = "Connection Error or User Already Exists";
                }

                return;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}