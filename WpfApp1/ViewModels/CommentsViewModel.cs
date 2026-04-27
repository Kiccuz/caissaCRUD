using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaissaCRUD.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CaissaApp.ViewModels
{
    public partial class CommentedProblemViewModel : ObservableObject
    {
        public string Fen { get; set; }
        public string Stipulation { get; set; }

        public ObservableCollection<CommentViewModel> Comments { get; } = new();
    }

    public partial class CommentsViewModel : ObservableObject
    {
        private readonly ICaissaService _service;

        public ObservableCollection<CommentedProblemViewModel> Problems  { get; } = new ();

        public ObservableCollection<CommentViewModel> Comments { get; } = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isGrouped;
 
        public string ViewModeLabel => IsGrouped ? "Vista Flat" : "Vista Raggruppata";

        [RelayCommand]
        private void ToggleView()
        {
            IsGrouped = !IsGrouped;
            OnPropertyChanged(nameof(ViewModeLabel));
        }

        public CommentsViewModel(ICaissaService service)
        {
            _service = service;
        }

        public Action<ProblemViewModel>? OpenRequested;

        [RelayCommand]
        private void OpenProblem(CommentedProblemViewModel problem)
        {
            ProblemViewModel pr = new ProblemViewModel();
            pr.Fen = problem.Fen;
            pr.Stipulation = problem.Stipulation;
            OpenRequested?.Invoke(pr);
        }

        [RelayCommand]
        private async Task LoadComments()
        {
            IsLoading = true;
            try
            {
                Comments.Clear();
                Problems.Clear();

                var problems = await _service.GetProblems();

                if (problems == null || problems.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No problems returned");
                    return;
                }

                foreach (var p in problems)
                {
                    CommentedProblemViewModel pr = new CommentedProblemViewModel();
                    
                    pr.Fen = p.Fen;
                    pr.Stipulation = p.Stipulation;

                    

                    System.Diagnostics.Debug.WriteLine($"Loading comments for ProblemId = {p.ProblemId}");

                    var data = await _service.GetComments(p.ProblemId);



                    if (data == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Null comments for {p.ProblemId}");
                        continue;
                    }

                    foreach (var c in data)
                    {
                        Comments.Add(new CommentViewModel
                        {
                            Id = c.ProblemId,
                            Text = c.Text,
                            CreatedAt = c.CreatedAt
                        });
                        pr.Comments.Add(new CommentViewModel
                        {
                            Id = c.ProblemId,
                            Text = c.Text,
                            CreatedAt = c.CreatedAt
                        });

                    }
                    if (pr.Comments.Count > 0)
                    {
                        Problems.Add(pr);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                
            }
            IsLoading = false;
        }
    }
}
