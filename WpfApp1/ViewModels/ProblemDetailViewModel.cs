using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CaissaApp.ViewModels;
public partial class ProblemDetailViewModel : ObservableObject
{
    public string Fen { get; }
    public string Stipulation { get; }

    public string Url => $"https://caissarev.xyz/edit?fen={WebUtility.UrlEncode(Fen)}&stipulazione={WebUtility.UrlEncode(Stipulation)}";
 
    public ProblemDetailViewModel(string fen, string stipulation)
    {
        Fen = fen;
        Stipulation = stipulation;
    }
}