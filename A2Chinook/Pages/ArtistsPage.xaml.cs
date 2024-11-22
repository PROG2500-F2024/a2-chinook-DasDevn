using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using A2Chinook.Data;
using System.Linq;
using A2Chinook.Models;

namespace A2Chinook.Pages
{
    /// <summary>
    /// Interaction logic for ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource artistViewSource = new CollectionViewSource();
        public ArtistsPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            artistViewSource = (CollectionViewSource)FindResource(nameof(artistViewSource));

            //use the dbConteext to tell ef to load the data we want to use on this page
            context.Artists.Load();

            //Set the viewsource data source to use the employees data collection (dbset)
            artistViewSource.Source = context.Artists.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query = from artist in context.Artists
                        where artist.Name.Contains(textSearch.Text)
                        orderby artist.ArtistId ascending
            select artist;

       
            ListArtistSearchResults.ItemsSource = query.ToList();

            FullArtistDataGrid.Visibility = Visibility.Collapsed;
            ListArtistSearchResults.Visibility = Visibility.Visible;
        }
    }
}
