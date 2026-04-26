using CaissaApp.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private object currentView;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentView = new HomeViewModel();
    }

    [RelayCommand]
    private void NavigateHome()
    {
        CurrentView = new HomeViewModel();
    }

    private ProblemsViewModel _problemsVm;

    [RelayCommand]
    private void NavigateProblems()
    {
        if (_problemsVm == null)
        {
            _problemsVm = _serviceProvider.GetRequiredService<ProblemsViewModel>();

            _problemsVm.OpenRequested = OpenProblem;
        }

        CurrentView = _problemsVm;
    }

    private CommentsViewModel _commentsVm;
    [RelayCommand]
    private void NavigateSettings()
    {
        if (_commentsVm == null)
        {
            _commentsVm = _serviceProvider.GetRequiredService<CommentsViewModel>();
            _commentsVm.OpenRequested = OpenProblem;
        }
        CurrentView = _commentsVm;
    }

    [RelayCommand]
    private void OpenProblem(ProblemViewModel problem)
    {
        CurrentView = new ProblemDetailViewModel(problem.Fen, problem.Stipulation);
    }
}