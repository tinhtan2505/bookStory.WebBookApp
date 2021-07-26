using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.Application.Catalog.Ratings;
using bookStory.ViewModels.Catalog.Ratings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookStory.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _paragrapService;

        public RatingsController(IRatingService bookService)
        {
            _paragrapService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _paragrapService.GetAll();
            return Ok(items);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageRatingPagingRequest request)
        {
            var products = await _paragrapService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{keywordUserId}/{keywordIdTranslation}")]
        public async Task<IActionResult> GetRating(Guid keywordUserId, int keywordIdTranslation)
        {
            var products = await _paragrapService.GetRating(keywordUserId, keywordIdTranslation);
            if (products == null)
                return BadRequest("Cannot find book");
            return Ok(products);
        }

        //http:/localhost:port/book/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _paragrapService.GetById(id);
            if (book == null)
                return BadRequest("Cannot find book");
            return Ok(book);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] RatingCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IdRating = await _paragrapService.Create(request);
            if (IdRating == 0)
                return BadRequest();

            var book = await _paragrapService.GetById(IdRating);

            return CreatedAtAction(nameof(GetById), new { id = IdRating }, book);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] RatingUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = id;
            var affectedResult = await _paragrapService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedResult = await _paragrapService.Delete(id);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }
    }
}