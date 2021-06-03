using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.Application.Catalog.Translations;
using bookStory.ViewModels.Catalog.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookStory.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TranslationsController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationsController(ITranslationService bookService)
        {
            _translationService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _translationService.GetAll();
            return Ok(items);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageTranslationPagingRequest request)
        {
            var products = await _translationService.GetAllPaging(request);
            return Ok(products);
        }

        //http:/localhost:port/book/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _translationService.GetById(id);
            if (book == null)
                return BadRequest("Cannot find book");
            return Ok(book);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] TranslationCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IdTranslation = await _translationService.Create(request);
            if (IdTranslation == 0)
                return BadRequest();

            var book = await _translationService.GetById(IdTranslation);

            return CreatedAtAction(nameof(GetById), new { id = IdTranslation }, book);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] TranslationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = id;
            var affectedResult = await _translationService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedResult = await _translationService.Delete(id);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }
    }
}