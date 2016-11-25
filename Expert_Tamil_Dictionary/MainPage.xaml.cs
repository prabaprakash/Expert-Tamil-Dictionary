using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Windows.Storage;
using SQLite;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Microsoft.Phone.Tasks;
using System.Windows.Documents;
namespace Expert_Tamil_Dictionary
{

    using Newtonsoft.Json;

    public partial class MainPage : PhoneApplicationPage
    {
        int cou = 0;
        string tiled = null;

        #region Share
        private void share(object sender, EventArgs e)
        {
            try
            {
                if (word.Text != "" && word.Text != "Start Search")
                {
                    SmsComposeTask smsComposerTask = new SmsComposeTask();
                    //smsComposerTask.To = "0094771611854";
                    smsComposerTask.Body = word.Text + "-" + tiled;

                    smsComposerTask.Show();
                }
                else
                {
                    MessageBox.Show("Start Search Then Share Meaning as  Message", "Search !!!", MessageBoxButton.OK);
                }
            }
            catch
            {
            }
        }
        private void ShareEmail(object sender, EventArgs e)
        {
            try
            {
                if (word.Text != "" && word.Text != "Start Search")
                {
                    EmailComposeTask emailComposeTask = new EmailComposeTask()
                        {
                            Subject = "Expert Tamil Dictionary",
                            Body = word.Text + "-" + tiled,


                        };
                    //  

                    emailComposeTask.Show();
                }
                else
                {
                    MessageBox.Show("Start Search Then Share Meaning via EMail", "Search !!!", MessageBoxButton.OK);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region OnAppLoad
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (mainpivot.SelectedIndex == 1)
            {
                e.Cancel = true;
                mainpivot.SelectedIndex = 0;
            }
            else if (mainpivot.SelectedIndex == 2)
            {
                e.Cancel = true;
                mainpivot.SelectedIndex = 1;
            }
            else if (mainpivot.SelectedIndex == 0)
            {

            }
        }
        public MainPage()
        {

            InitializeComponent();


            //ApplicationBar.IsVisible = true;
            cou = 0;
            createtable();
            this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(MainPage_OrientationChanged);
            ProjectFile();
        }
        void MainPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if ((e.Orientation == PageOrientation.LandscapeRight) || (e.Orientation == PageOrientation.LandscapeLeft))
            {
                //richtextbox1.Width = 702;
                //richtextbox1.Height = 183;
                //richtextbox1.Visibility = Visibility.Collapsed;

            }
            else if ((e.Orientation == PageOrientation.PortraitDown) || (e.Orientation == PageOrientation.PortraitUp))
            {
                //richtextbox1.Width = 454;
                //richtextbox1.Height = 484;
                //richtextbox1.Visibility = Visibility.Collapsed;

            }
        }
        public List<string> jsonList = new List<string>();

        public string jsonString = null;
        public async void ProjectFile()
        {
            //settings
            try
            {
                var uri = new Uri("ms-appx:///encrypted.db3");
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var destinationFolder = ApplicationData.Current.LocalFolder;
                // var f = await destinationFolder.GetFileAsync("encrypted.db3");
                await file.CopyAsync(destinationFolder);
            }
            catch { }


            try
            {
                var fil = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///dictionary.txt"));
                await fil.CopyAsync(ApplicationData.Current.LocalFolder);
            }
            catch { }


            try
            {
                StorageFile files = await ApplicationData.Current.LocalFolder.GetFileAsync("dictionary.txt");
                using (StreamReader sr = new StreamReader(await files.OpenStreamForReadAsync()))
                {
                    jsonString = await sr.ReadToEndAsync();
                }
                if (jsonString != null)
                {
                    jsonList = JsonConvert.DeserializeObject<List<string>>(jsonString);
                }
            }
            catch
            {
            }

            //try
            //{
            //    var uri = new Uri("ms-appx:///encrypted.db3");
            //    var file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            //    var destinationFolder = ApplicationData.Current.LocalFolder;


            //    // var f = await destinationFolder.GetFileAsync("encrypted.db3");

            //    await file.CopyAsync(destinationFolder);
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Equals("Cannot create a file when that file already exists. (Exception from HRESULT: 0x800700B7)"))
            //    {
            //        textbox2.Text = "";
            //    }
            //    else
            //    {
            //        richtextbox1.Text = ex.Message;
            //    }
            //}
        }
        #endregion

        //protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Orientation == PageOrientation.LandscapeLeft || Orientation == PageOrientation.LandscapeRight)
        //        {

        //            textbox2.Height = 80;
        //            textbox2.Margin = new Thickness(-12, -35, -19, 0);
        //            textbox2.Width = 735;
        //            richtextbox1.Height = 335;
        //            richtextbox1.Margin = new Thickness(-12, 45, -19, 00);
        //            richtextbox1.Width = 735;

        //        }
        //        else if (Orientation == PageOrientation.Portrait)
        //        {



        //            textbox2.Margin = new Thickness(-12, -40, 0, 0);
        //            textbox2.Width = 485;
        //            richtextbox1.Height = 628;
        //            richtextbox1.Margin = new Thickness(-12, 30, -17, 0);
        //            richtextbox1.Width = 485;


        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        //{
        //    // Switch the placement of the buttons based on an orientation change.
        //    if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
        //    {
        //        textbox2.Height=80;
        //        textbox2.Margin=new Thickness(-12,-35,-13,0);
        //        textbox2.Width = 481;
        //    }
        //    // If not in portrait, move buttonList content to visible row and column.
        //    else
        //    {
        //        textbox2.Height=80;
        //        textbox2.Margin=new Thickness(-12,-35,-19,0);
        //        textbox2.Width = 735;
        //       richtextbox1.Height=335;
        //       richtextbox1.Margin = new Thickness(-12, 45, -19, 00);
        //        richtextbox1.Width=735;
        //    }
        //} 


        #region Database
        public void deleteall(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Clear The Recent List", "Recently Searched Words", MessageBoxButton.OKCancel);
            // textbox2.Text = MessageBoxResult.Cancel.ToString();
            if (result == MessageBoxResult.Cancel)
            {
            }
            else
            {

                var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "data.db3");
                using (var db = new SQLite.SQLiteConnection(dbpath))
                {

                    db.DeleteAll<person>();

                }
                list1.Items.Clear();
            }
        }
        private void createtable()
        {
            try
            {
                var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "data.db3");
                using (var db = new SQLite.SQLiteConnection(dbpath))
                {
                    // Create the tables if they don't exist
                    db.CreateTable<person>();
                    db.Commit();

                    db.Dispose();
                    db.Close();
                }

            }
            catch
            {

            }
        }
        public class embed
        {
            [MaxLength(10), PrimaryKey, AutoIncrement]
            public int id { get; set; }
            [MaxLength(255)]
            public String word { get; set; }
            [MaxLength(255)]
            public String meaning { get; set; }

        }
        public class person
        {


            [MaxLength(255), PrimaryKey]
            public String word { get; set; }

        }
        #endregion
        private String wd = "ff";
        // public ObservableCollection<ViewModel> s = new ObservableCollection<ViewModel>(); 
        #region TextChanged
        private void textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                //  s.Clear();
                list.Items.Clear();
                wd = textbox2.Text.ToLower();
                //if (textbox2.Text.Length >= 2)
                //{
                //var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "encrypted.db3");


                //using (SQLiteConnection db = new SQLiteConnection(dbpath))
                //{




                //var query = from x in db.Table<embed>()
                //            where x.word.StartsWith(wd)
                //            select x;
                var query = (from x in jsonList where x.StartsWith(wd) select x).Take(10);
                //int i = 0;
                if (query.Any())
                {
                    foreach (var x in query)
                    {
                        //richtextbox1.Text += "\n\n" + x.meaning.ToString();
                        //if (i == 6)
                        //{
                        //    break;
                        //}

                        //else
                        //{

                        //  s.Add(new ViewModel(){_word = x});
                        list.Items.Add(x);
                    }
                    //list.ItemsSource = s;
                    //listview1.Items.Add(a);
                } //}
                else
                {
                    list.Items.Add("not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //i++;
            //}


            //}

            //}

        }

        #endregion

        #region Selection Changed
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                if (list.SelectedItem != null)
                {
                    if (list.SelectedItem.ToString() != "")
                    {
                        search(list.SelectedItem.ToString().ToLower());
                        mainpivot.SelectedIndex = 1;
                    }
                }


            }
            catch (SQLiteException ex)
            {
                // richtextbox1.Text = "try again";
                MessageBox.Show(ex.StackTrace);
                //  Debug.WriteLine(ex.Message+ex.StackTrace);
            }

        }

        public void search(string add)
        {

            var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "encrypted.db3");

            word.Text = add;

            using (SQLiteConnection db = new SQLiteConnection(dbpath))
            {




                var query = from x in db.Table<embed>()
                            where x.word.ToLower() == add
                            select x;


                foreach (var x in query)
                {



                    tiled = x.meaning;
                }
                fff.Text = tiled;
                //fff.Blocks.Clear();
                //Run myRun1 = new Run();
                //myRun1.Text = x.meaning;
                //Paragraph myParagraph = new Paragraph();
                //myParagraph.Inlines.Add(myRun1);
                //fff.Blocks.Add(myParagraph);
                cou = cou + 1;
                tiles(tiled, cou);
                var rpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db3");
                using (var rdb = new SQLite.SQLiteConnection(rpath))
                {
                    // Create the tables if they don't exist

                    var d = from x in rdb.Table<person>() where x.word.ToLower() == add select x;
                    if (d.Count() == 0)
                    {
                        rdb.Insert(new person() { word = add, });
                    }


                    rdb.Commit();
                    // rdb.Dispose();
                    rdb.Close();

                }
                db.Close();
            }

        }

        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (list1.SelectedItem != null)
                {
                    if (list1.SelectedItem.ToString() != "")
                    {
                        searchlist1(list1.SelectedItem.ToString().ToLower());
                        mainpivot.SelectedIndex = 1;
                    }
                }


            }
            catch (SQLiteException ex)
            {
                // richtextbox1.Text = "try again";
                MessageBox.Show(ex.StackTrace);
                //  Debug.WriteLine(ex.Message+ex.StackTrace);
            }
        }

        private void searchlist1(string add)
        {
            var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "encrypted.db3");

            word.Text = add;
            using (SQLiteConnection db = new SQLiteConnection(dbpath))
            {




                var query = from x in db.Table<embed>()
                            where x.word.ToLower() == add
                            select x;
                //fff.Blocks.Clear();
                //Run myRun1 = new Run();

                //myRun1.Text = x.meaning;
                //Paragraph myParagraph = new Paragraph();
                //myParagraph.Inlines.Add(myRun1);
                //fff.Blocks.Add(myParagraph);
                foreach (var x in query)
                {


                    tiled = x.meaning;
                }
                fff.Text = tiled;
                cou = cou + 1;
                tiles(tiled, cou);

                db.Close();
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {


            //   ApplicationBar.IsVisible = true;
            try
            {
                var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "data.db3");
                using (var db = new SQLite.SQLiteConnection(dbpath))
                {
                    list1.Items.Clear();

                    // var sd = from x in db.Table<person>()  select x;
                    //.Distinct<kural>()
                    if (list1.Items == null)
                    {
                        list1.Items.Add("Recent  List is Empty!!!Start Search");

                    }
                    var d = from x in db.Table<person>() select x;

                    foreach (var sd in d)
                    {
                        list1.Items.Add(sd.word);
                    }

                    db.Close();

                }
            }
            catch
            {
            }
            //search(textbox2.Text.ToString());
        }
        private void textbox2_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {

            //if (e.Key.ToString() == "Back")
            //{
            //    if (textbox2.Text.Length >= 10)
            //    {
            //        txt = 1;
            //    }
            //    else
            //    {
            //        txt = 0;
            //    }

            //}
        }
        #endregion

        #region Tiles
        private void tiles(String sender, int count)
        {
            try
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.First();



                StandardTileData NewTileData = new StandardTileData
                {




                    Title = "தமிழ் அகராதி",
                    // BackgroundImage = new Uri(textBoxBackgroundImage.Text, UriKind.Relative),
                    Count = count,
                    BackTitle = "தமிழ் அகராதி",
                    //BackBackgroundImage = new Uri(textBoxBackBackgroundImage.Text, UriKind.Relative),
                    BackContent = word.Text + "->" + tiled,

                };


                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }
            catch
            {
            }
            // String d="";
            //Random a = new Random();
            //int n = a.Next(1, 55000);
            // var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "encrypted.db3");


            // using (SQLiteConnection db = new SQLiteConnection(dbpath))
            // {



            //     var query = from x in db.Table<embed>()
            //                 where x.id==n
            //                 select x;
            //     foreach (var ge in query)
            //     {
            //         d = ge.word.ToString() + "->" + ge.meaning.ToString();
            //     }



        }
        #endregion

        #region Unknown
        private void rec_Click(object sender, RoutedEventArgs e)
        {

        }
        private void fff_ContentChanged_1(object sender, ContentChangedEventArgs e)
        {

        }
        #endregion

        //public void deleteselected(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var dbpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "data.db3");
        //        using (var db = new SQLite.SQLiteConnection(dbpath))
        //        {
        //            db.Delete<person>(list.SelectedItem.ToString());

        //            var d = from x in db.Table<person>() select x;
        //            list.Items.Clear();
        //            foreach (var sd in d)
        //            {
        //                list.Items.Add(sd.word.ToString());

        //            }
        //            db.Dispose();
        //            db.Close();
        //        }

        //    }
        //    catch
        //    {
        //    }
        //}

        #region About
        public void about(object sender, EventArgs e)
        {
            //ApplicationBar.IsVisible = false;
            this.NavigationService.Navigate(new Uri("/about.xaml", UriKind.Relative));
        }
        public async void get(object sender, EventArgs e)
        {
            string url = @"http://apps.microsoft.com/windows/en-US/app/expert-tamil-dictionary/bb1706f6-8b4a-458d-a825-470d344f6309";

            // Create a Uri object from a URI string 
            var uri = new Uri(url);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        #endregion

        #region Pivot Item Events
        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (word.Text == "")
            {
                word.Text = "Start Search";
            }
        }

        private void Mainpivot_OnLoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            var pivot = (Pivot)sender;

            if (e.Item == pivot.Items[2])
            {
                this.Button_Click_4(sender, new RoutedEventArgs());
            }
        }
        #endregion

        //public void search(object sender, TextChangedEventArgs e)
        //{
        //}
        //public void share(object sender, TextChangedEventArgs e)
        //{

        //}
        //public void help(object sender, TextChangedEventArgs e)
        //{
        //}
        //public void setting(object sender, TextChangedEventArgs e)
        //{
        //}
        //public void menu1(object sender, TextChangedEventArgs e)
        //{
        //}
        //public void menu2(object sender, TextChangedEventArgs e)
        //{
        //}
        //// Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void socialshare(object sender, EventArgs e)
        {

            try
            {
                if (word.Text != "" && word.Text != "Start Search")
                {
                    ShareStatusTask eTask = new ShareStatusTask()
                                                {
                                                    Status =
                                                        "Expert Tamil Dictionary\n" + word.Text + "-"
                                                        + tiled,
                                                };


                    eTask.Show();
                }
                else
                {
                    MessageBox.Show("Start Search Then Share Meaning in Social Networks", "Search !!!", MessageBoxButton.OK);
                }
            }
            catch
            {
            }
        }


    }

}