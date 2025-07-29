using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Manager;
using TodoListApp.WebApi.Model;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _tagService.GetAllAsync();
            var model = entities.Select(x => new TagModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var entity = await _tagService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new TagModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(TagModel tagModel)
        {
            var entity = new Tag
            {
                Name = tagModel.Name,
            };

            await _tagService.CreateAsync(entity);

            var responseModel = new TagModel
            {
                Name = entity.Name,
            };
            return this.CreatedAtAction(nameof(this.GetTag), new { id = responseModel.Id }, responseModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTag(int id, TagModel tagModel)
        {
                var updateEntity = await _tagService.GetByIdAsync(id);
                if (updateEntity == null)
                {
                    return NotFound();
                }

                updateEntity.Name = tagModel.Name;

                await _tagService.UpdateAsync(updateEntity);
                return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteTag(int id)
        {
            var existing = await this._tagService.GetByIdAsync(id);
            if (existing == null)
            {
                return this.NotFound();
            }

            await this._tagService.DeleteAsync(id);
            return this.NoContent();
        }
    }
}