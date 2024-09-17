using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        Decimal GetPokemonRating(int PokeId);
        bool PokemonExists(int  PokeId);
        bool CreatePokemon(int ownerId,int categoryId,Pokemon pokemon);
        bool Save();
    }
}
