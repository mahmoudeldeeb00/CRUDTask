using System;
using System.Collections.Generic;

namespace Task.DAl.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string NationalId { get; set; } = null!; 
        public string? Telephone { get; set; }
        public decimal? Salary { get; set; }
        public int VillageId { get; set; }

        public virtual Village Village { get; set; } = null!;
    }
}
