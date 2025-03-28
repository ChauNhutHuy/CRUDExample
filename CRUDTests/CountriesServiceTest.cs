using ServiceContracts;
using ServiceContracts.DTO;
using Xunit;
using Services;
namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTest()
        {
            _countriesService = new CountryService();
        }
        #region AddCountry
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
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllcountries = _countriesService.GetAllCountries();
            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllcountries);
        }
        #endregion
        #region GetAllCountries
        [Fact]
        // The list of countries should be empty by default (before adding any country)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_country_response_list);
        }
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() {CountryName="USA"},
                new CountryAddRequest() {CountryName = "UK"}
            };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

           List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            //read each element from countries_list_from_add_country
            foreach(CountryResponse expected_country in countries_list_from_add_country)
            {
                //check if the expected_country is present in actualCountryResponseList
                Assert.Contains(expected_country, actualCountryResponseList);
            }    
        }
        #endregion

        #region GetCountryByCountryID
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countrID = null;
            //Act
            CountryResponse? actual_country_response = _countriesService.GetCountryByCountryID(countrID);
            //Assert
            Assert.Null(actual_country_response);
        }

        [Fact]
        public void GetCountryByCountryId_ValidCountryID()
        {
            //Arrange
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "Việt Nam" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            //Act 
           CountryResponse? country_response_from_get = _countriesService.GetCountryByCountryID(country_response_from_add.CountryID);

            //Assert
            Assert.Equal(country_response_from_add,country_response_from_get);
        }
        #endregion
    }
} 
