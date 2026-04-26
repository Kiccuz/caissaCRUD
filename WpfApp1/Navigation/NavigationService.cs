
public interface INavigationService
{
    void Navigate(object viewModel);
}

public class NavigationService : INavigationService
{
    private readonly MainWindowViewModel _shell;

    public NavigationService(MainWindowViewModel shell)
    {
        _shell = shell;
    }

    public void Navigate(object viewModel)
    {
        _shell.CurrentView = viewModel;
    }
}