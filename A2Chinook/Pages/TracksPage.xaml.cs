using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using A2Chinook.Data;


namespace A2Chinook.Pages
{
    /// <summary>
    /// Interaction logic for TracksPage.xaml
    /// </summary>
    public partial class TracksPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource trackViewSource = new CollectionViewSource();
        public TracksPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            trackViewSource = (CollectionViewSource)FindResource(nameof(trackViewSource));

            //use the dbConteext to tell ef to load the data we want to use on this page
            context.Tracks.Load();

            //Set the viewsource data source to use the employees data collection (dbset)
            trackViewSource.Source = context.Tracks.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }
    }
}
