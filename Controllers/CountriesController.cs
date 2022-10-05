using AutoMapper;
using CountryDetails.ExternalResources;
using CountryDetails.IntermediateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDetails.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        ExternalApiClient _client;
        
        public CountriesController()
        {
            _client = new ExternalApiClient();
        }

        // GET: api/Countries
        [HttpGet("{countryCode}")]
        public async Task<IEnumerable<Country>> GetCountryByCide(string countryCode)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CountryAPIReturn, Country>());
            var mapper = new Mapper(config);

            var countriesResult = await _client.GetCountryDetails(countryCode);
            var countries = mapper.Map<List<Country>>(countriesResult);

            return countries;
        }
    }
}
