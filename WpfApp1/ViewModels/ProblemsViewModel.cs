using System.Collections.ObjectModel;
using CaissaCRUD.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CaissaApp.ViewModels;

public partial class ProblemsViewModel : ObservableObject
{
    private readonly ICaissaService _service;

    public ObservableCollection<ProblemViewModel> Problems { get; } = new();

    public ProblemsViewModel(ICaissaService service)
    {
        _service = service;
    }

    public Action<ProblemViewModel>? OpenRequested;

    [RelayCommand]
    private void OpenProblem(ProblemViewModel problem)
    {
        OpenRequested?.Invoke(problem);
    }

    [RelayCommand]
    private async Task LoadProblems()
    {


        var data = await _service.GetProblems();

        Problems.Clear();

        foreach (var p in data)
        {
            Problems.Add(new ProblemViewModel
            {
                Fen = p.Fen,
                Stipulation = p.Stipulation,
                Date = DateTime.TryParse(p.Date, out var d) ? d : DateTime.Now,
                Notes = p.Notes
            });
        }
    }
}