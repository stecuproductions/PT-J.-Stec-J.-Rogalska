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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Logika interakcji dla klasy ConfirmDelete.xaml
    /// </summary>
    public partial class ConfirmDelete : Window
    {
        public bool confirmed { get; private set; } = false;
        public ConfirmDelete(string title, string author)
        {
            InitializeComponent();
            MessageTextBlock.Text = $"Are you sure you want to remove: \n\"{title}\" by {author}?";
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            confirmed = true;
            this.Close();
        }
    }
}
