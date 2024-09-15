


namespace AspNETcore.BSR.Models;

public class Home
{
    public int Id { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int GarageSpots { get; set; }
    public decimal Price { get; set; }
    public int Area { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public static explicit operator Home(AddHomeViewModel v)
    {
        return new Home
        {
            Id = v.Id,
            StreetAddress = v.StreetAddress,
            City = v.City,
            State = v.State,
            Bedrooms = v.Bedrooms,
            Bathrooms = v.Bathrooms,
            GarageSpots = v.GarageSpots,
            Price = v.Price,
            Area = v.Area,
            ImageUrl = v.ImageUrl
        };
    }
}
