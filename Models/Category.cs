using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorunt.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    //2- add new cloumn
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }




    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
