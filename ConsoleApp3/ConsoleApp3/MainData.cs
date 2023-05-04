using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

public class MainData
{
    [JsonProperty("temp")]
    public double Temperature { get; set; }

    [JsonProperty("pressure")]
    public double Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }
}