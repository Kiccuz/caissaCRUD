using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using CaissaApp.Model;
namespace CaissaCRUD.Services
{
    public interface ICaissaService
    {
        Task<List<ChessProblem>> GetProblems();
        Task<List<Comment>> GetComments(int id);
    }

    class CaissaService : ICaissaService
    {
        private readonly HttpClient _http;

        public CaissaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ChessProblem>> GetProblems()
        {

            return await _http.GetFromJsonAsync<List<ChessProblem>>("problems")
                   ?? new();
        }

        public async Task<List<Comment>> GetComments(int id)
        {
 

            return await _http.GetFromJsonAsync<List<Comment>>($"problems/{id}/comments")
                   ?? new();
        }
    }
}
