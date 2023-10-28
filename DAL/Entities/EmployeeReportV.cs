using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("EmployeeReportV")]
public partial class EmployeeReportV
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [Key]
    [Column(Order = 1)]
    [StringLength(14)]
    public string NationalId { get; set; }

    [StringLength(11)]
    public string Telephone { get; set; }

    public decimal? Salary { get; set; }

    [Key]
    [Column(Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VillageId { get; set; }

    [Key]
    [Column(Order = 3)]
    [StringLength(50)]
    public string VillageName { get; set; }

    [Key]
    [Column(Order = 4)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CityId { get; set; }

    [Key]
    [Column(Order = 5)]
    [StringLength(50)]
    public string CityName { get; set; }

    [Key]
    [Column(Order = 6)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int GovernateId { get; set; }

    [Key]
    [Column(Order = 7)]
    [StringLength(50)]
    public string DovernateName { get; set; }
}