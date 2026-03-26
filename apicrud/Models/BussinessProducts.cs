using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BussinessProducts
{
    [Key]
    public Guid BussinessProductid { get; set; }

    [Required]
    public string BussinessProductname { get; set; }
    public DateTime? createdat { get; set; }
    
    public DateTime? updatedat { get; set; }

    public bool isactive { get; set; }

    public bool isarchived { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal? SellPrice { get; set; }

    public int? stock { get; set; }

    public string? Producttype { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? productserialnumber { get; set; }
}