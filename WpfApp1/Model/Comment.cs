using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaissaApp.Model
{
    public class Comment
    {
        public int ProblemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Text { get; set; }
    }
}
