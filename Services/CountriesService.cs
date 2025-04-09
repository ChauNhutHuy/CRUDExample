using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Security.Cryptography;

namespace Services
{
    public class CountriesService: ICountriesService
    {
        // private field
        private readonly ICountriesRepository _countriesRepository;
        public CountriesService(ICountriesRepository countriesRepository)
        {
           _countriesRepository = countriesRepository;
        }
        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validation: countryAddRequest isn't null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }    
            Country country = countryAddRequest.ToCountry();

            // validation: CountryName can't be duplicate
            if(await _countriesRepository.GetCountryByCountryID(country.CountryID) != null)
            {
                throw new ArgumentException("Given country name already exits");
            }    
            //generate CountryID
            country.CountryID = Guid.NewGuid();
            await _countriesRepository.AddCountry(country);
           
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
           var countries = await _countriesRepository.GetAllCountries();
           
            return countries.Select(x => x.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }
            var country_response_from_list = await _countriesRepository.GetCountryByCountryID(countryID.Value);
            if(country_response_from_list == null)
            {
                return null;
            }
            return country_response_from_list.ToCountryResponse();
        }
    }
}
