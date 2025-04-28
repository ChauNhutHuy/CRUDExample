using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the contract for the countries service
    /// </summary>
    public interface ICountriesAdderService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the country object after adding it (including newly generated country id)</returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? country);
    }
}
