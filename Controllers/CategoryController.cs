using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories(int categoryId)
        {
            if (!_categoryRepository.CategoriesExists(categoryId))
                return NotFound();
            var category = _mapper.Map<PokemonDto>(_categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var Pokemons = _mapper.Map<List<Pokemon>>(_categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Pokemons);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categorycreate)
        {
            if(categorycreate == null)
                return BadRequest(ModelState);
            var category = _categoryRepository.GetCategories().
                            Where(a=> a.Name.Trim().ToUpper() == categorycreate.Name.Trim().ToUpper()).FirstOrDefault();
            if(category != null)
            {
                ModelState.AddModelError(" ","Category is already exist");
                return StatusCode(422,ModelState);
            }
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var CategoryMap = _mapper.Map<Category>(categorycreate);
            if (_categoryRepository.CreateCategory(CategoryMap))
            {
                ModelState.AddModelError("", "Something went worng while saving category");
                return StatusCode(500,ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
