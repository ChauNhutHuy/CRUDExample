using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the contract for the countries service
    /// </summary>
    public interface ICountriesGetterService
    {
        /// <summary>
        /// Return all countries from the list
        /// </summary>
        /// <returns>All countries from the list as list of CountryResponse</returns>
        Task<List<CountryResponse>> GetAllCountries();
        Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);
    }
}
