using System;
using System.Collections.Generic;

namespace Task.DAl.Entities
{
    public partial class Village
    {
        public Village()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public byte? Status { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
