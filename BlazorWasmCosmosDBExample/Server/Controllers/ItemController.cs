using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorWasmCosmosDBExample.Shared.Models;
using BlazorWasmCosmosDBExample.Server.Services;
using BlazorWasmCosmosDBExample.Shared;
using System.Diagnostics;

namespace BlazorWasmCosmosDBExample.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        //private static readonly string CollectionId = "Item";
        private readonly ICosmosDbService _cosmosDbService;
        public ItemController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            var result = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            return result;

        }

        //[HttpGet("{itemId}")]
        //public async Task<IEnumerable<Item>> GetSingleItem([FromRoute] string itemId)
        //{
        //    var result = await _cosmosDbService.GetItemsAsync($"SELECT * FROM c WHERE c.id ='{itemId}'");
        //    return result;

        //}

        [HttpGet("{itemId}")]
        public async Task<Item> GetSingleItem([FromRoute] string itemId)
        {

            var result = await _cosmosDbService.GetItemAsync(itemId);
            return result;

        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync([FromBody] Item item)
        {

            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(item);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult> EditAsync([FromBody] Item item)
        {

            await _cosmosDbService.UpdateItemAsync(item.Id, item);

            return Ok();

        }

        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteAsync([FromBody] string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Item item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _cosmosDbService.DeleteItemAsync(id);

            return Ok();
        }

        //[HttpGet]
        //public async Task<IEnumerable<Item>> Get()
        //{
        //    var result = await CosmosDBRepository<Item>.GetItemsAsync(CollectionId);
        //    return result;

        //}

        //[ActionName("Index")]
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        //}

        //[ActionName("Create")]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateAsync([Bind("Id,Name,Description,Completed")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        item.Id = Guid.NewGuid().ToString();
        //        await _cosmosDbService.AddItemAsync(item);
        //        return RedirectToAction("Index");
        //    }

        //    return View(item);
        //}

        //[HttpPost]
        //[ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditAsync([Bind("Id,Name,Description,Completed")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _cosmosDbService.UpdateItemAsync(item.Id, item);
        //        return RedirectToAction("Index");
        //    }

        //    return View(item);
        //}

        //[ActionName("Edit")]
        //public async Task<ActionResult> EditAsync(string id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }

        //    Item item = await _cosmosDbService.GetItemAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(item);
        //}

        //[ActionName("Delete")]
        //public async Task<ActionResult> DeleteAsync(string id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }

        //    Item item = await _cosmosDbService.GetItemAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(item);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        //{
        //    await _cosmosDbService.DeleteItemAsync(id);
        //    return RedirectToAction("Index");
        //}

        //[ActionName("Details")]
        //public async Task<ActionResult> DetailsAsync(string id)
        //{
        //    return View(await _cosmosDbService.GetItemAsync(id));
        //}
    }
}
