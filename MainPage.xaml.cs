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
using System.Timers;
using Firebase.Auth;
using System.Data;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class MainPage : Page
    {
        string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
        private Timer time = new Timer();
        public string iadmin;
        public string uid;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUp));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        //  private async void Button_Click(object sender, RoutedEventArgs e)
        private async void Button_Click(object sender, RoutedEventArgs e)

        {
            string query = "SELECT * FROM users_data";

            MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {

                var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBqJIWuT5yhbX9Ra0MVMqMDrL6Buict_QQ"));
                FirebaseAuthLink value = await firebaseAuth.SignInWithEmailAndPasswordAsync(email.Text, password.Password);
                this.uid = value.User.LocalId;

                App.uid = this.uid;

                try
                {
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();
                    // Success, now list 

                    // If there are available rows
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (
                                reader.GetString(0).ToString() == this.uid
                                && reader.GetString(5).ToString() == "True")
                            {
                               this.Frame.Navigate(typeof(Admin_Dashboard));
                                //catchHandle.Text = reader.GetString(5).ToString();
                                break;
                            }
                            else
                            {
                                // this.Frame.Navigate(typeof(Dashboard));
                                this.Frame.Navigate(typeof(Dashboard));

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }

                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text =ex.Message;
                }


            }
            catch (Exception ex)
            {
                catchHandle.Text = "Invalid crednetials or user may have been deleted";

            }
            return;
        }


        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}