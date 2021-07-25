using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.Application.Catalog.Books;
using bookStory.ViewModels.Catalog.BookImages;
using bookStory.ViewModels.Catalog.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookStory.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            //_publicBookService = publicBookService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageBookPagingRequest request)
        {
            var products = await _bookService.GetAllPaging(request);
            return Ok(products);
        }

        //http:/localhost:port/book/id
        [HttpGet("{IdBook}")]
        public async Task<IActionResult> GetById(int IdBook)
        {
            var book = await _bookService.GetById(IdBook);
            if (book == null)
                return BadRequest("Cannot find book");
            return Ok(book);
        }

        [HttpGet("featured/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTops(int take)
        {
            var products = await _bookService.GetTops(take);
            return Ok(products);
        }

        [HttpGet("latest/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProducts(int take)
        {
            var products = await _bookService.GetLatestProducts(take);
            return Ok(products);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BookCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IdBook = await _bookService.Create(request);
            if (IdBook == 0)
                return BadRequest();

            var book = await _bookService.GetById(IdBook);

            return CreatedAtAction(nameof(GetById), new { id = IdBook }, book);
        }

        [HttpPut("{IdBook}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int IdBook, [FromForm] BookUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = IdBook;
            var affectedResult = await _bookService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{IdBook}")]
        [Authorize]
        public async Task<IActionResult> Delete(int IdBook)
        {
            var affectedResult = await _bookService.Delete(IdBook);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        //Images
        [HttpPost("{IdBook}/images")]
        public async Task<IActionResult> CreateImage(int IdBook, [FromForm] BookImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _bookService.AddImage(IdBook, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _bookService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{IdBook}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] BookImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{IdBook}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{IdBook}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int IdBook, int imageId)
        {
            var image = await _bookService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }
    }
}