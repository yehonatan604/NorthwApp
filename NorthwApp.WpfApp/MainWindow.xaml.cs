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
using Microsoft.EntityFrameworkCore;
using NorthwApp.Entities.Models;

namespace NorthwApp.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NorthwindContext db = new();
        public MainWindow()
        {
            InitializeComponent();
            Dgrid.Visibility = Visibility.Collapsed;

            Style style = this.FindResource("BtnStyle") as Style;

            foreach (Button button in Buttons.Children)
            {
                button.Style  = style;
            }

        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            var query = from prd in db.Products
                        select new {prd.ProductName, prd.QuantityPerUnit} ;

            FillGrid(query);
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            var query = from prd in db.Products
                        where prd.Discontinued == false 
                        select new { prd.ProductId, prd.ProductName };

            FillGrid(query);
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            var query = from prd in db.Products
                        where prd.Discontinued == true
                        select new { prd.ProductId, prd.ProductName };

            FillGrid(query);
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            int dis = (from prd in db.Products
                       where prd.Discontinued == false
                       select prd.Discontinued).Count();

            int cur = (from prd in db.Products
                       where prd.Discontinued == true
                       select prd.Discontinued).Count();

            var query = from i in new List<int>() { 890 }
                        select new { discontinued = dis, continued = cur };

            FillGrid(query);
        }

        private void FillGrid(IEnumerable<object> query)
        {
            Dgrid.ItemsSource = query.ToList();
            Dgrid.Visibility = Visibility.Visible;
        }
    }
}
