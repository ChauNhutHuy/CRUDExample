﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Security.Cryptography;

namespace Services
{
    public class CountryService: ICountriesService
    {
        // private field
        private readonly PersonsDbContext _db;
        public CountryService(PersonsDbContext dbContext)
        {
           _db = dbContext;
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            if(_db.Countries.Where(x => x.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exits");
            }    
            //generate CountryID
            country.CountryID = Guid.NewGuid();
            _db.Countries.Add(country);
            _db.SaveChanges();
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
           return _db.Countries.Select(country =>  country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }
            var country_response_from_list = _db.Countries.FirstOrDefault(temp => temp.CountryID == countryID);
            if(country_response_from_list == null)
            {
                return null;
            }
            return country_response_from_list.ToCountryResponse();
        }
    }
}
