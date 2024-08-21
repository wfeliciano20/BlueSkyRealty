using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNETcore.BSR.Models;

public class CitiesResponse
{
    public bool Error { get; set; }
    public string Msg { get; set; }
    public List<string> Data { get; set; }
}

public class CityRequest
{
    public string State { get; set; }
}
