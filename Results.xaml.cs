using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMIU_VOTING_SYSTEM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Results : Page
    {
        ObservableCollection<GridViewItem> presidentItems = new ObservableCollection<GridViewItem>();
        ObservableCollection<GridViewItem> vicepresidentItems = new ObservableCollection<GridViewItem>();
        ObservableCollection<GridViewItem> treasurerItems = new ObservableCollection<GridViewItem>();
        ObservableCollection<GridViewItem> fsecItems = new ObservableCollection<GridViewItem>();
        ObservableCollection<GridViewItem> msecItems = new ObservableCollection<GridViewItem>();

        string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";
        public string mysociety;
        public string society;
        public string resultDisplayStatus;
        public object reader3;
        public object reader2;
 

        public Results()
        {


            this.InitializeComponent();

            string resultDisplay = "SELECT `show_results` FROM `admin`";

            MySqlConnection databaseConnection1 = new MySqlConnection(MyConnectionString);
            MySqlCommand commandDatabase1 = new MySqlCommand(resultDisplay, databaseConnection1);
            commandDatabase1.CommandTimeout = 60;


            try
            {
                databaseConnection1.Open();

                reader3 = commandDatabase1.ExecuteScalar();

                this.resultDisplayStatus = reader3.ToString();

                databaseConnection1.Close();

            }
            catch (Exception ex)
            {

                catchHandle.Text = ex.ToString();
            }

            if (this.resultDisplayStatus == "True" )
            {






                string query2 = "SELECT `society` FROM `users_data` WHERE `uid` = '" + App.uid + "'";
                MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query2, databaseConnection);
                commandDatabase.CommandTimeout = 60;

                try
                {
                    databaseConnection.Open();
                    reader2 = commandDatabase.ExecuteScalar();
                    this.society = reader2.ToString();
                    databaseConnection.Close();
                }
                catch (Exception ex)
                {

                    catchHandle.Text = ex.ToString();
                    throw;
                }


                string getResults = "SELECT * FROM `" + this.society + "`";

                MySqlConnection gResults = new MySqlConnection(MyConnectionString);
                MySqlCommand resutsCommand = new MySqlCommand(getResults, gResults);
                MySqlDataReader resultGetter;
                commandDatabase.CommandTimeout = 60;



                try
                {
                    gResults.Open();

                    resultGetter = resutsCommand.ExecuteReader();



                    if (resultGetter.HasRows)
                    {
                        while (resultGetter.Read())
                        {

                            if (resultGetter.GetString(1).ToString() == "President")
                            {


                                //  GridViewItem obj = new GridViewItem();

                                presidentItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()}: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });
                                //                           

                                // presidentsResults.Children.Add(new TextBlock() { Text = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}"  });



                            }
                            else if (resultGetter.GetString(1).ToString() == "Vice_President")
                            {
                                vicepresidentItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Treasurer")
                            {
                                treasurerItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Male_Secretary")
                            {
                                msecItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Female_Secretary")
                            {
                                fsecItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                        }
                    }

                    gResults.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }


            }
            else
            {
                catchHandle.Text = "Please wait for results";
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.resultDisplayStatus == "True")
            {
                this.presidentItems.Clear();
                this.vicepresidentItems.Clear();
                this.treasurerItems.Clear();
                this.fsecItems.Clear();
                this.msecItems.Clear();
                this.mysociety = changeresults.SelectedValue.ToString();


                string getResults = "SELECT * FROM `" + this.mysociety + "`";

                MySqlConnection gResults = new MySqlConnection(MyConnectionString);
                MySqlCommand resutsCommand = new MySqlCommand(getResults, gResults);
                MySqlDataReader resultGetter;
                resutsCommand.CommandTimeout = 60;



                try
                {
                    gResults.Open();

                    resultGetter = resutsCommand.ExecuteReader();



                    if (resultGetter.HasRows)
                    {
                        while (resultGetter.Read())
                        {

                            if (resultGetter.GetString(1).ToString() == "President")
                            {


                                //  GridViewItem obj = new GridViewItem();

                                presidentItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });
                                //                           

                                // presidentsResults.Children.Add(new TextBlock() { Text = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}"  });



                            }
                            else if (resultGetter.GetString(1).ToString() == "Vice_President")
                            {
                                vicepresidentItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Treasurer")
                            {
                                treasurerItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Male_Secretary")
                            {
                                msecItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                            else if (resultGetter.GetString(1).ToString() == "Female_Secretary")
                            {
                                fsecItems.Add(new GridViewItem() { Content = $"{resultGetter.GetString(0).ToString()} votes: {resultGetter.GetString(3).ToString()}", FontSize = 20, Margin = new Thickness() { Left = 20, Bottom = 20, Right = 20, Top = 20 }, Width = 200 });

                            }
                        }
                    }

                    gResults.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }


                try
                {




                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.ToString();
                    throw;
                }


            }
            else
            {

            }

        }
    }
}
