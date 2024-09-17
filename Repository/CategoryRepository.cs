using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context )
        {
            _context = context;
        }
        public bool CategoriesExists(int Id)
        {
            return _context.Categories.Any(c => c.Id == Id);
            //throw new NotImplementedException();
        }

        public bool CreateCategory(Category category)
        {
            //Change tracker
            // add, updating, modfiying
            // connected vs disconnected
          
            _context.Add(category);
            _context.SaveChanges();
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();    
            //throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c=>c.Pokemon).ToList();
            //throw new NotImplementedException();
        }

        public bool Save()
        {
            var Saved = _context.SaveChanges();
            return Saved > 0 ? true : false;
        }
    }
}
