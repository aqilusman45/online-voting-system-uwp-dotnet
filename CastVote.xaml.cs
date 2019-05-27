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




    public sealed partial class CastVote : Page
    {


        ObservableCollection<ComboBoxItem> president1 = new ObservableCollection<ComboBoxItem>();
        ObservableCollection<ComboBoxItem> vpresident = new ObservableCollection<ComboBoxItem>();
        ObservableCollection<ComboBoxItem> msecretary = new ObservableCollection<ComboBoxItem>();
        ObservableCollection<ComboBoxItem> fsecretary = new ObservableCollection<ComboBoxItem>();
        ObservableCollection<ComboBoxItem> treasure = new ObservableCollection<ComboBoxItem>();

        public string society;
        public string president;
        public string vicePresident;
        public string mSecratery;
        public string fSecratery;
        public string treasurar;
        public string startVoting;
        public object reader3;
        public int vote;
        public object readerpresident;
        public object readerpresidentVote;
        public object canVoteReader;
        public object readervicepresident;
        public object readervicepresidentvote;
        public object readermsecratary;
        public object readermsecrataryvote;
        public object readerfsecratary;
        public object readerfsecrataryvote;
        public object readertreasurer;
        public object readertreasurervote;


        string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=users;";

        public CastVote()
        {
            this.InitializeComponent();



            string canvotecheckQuery = "SELECT `canvote` FROM `users_data` WHERE `uid` = '" + App.uid + "'";


            MySqlConnection cantVoteCheck = new MySqlConnection(MyConnectionString);
            MySqlCommand canVote = new MySqlCommand(canvotecheckQuery, cantVoteCheck);
            canVote.CommandTimeout = 60;

            try
            {
                cantVoteCheck.Open();
                canVoteReader = canVote.ExecuteScalar();
                //  catchHandle.Text = canVoteReader.ToString();

                cantVoteCheck.Close();
            }
            catch (Exception ex)
            {
                catchHandle.Text = ex.Message.ToString();
                throw;
            }



            string query = "SELECT * FROM users_data";

            MySqlConnection databaseConnection1 = new MySqlConnection(MyConnectionString);
            string querry3 = "SELECT * FROM admin";
            MySqlCommand commandDatabase3 = new MySqlCommand(querry3, databaseConnection1);
            commandDatabase3.CommandTimeout = 60;

            try
            {
                databaseConnection1.Open();
                reader3 = commandDatabase3.ExecuteScalar();
                startVoting = reader3.ToString();

                if (startVoting == "0")
                {
                    catchHandle.Text = "Admin has not enabled voting yet.";
                }

                databaseConnection1.Close();
            }
            catch (Exception ex)
            {
                catchHandle.Text = ex.Message.ToString();

                throw;
            }




            if (startVoting == "1" && Convert.ToString(canVoteReader) == "1")
            {
                MySqlConnection databaseConnection = new MySqlConnection(MyConnectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader1;


                try
                {
                    databaseConnection.Open();
                    reader1 = commandDatabase.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {



                            if (reader1.GetString(0).ToString() == App.uid.ToString())
                            {

                                this.society = reader1.GetString(3).ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(events));
                    }
                    databaseConnection.Close();

                }
                catch (Exception ex)
                {

                    catchHandle.Text = ex.Message;
                }

                string query2 = "SELECT * FROM " + this.society;
                MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
                commandDatabase2.CommandTimeout = 60;
                MySqlDataReader reader2;


                try
                {
                    databaseConnection.Open();
                    reader2 = commandDatabase2.ExecuteReader();

                    try
                    {

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {

                                if (reader2.GetString(1).ToString() == "President")
                                {

                                    president1.Add(new ComboBoxItem() { Content = reader2.GetString(0) });



                                }
                                else if (reader2.GetString(1).ToString() == "Vice_President")
                                {
                                    vpresident.Add(new ComboBoxItem() { Content = reader2.GetString(0) });

                                }
                                else if (reader2.GetString(1).ToString() == "Treasurer")
                                {
                                    treasure.Add(new ComboBoxItem() { Content = reader2.GetString(0) });

                                }
                                else if (reader2.GetString(1).ToString() == "Male_Secretary")
                                {
                                    msecretary.Add(new ComboBoxItem() { Content = reader2.GetString(0) });

                                }
                                else if (reader2.GetString(1).ToString() == "Female_Secretary")
                                {
                                    fsecretary.Add(new ComboBoxItem() { Content = reader2.GetString(0) });

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        catchHandle.Text = ex.Message.ToString();
                    }

                }
                catch (Exception ex)
                {


                    catchHandle.Text = ex.Message.ToString();
                }


                databaseConnection.Close();
            }
            else
            {

                catchHandle.Text = " You don't have permissions to vote ";
                casteVoteButton.IsEnabled = false;

            }




        }

        private TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> NavigateHome()
        {
            this.Frame.Navigate(typeof(events));
            throw new NotImplementedException();
        }


        private void Button_ClickAsync(object sender, RoutedEventArgs e)
        {


            if (presidentbox.SelectionBoxItem == null || msec.SelectionBoxItem == null || fsec.SelectionBoxItem == null || vpresidentboc == null || tres.SelectionBoxItem == null)
            {
                catchHandle.Text = "No Candidate Selected";
            }
            else
            {


                //President 

                MySqlConnection presidentConnection = new MySqlConnection(MyConnectionString);

                string presidentQuery = "SELECT `candidate_Name` FROM `" + this.society + "` WHERE `candidate_Name` = '" + presidentbox.SelectionBoxItem.ToString() + "'";

                string presidentVote = "SELECT `vote` FROM `" + this.society.ToString() + "` WHERE `candidate_Name` = '" + presidentbox.SelectionBoxItem.ToString() + "'";


                MySqlCommand presidentCommand = new MySqlCommand(presidentQuery, presidentConnection);
                MySqlCommand presidentCommand1 = new MySqlCommand(presidentVote, presidentConnection);

                try
                {
                    presidentConnection.Open();
                    readerpresident = presidentCommand.ExecuteScalar();
                    presidentConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }

                try
                {
                    presidentConnection.Open();
                    readerpresidentVote = presidentCommand1.ExecuteScalar();
                    catchHandle.Text = presidentbox.SelectedValue.ToString();
                    presidentConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }

                this.vote = Convert.ToInt32(readerpresidentVote) + 1;

                try
                {
                    presidentConnection.Open();
                    string presidentQuery2 = "UPDATE `" + this.society.ToString() + "` SET `vote`= '" + (Convert.ToInt32(readerpresidentVote) + 1) + "' WHERE `candidate_Name` = '" + presidentbox.SelectionBoxItem.ToString() + "'";
                    MySqlCommand presidentCommand2 = new MySqlCommand(presidentQuery2, presidentConnection);
                    presidentCommand2.ExecuteNonQuery();
                    // catchHandle.Text = this.vote.ToString();
                    presidentConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                    throw;
                }

                //vicePresident 

                MySqlConnection vicepresidentConnection = new MySqlConnection(MyConnectionString);

                string vicepresidentQuery = "SELECT `candidate_Name` FROM `" + this.society + "` WHERE `candidate_Name` = '" + vpresidentboc.SelectionBoxItem.ToString() + "'";

                string vicepresidentVote = "SELECT `vote` FROM `" + this.society.ToString() + "` WHERE `candidate_Name` = '" + vpresidentboc.SelectionBoxItem.ToString() + "'";


                MySqlCommand vicepresidentCommand = new MySqlCommand(vicepresidentQuery, vicepresidentConnection);
                MySqlCommand vicepresidentCommand1 = new MySqlCommand(vicepresidentVote, vicepresidentConnection);

                try
                {
                    vicepresidentConnection.Open();
                    readervicepresident = vicepresidentCommand.ExecuteScalar();
                    vicepresidentConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }


                try
                {
                    vicepresidentConnection.Open();
                    readervicepresidentvote = vicepresidentCommand1.ExecuteScalar();
                    catchHandle.Text = readervicepresidentvote.ToString();
                    vicepresidentConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }

                this.vote = Convert.ToInt32(readervicepresidentvote) + 1;

                try
                {
                    vicepresidentConnection.Open();
                    string vicepresidentQuery2 = "UPDATE `" + this.society.ToString() + "` SET `vote`= '" + (Convert.ToInt32(readervicepresidentvote) + 1) + "' WHERE `candidate_Name` = '" + vpresidentboc.SelectionBoxItem.ToString() + "'";
                    MySqlCommand vicepresidentCommand2 = new MySqlCommand(vicepresidentQuery2, vicepresidentConnection);
                    vicepresidentCommand2.ExecuteNonQuery();
                    // catchHandle.Text = this.vote.ToString();
                    vicepresidentConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                    throw;
                }

                //Male Secretary

                MySqlConnection msecretaryConnection = new MySqlConnection(MyConnectionString);

                string msecretaryQuery = "SELECT `candidate_Name` FROM `" + this.society + "` WHERE `candidate_Name` = '" + msec.SelectionBoxItem.ToString() + "'";

                string msecretarytVote = "SELECT `vote` FROM `" + this.society.ToString() + "` WHERE `candidate_Name` = '" + msec.SelectionBoxItem.ToString() + "'";


                MySqlCommand msecretaryCommand = new MySqlCommand(msecretaryQuery, msecretaryConnection);
                MySqlCommand msecretaryCommand1 = new MySqlCommand(msecretarytVote, msecretaryConnection);

                try
                {
                    msecretaryConnection.Open();
                    readermsecratary = msecretaryCommand.ExecuteScalar();
                    msecretaryConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }


                try
                {
                    msecretaryConnection.Open();
                    readermsecrataryvote = msecretaryCommand1.ExecuteScalar();
                    // catchHandle.Text = readermsecrataryvote.ToString();
                    msecretaryConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }

                this.vote = Convert.ToInt32(readermsecrataryvote) + 1;

                try
                {
                    msecretaryConnection.Open();
                    string msecQuery2 = "UPDATE `" + this.society.ToString() + "` SET `vote`= '" + (Convert.ToInt32(readermsecrataryvote) + 1) + "' WHERE `candidate_Name` = '" + msec.SelectionBoxItem.ToString() + "'";
                    MySqlCommand msecCommand2 = new MySqlCommand(msecQuery2, msecretaryConnection);
                    msecCommand2.ExecuteNonQuery();
                    // catchHandle.Text = this.vote.ToString();
                    msecretaryConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                    throw;
                }

                //female Secretary

                MySqlConnection fsecretaryConnection = new MySqlConnection(MyConnectionString);

                string fsecretaryQuery = "SELECT `candidate_Name` FROM `" + this.society + "` WHERE `candidate_Name` = '" + fsec.SelectionBoxItem.ToString() + "'";

                string fsecretarytVote = "SELECT `vote` FROM `" + this.society.ToString() + "` WHERE `candidate_Name` = '" + fsec.SelectionBoxItem.ToString() + "'";


                MySqlCommand fsecretaryCommand = new MySqlCommand(fsecretaryQuery, fsecretaryConnection);
                MySqlCommand fsecretaryCommand1 = new MySqlCommand(fsecretarytVote, fsecretaryConnection);

                try
                {
                    fsecretaryConnection.Open();
                    readerfsecratary = fsecretaryCommand.ExecuteScalar();
                    fsecretaryConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }


                try
                {
                    fsecretaryConnection.Open();
                    readerfsecrataryvote = fsecretaryCommand1.ExecuteScalar();
                    // catchHandle.Text = readerfsecrataryvote.ToString();
                    fsecretaryConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }

                this.vote = Convert.ToInt32(readerfsecrataryvote) + 1;

                try
                {
                    fsecretaryConnection.Open();
                    string fsecQuery2 = "UPDATE `" + this.society.ToString() + "` SET `vote`= '" + (Convert.ToInt32(readerfsecrataryvote) + 1) + "' WHERE `candidate_Name` = '" + fsec.SelectionBoxItem.ToString() + "'";
                    MySqlCommand fsecCommand2 = new MySqlCommand(fsecQuery2, fsecretaryConnection);
                    fsecCommand2.ExecuteNonQuery();
                    // catchHandle.Text = this.vote.ToString();
                    fsecretaryConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                    throw;
                }

                //Treasurer

                MySqlConnection treasurerConnection = new MySqlConnection(MyConnectionString);

                string treasurerQuery = "SELECT `candidate_Name` FROM `" + this.society + "` WHERE `candidate_Name` = '" + tres.SelectionBoxItem.ToString() + "'";

                string treasurertVote = "SELECT `vote` FROM `" + this.society.ToString() + "` WHERE `candidate_Name` = '" + tres.SelectionBoxItem.ToString() + "'";


                MySqlCommand treasurerCommand = new MySqlCommand(treasurerQuery, treasurerConnection);
                MySqlCommand treasurerCommand1 = new MySqlCommand(treasurertVote, treasurerConnection);

                try
                {
                    treasurerConnection.Open();
                    readertreasurer = treasurerCommand.ExecuteScalar();
                    treasurerConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                    throw;
                }

                try
                {
                    treasurerConnection.Open();
                    readertreasurervote = treasurerCommand1.ExecuteScalar();
                    // catchHandle.Text = readertreasurervote.ToString();
                    treasurerConnection.Close();

                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();
                }

                this.vote = Convert.ToInt32(readertreasurervote) + 1;

                try
                {
                    treasurerConnection.Open();
                    string fsecQuery2 = "UPDATE `" + this.society.ToString() + "` SET `vote`= '" + (Convert.ToInt32(readertreasurervote) + 1) + "' WHERE `candidate_Name` = '" + tres.SelectionBoxItem.ToString() + "'";
                    MySqlCommand fsecCommand2 = new MySqlCommand(fsecQuery2, treasurerConnection);
                    fsecCommand2.ExecuteNonQuery();
                    // catchHandle.Text = this.vote.ToString();
                    treasurerConnection.Close();
                }
                catch (Exception ex)
                {
                    catchHandle.Text = ex.Message.ToString();

                    throw;
                }



                string disableCanvote = "UPDATE `users_data` SET `canvote`= '0' WHERE `uid` ='"+App.uid+"'";


                MySqlConnection canvoteConnection = new MySqlConnection(MyConnectionString);


                MySqlCommand disableCanVote = new MySqlCommand(disableCanvote, canvoteConnection);




                    try
                {
                    canvoteConnection.Open();
                    disableCanVote.ExecuteNonQuery();
                    canvoteConnection.Close();
                }
                catch (Exception)
                {
                    catchHandle.Text = "Connection Error Please Try Again.";
                    throw;
                }
                casteVoteButton.IsEnabled = false;

            }
        }
    }
}