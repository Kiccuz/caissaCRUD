using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaissaApp.Model
{
    using System.Text.Json.Serialization;

    public class ChessProblem
    {
        public int ProblemId { get; set; }
        public string Fen { get; set; }
        public string Stipulation { get; set; }

        [JsonPropertyName("createdAt")]
        public string Date { get; set; }

        [JsonPropertyName("rule")]
        public string Notes { get; set; }
    }
}
