using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [Range(1, Int32.MaxValue)]
    public decimal UnitPrice { get; set; }
}