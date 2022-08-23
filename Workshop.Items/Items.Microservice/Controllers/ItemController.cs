using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Items.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ItemController : ControllerBase
    {
        private readonly workshopitemsdbContext _context;
        private readonly IMapper _mapper;

        public ItemController(workshopitemsdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ItemModel>> GetAsync()
        {
            return _mapper.Map<List<ItemModel>>(await _context.Items.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<List<ItemModel>> GetByCollectionAsync(int id)
        {
            return _mapper.Map<List<ItemModel>>(await _context.Items.Where(x => x.ItemCollectionId == id).ToListAsync());
        }

        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
