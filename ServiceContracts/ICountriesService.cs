using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the contract for the countries service
    /// </summary>
    public interface ICountriesService
    {
       public CountryResponse AddCountry(CountryAddRequest? country);
    }
}
