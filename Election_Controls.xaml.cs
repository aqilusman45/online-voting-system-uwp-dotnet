using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Election_Controls : Page
    {
        ObservableCollection<RadioButton> usersList = new ObservableCollection<RadioButton>();
        public string userUid;
        public string startVoting;

        public Election_Controls()
        {

            this.InitializeComponent();
            string query = "SELECT * FROM users_data";

            string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
            MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {


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
                            RadioButton radioButton = new RadioButton();
                            // usersList.Add(new RadioButton() { Content = reader.GetString(1), Name = reader.GetString(0), });
                            radioButton.Name = reader.GetString(0).ToString();
                            radioButton.Content = reader.GetString(1).ToString();
                            radioButton.Checked += new RoutedEventHandler(UserRadioButtonChanged);
                            userList.Children.Add(radioButton);

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
                    catchHandle.Text = ex.Message;
                }


            }
            catch (Exception ex)
            {
                catchHandle.Text = "Invalid crednetials or user may have been deleted";

            }
            return;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            catchHandle.Text = userUid.ToString();
        }

        private void UserRadioButtonChanged(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;


            //   this.userUid = rb..ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

          
                try
                {
                    string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
                    string Query = " UPDATE `admin` SET `startVoting`=" + 1 + " ";
                    MySqlConnection start = new MySqlConnection(MyConnectionString);
                    MySqlCommand StartN = new MySqlCommand(Query, start);
                    StartN.CommandTimeout = 60;
                    start.Open();
                    StartN.ExecuteNonQuery();

                    

                    start.Close();
                    catchHandle.Text = "ELECTION STARTED SUCCESSFULLY";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                }
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

           
                try
                {

                    string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
                    string Query = " UPDATE `admin` SET `startVoting`=" + 0 + " ";
                    MySqlConnection start = new MySqlConnection(MyConnectionString);
                    MySqlCommand StartN = new MySqlCommand(Query, start);
                    StartN.CommandTimeout = 60;
                    start.Open();
                    StartN.ExecuteNonQuery();

                    
                    start.Close();
                    catchHandle.Text = "ELECTION ENDED SUCCESSFULLY";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                }

           

        }

        private void Button_Clickshow(object sender, RoutedEventArgs e)
        {
            try
            {
                string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
                string Query = " UPDATE `admin` SET `show_results`=" + 1 + " ";
                MySqlConnection show = new MySqlConnection(MyConnectionString);
                MySqlCommand sn = new MySqlCommand(Query, show);
                sn.CommandTimeout = 60;
                show.Open();
                sn.ExecuteNonQuery();



                show.Close();
                catchHandle.Text = "Result is declared";
            }
            catch (Exception ex)
            {
                catchHandle.Text = ex.Message.ToString();

            }

        }

        private void Button_Clickhide(object sender, RoutedEventArgs e)
        {
            try
            {
                string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
                string Query = " UPDATE `admin` SET `show_results`=" + 0 + " ";
                MySqlConnection show = new MySqlConnection(MyConnectionString);
                MySqlCommand sn = new MySqlCommand(Query, show);
                sn.CommandTimeout = 60;
                show.Open();
                sn.ExecuteNonQuery();



                show.Close();
                catchHandle.Text = "Result hidden successfully";
            }
            catch (Exception ex)
            {
                catchHandle.Text = ex.Message.ToString();

            }
        }
    }

       
    }

