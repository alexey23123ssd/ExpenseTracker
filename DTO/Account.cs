using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Account
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
