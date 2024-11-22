using A2Chinook.Data;
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
    /// Interaction logic for CustomerOrders.xaml
    /// </summary>
    public partial class CustomerOrders : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource customerOrdersViewSource;


        public CustomerOrders()
        {
            InitializeComponent();

            customerOrdersViewSource = (CollectionViewSource)FindResource(nameof(customerOrdersViewSource));

            //Load data from all sets
            _context.Customers.Load();
            _context.InvoiceLines.Load();
            _context.Invoices.Load();

            customerOrdersViewSource.Source = _context.Customers.Local.ToObservableCollection();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var query =
                from cust in _context.Customers
                where cust.LastName.Contains(textSearch.Text)
                select new
                {
                    FullName = cust.LastName + " " + cust.FirstName,
                    cust.City,
                    cust.Country,
                    cust.Email,
                    Invoices = (
                        from inv in cust.Invoices
                        orderby inv.InvoiceDate descending
                        select new
                        {
                            inv.InvoiceDate,
                            inv.Total,
                            TotalQuantity = (
                                from line in inv.InvoiceLines
                                select line.Quantity
                            ).Sum()
                        }
                    ).ToList()
                };

            catalogListView.ItemsSource = query.ToList();
        }
    }
}

