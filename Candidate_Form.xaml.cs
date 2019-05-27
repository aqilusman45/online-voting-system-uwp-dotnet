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
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Candidate_Form : Page
    {
        string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";

        public Candidate_Form()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);

            if (society.SelectedValue.ToString() == "arts")
            {
                try
                {
                    string query = "INSERT INTO arts(candidate_Name,Designation,society) values( '" + name.Text + "','" + Designation.SelectedValue.ToString() + "','" + society.SelectedValue.ToString() + "')";
                    databaseConnection.Open();
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.ExecuteNonQuery();
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Close();
                    catchHandle.Text = "Candidate Registered";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }
            }
            else if (society.SelectedValue.ToString() == "community")
            {


                try
                {
                    string query = "INSERT INTO community(candidate_Name,Designation,society) values( '" + name.Text + "','" + Designation.SelectedValue.ToString() + "','" + society.SelectedValue.ToString() + "')";
                    databaseConnection.Open();
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.ExecuteNonQuery();
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Close();
                    catchHandle.Text = "Candidate Registered";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }
            }
            else if (society.SelectedValue.ToString() == "literary" )
            {


                try
                {
                    string query = "INSERT INTO literary (candidate_Name,Designation,society) values( '" + name.Text + "','" + Designation.SelectedValue.ToString() + "','" + society.SelectedValue.ToString() + "')";
                    databaseConnection.Open();
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.ExecuteNonQuery();
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Close();
                    catchHandle.Text = "Candidate Registered";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }
            }
            else if (society.SelectedValue.ToString() == "sports" )
            {


                try
                {
                    string query = "INSERT INTO sports (candidate_Name,Designation,society) values( '" + name.Text + "','" + Designation.SelectedValue.ToString() + "','" + society.SelectedValue.ToString() + "')";
                    databaseConnection.Open();
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.ExecuteNonQuery();
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Close();
                    catchHandle.Text = "Candidate Registered";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }
            }

            else if (society.SelectedValue.ToString() == "science")
            {


                try
                {
                    string query = "INSERT INTO science (candidate_Name,Designation,society) values( '" + name.Text + "','" + Designation.SelectedValue.ToString() + "','" + society.SelectedValue.ToString() + "')";
                    databaseConnection.Open();
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.ExecuteNonQuery();
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Close();
                    catchHandle.Text = "Candidate Registered";
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }
            }

            else
            {

                catchHandle.Text = "Plz enter the field from the given list (arts,community,literary,sports)";
            }
               

            }
        }
    
}
