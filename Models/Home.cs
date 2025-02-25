﻿using System.ComponentModel.DataAnnotations;

namespace AspNETcore.BSR.Models;

public class Home
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Address field is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Area field is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Area must be a positive value.")]
    public int Area { get; set; }

    public string? ImageUrl { get; set; }
}
