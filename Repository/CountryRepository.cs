using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public CountryRepository(DataContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        private DataContext _Context;
        private readonly IMapper _mapper;

        public bool CountryExists(int id)
        {
            return _Context.Countries.Any(c => c.Id == id);
            //throw new NotImplementedException();
        }

        public ICollection<Country> GetCountries()
        {
            return _Context.Countries.ToList();
            //throw new NotImplementedException();
        }

        public Country GetCountry(int id)
        {
            return _Context.Countries.Where(c => c.Id == id).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public Country GetCountryByOwner(int OwnerId)
        {
            
            return _Context.Owners.Where(o => o.Id == OwnerId).Select(c => c.Country).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _Context.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool CreateCountry(Country country)
        {
            _Context.Add(country);
            return Save();
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Country GetCountryByOwnerName(string OwnerName)
        {
            if (OwnerName == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                return _Context.Countries.FirstOrDefault(x => x.Name == OwnerName);
            }
            //throw new NotImplementedException();
        }
    }
}
