using System;
using System.Collections.Generic;

namespace Task.DAl.Entities
{
    public partial class City
    {
        public City()
        {
            Villages = new HashSet<Village>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public byte? Status { get; set; }
        public int GovernateId { get; set; }

        public virtual Governate Governate { get; set; } = null!;
        public virtual ICollection<Village> Villages { get; set; }
    }
}
