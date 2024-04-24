using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Payment { get; set; }
        public string? Comment { get; set; }
    }
}
