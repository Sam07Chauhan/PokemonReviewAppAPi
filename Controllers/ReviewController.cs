using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReview()
        {
            var Reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Reviews);
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int Id)
        {
            if (!_reviewRepository.ReviewExists(Id))
                return NotFound();
            var Review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(Id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Review);
        }
        [HttpGet("Pokemon/{Id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewOfAPokemon(int Id)
        {
            if (!_reviewRepository.ReviewExists(Id))
                return NotFound();
            
            var Review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewOfAPokemon(Id).ToList());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Review);
        }
    }
}
