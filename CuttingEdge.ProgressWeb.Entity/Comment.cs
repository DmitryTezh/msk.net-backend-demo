using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuttingEdge.ProgressWeb.Entity
{
    public class Comment : Domain
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }
}
