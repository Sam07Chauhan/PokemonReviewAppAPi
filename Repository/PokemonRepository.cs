using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var PokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var Category = _context.Categories.Where(b=>b.Id == categoryId).FirstOrDefault();
            var PokemonOwner = new PokemonOwner()
            {
                Owner = PokemonOwnerEntity,
                Pokemon = pokemon

            };
            _context.Add(PokemonOwner);
            var PokemonCategory = new PokemonCategory()
            {
                Category = Category,
                Pokemon = pokemon
            };
            _context.Add(PokemonCategory);
            _context.Add(pokemon);
            return Save();
                
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public decimal GetPokemonRating(int PokeId)
        {
            var review = _context.Reviews.Where(p => p.Id == PokeId);
            if(review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating)/review.Count());
            //throw new NotImplementedException();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p=>p.Id).ToList();
        }

        public bool PokemonExists(int PokeId)
        {
            return _context.Pokemons.Any(p => p.Id == PokeId);
            //throw new NotImplementedException();
        }

        public bool Save()
        {
            var Save = _context.SaveChanges();
            return Save > 0 ? true : false;
        }
    }
}
