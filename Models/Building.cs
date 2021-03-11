using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? PhoneId { get; set; }
        public string Address { get; set; }
    }
}
