using System.Windows;
using CaissaApp.ViewModels;
using CaissaApp.Views;
using CaissaCRUD.Services;
using Microsoft.Extensions.DependencyInjection;
using CaissaApp;

namespace CaissaApp
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var services = new ServiceCollection();
                services.AddHttpClient<ICaissaService, CaissaService>(client =>
                {
                    client.BaseAddress = new Uri("https://ex-clibackend-1.onrender.com/");
                });

                services.AddSingleton<MainWindow>();

                services.AddSingleton<MainWindowViewModel>();

                services.AddTransient<ProblemsViewModel>();

                services.AddTransient<CommentsViewModel>();

                ServiceProvider = services.BuildServiceProvider();

                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();

                mainWindow.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            base.OnStartup(e);

        }

    }

}
