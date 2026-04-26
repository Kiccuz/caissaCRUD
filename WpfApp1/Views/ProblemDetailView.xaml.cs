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
using CaissaApp.ViewModels;

namespace CaissaApp.Views
{
    /// <summary>
    /// Interaction logic for ProblemDetailView.xaml
    /// </summary>
    public partial class ProblemDetailView : UserControl
    {
        public ProblemDetailView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProblemDetailViewModel vm)
            {
                await Browser.EnsureCoreWebView2Async();
                Browser.Source = new Uri(vm.Url);
            }
        }
    }
}
