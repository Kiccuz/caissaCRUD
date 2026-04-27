using System.Collections.ObjectModel;
using System.Windows.Input;
using CaissaCRUD.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CaissaApp.ViewModels;

public partial class ProblemsByStipViewModel : ObservableObject
{
    public ObservableCollection<ProblemViewModel> Problems { get; } = new();
    public String Stip { get; set; }
}

public partial class ProblemsViewModel : ObservableObject
{
    private readonly ICaissaService _service;

    public ObservableCollection<ProblemViewModel> Problems { get; } = new();



    public ObservableCollection<ProblemsByStipViewModel> ProblemsByStip { get; } = new();

    [ObservableProperty]
    private bool isGrouped;
    public string ViewModeLabel => IsGrouped ? "Vista Flat" : "Vista Raggruppata";
    [RelayCommand]
    private void ToggleView()
    {
        IsGrouped = !IsGrouped;
        OnPropertyChanged(nameof(ViewModeLabel));
    }


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

        var stipulations = new SortedDictionary<string, ProblemsByStipViewModel>();

        Problems.Clear();
        ProblemsByStip.Clear();

        foreach (var p in data)
        {
            var pr = new ProblemViewModel
            {
                Fen = p.Fen,
                Stipulation = p.Stipulation,
                Date = DateTime.TryParse(p.Date, out var d) ? d : DateTime.Now,
                Notes = p.Notes
            };

            Problems.Add(pr);

            if (!stipulations.TryGetValue(p.Stipulation, out var group))
            {
                group = new ProblemsByStipViewModel
                {
                    Stip = p.Stipulation
                };

                stipulations[p.Stipulation] = group;
            }

            group.Problems.Add(pr);
        }
        foreach (var g in stipulations.Values)
        {
            ProblemsByStip.Add(g);
        }
    }
}