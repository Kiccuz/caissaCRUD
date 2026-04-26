using CommunityToolkit.Mvvm.ComponentModel;

namespace CaissaApp.ViewModels;

public partial class ProblemViewModel : ObservableObject
{
    [ObservableProperty]
    private string fen;

    [ObservableProperty]
    private string stipulation;

    [ObservableProperty]
    private DateTime date;

    [ObservableProperty]
    private string notes;
}