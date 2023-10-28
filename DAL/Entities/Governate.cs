using System;
using System.Collections.Generic;

namespace Task.DAl.Entities
{
    public partial class Governate
    {
        public Governate()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public byte? Status { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
