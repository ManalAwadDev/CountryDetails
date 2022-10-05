
namespace CountryDetails.IntermediateModels
{
    public class Country
    {
        public string Name { get; set; }
        public Region Region { get; set; }
        public string CapitalCity { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
