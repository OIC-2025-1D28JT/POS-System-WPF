using System.Windows;
using System.Windows.Media;

namespace POSS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PosPage());
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new PosPage());
        private void btnHistory_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new SalesHistoryPage());
        private void btnProduct_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new ProductPage());
    }
}