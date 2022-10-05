using AutoMapper;
using CountryDetails.ExternalResources;
using CountryDetails.IntermediateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private static MapperConfigurationExpression _mapper;

        public CountriesController()
        {
            _client = new ExternalApiClient();
            _mapper = new MapperConfigurationExpression();
        }

        // GET: api/Countries
        [HttpGet("{countryCode}")]
        public async Task<ActionResult> GetCountryByCide(string countryCode)
        {

            var countriesMapping = new MapperConfiguration(cfg => cfg.CreateMap<CountryAPIReturn, Country>());

            //_mapper.CreateMap<Message, ErrorMessage>();
            //_mapper.CreateMap<CountryAPIReturn, ErrorMessage>()
            //    .ForPath(dest => dest, x => x.MapFrom(src => src.message.First()));

            var errorMapping = new MapperConfiguration(_mapper);

            var errorMapper = new Mapper(errorMapping);

            var countriesMapper = new Mapper(countriesMapping);

            var countriesResult = await _client.GetCountryDetails(countryCode);

            if (countriesResult.Any(x => Equals(x.message, null))) // no error
            {

                var countries = countriesMapper.Map<List<Country>>(countriesResult);

                return Ok(countries);
            }

            // To Do create mapper for error message
            var error = countriesResult?.First().message;
            
            return Ok(error);
        }
    }
}
