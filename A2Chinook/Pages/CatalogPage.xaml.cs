using A2Chinook.Data;
using A2Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Chinook.Pages
{
    /// <summary>
    /// Interaction logic for CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource catalogViewSource;

        public CatalogPage()
        {
            InitializeComponent();
            catalogViewSource = (CollectionViewSource)FindResource(nameof(catalogViewSource));

            //Load Data from BOTH sets
            _context.Artists.Load();
            _context.Albums.Load();

            catalogViewSource.Source = _context.Artists.Local.ToObservableCollection();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Define a grouping query to get grouped category data
            var query =
                from artist in _context.Artists
                where artist.Name.Contains(textSearch.Text)
                group artist by artist.Name.ToUpper().Substring(0, 1) into artistGroup 
                select new
                {
                    Index = artistGroup.Key,
                    ArtistCount = artistGroup.Count().ToString(),
                    artist = artistGroup.ToList<Artist>()
                    
                };

            // Execute query and bind to the ListView
            catalogListView.ItemsSource = query.ToList();
        }
    }
}
