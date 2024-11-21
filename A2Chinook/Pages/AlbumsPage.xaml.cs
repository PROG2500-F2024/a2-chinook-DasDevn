using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using A2Chinook.Data;

namespace A2Chinook.Pages
{
    /// <summary>
    /// Interaction logic for AlbumsPage.xaml
    /// </summary>
    public partial class AlbumsPage : Page
    {
        
        ChinookContext context = new ChinookContext();
        CollectionViewSource albumViewSource = new CollectionViewSource();
        public AlbumsPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            albumViewSource = (CollectionViewSource)FindResource(nameof(albumViewSource));

            //use the dbConteext to tell ef to load the data we want to use on this page
            context.Albums.Load();

            //Set the viewsource data source to use the employees data collection (dbset)
            albumViewSource.Source = context.Albums.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Linq
            //Retrieve a single value

            //Define LINQ
            var query = from album in context.Albums
                        where album.Title.Contains(textSearch.Text)
                        orderby album.Title.StartsWith(textSearch.Text) descending, album.Title
                        select album;

            ListAlbumSearchResults.ItemsSource = query.ToList();

            FullAlbumListView.Visibility = Visibility.Collapsed;
            ListAlbumSearchResults.Visibility = Visibility.Visible;
        }
    }
}

