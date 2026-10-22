using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konkov_prakt7
{
    public class Employe
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Position { get; set; }

        public decimal? Salary { get; set; }

        public DateOnly? HireDate { get; set; }

        public bool? IsActive { get; set; }

        public string? IsActiveStr { get; set; } = "";
    }
}
