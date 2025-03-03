using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTest(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }  
        //when countries are requested, the service should return a list of countries
        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest countryAddReques = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(countryAddReques);
            });
        }
        // when countryName is null, the service should throw an exception
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrrange
            CountryAddRequest countryAddReques = new CountryAddRequest()
            {
                CountryName = null
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(countryAddReques);
            });
        }
        //When the countryName is duplicated, the service should throw an exception
        [Fact]
        public void AddCountry_CountryNameIsDuplicated()
        {
            //Arrange
            CountryAddRequest request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        //when you supply proper countryName, the service should return the country
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);
            //Assert
            Assert.True(response.CountryID != Guid.Empty);
        }
    }
} 
