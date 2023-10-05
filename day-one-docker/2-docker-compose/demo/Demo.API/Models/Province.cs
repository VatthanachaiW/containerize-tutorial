namespace Demo.API.Models;

[Serializable]
public class Province
{
    public int ProvinceId { get; set; }
    public required string ProvinceName { get; set; }
    public bool IsActivated { get; set; }
}