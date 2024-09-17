using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OwnerRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o=>o.Id == ownerId).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int PokeId)
        {
            return _context.PokemonOwners.Where(p=>p.Pokemon.Id ==  PokeId).Select(o=> o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p=>p.Owner.Id ==  ownerId).Select(t=>t.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(a=>a.Id == ownerId);
        }
    }
}
