using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.Application.Catalog.Projects;
using bookStory.ViewModels.Catalog.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookStory.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _paragrapService;

        public ProjectsController(IProjectService bookService)
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
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProjectPagingRequest request)
        {
            var products = await _paragrapService.GetAllPaging(request);
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
        public async Task<IActionResult> Create([FromForm] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IdProject = await _paragrapService.Create(request);
            if (IdProject == 0)
                return BadRequest();

            var book = await _paragrapService.GetById(IdProject);

            return CreatedAtAction(nameof(GetById), new { id = IdProject }, book);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ProjectUpdateRequest request)
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