using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Konkov_prakt7
{
    public class Employe
    {
        public int Id { get; set; } = 0;

        public string? FullName { get; set; }

        public string? Position { get; set; }

        public decimal? Salary { get; set; }

        public DateOnly? HireDate { get; set; }

        public bool? IsActive { get; set; }

        public string? IsActiveStr { get; set; } = "";
        public DateTime? HireDateDp
        {
            get => HireDate?.ToDateTime(TimeOnly.MinValue);
            set => HireDate = value.HasValue ? DateOnly.FromDateTime(value.Value) : null;
        }
    }
}
