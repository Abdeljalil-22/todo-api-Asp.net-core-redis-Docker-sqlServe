using DemoDocker.Library.Data;
using DemoDocker.Library.Modes;
using DemoDocker.Library.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoDocker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoesController : ControllerBase
    {
        private readonly APIContext _context;
        private readonly ICacheService _cache;

 

        public TodoesController(APIContext context, ICacheService cache)
        {
            _context = context;
            _cache = cache;

        }

        // GET: api/Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            IEnumerable<Todo> list = [];
            var Cachelist = await _cache.GetDataAsync<IEnumerable<Todo>>("list");


            if (!(Cachelist is null))
            {

                list = Cachelist;

            }
            else
            {
                list = await _context.Todos.ToListAsync();
                _cache.SetDataAsync("list", list);
                //var cacheEntryOptions = new DistributedCacheEntryOptions()
                //       .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                //       .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                //_cache.SetStringAsync("list",JsonConvert.SerializeObject( list), cacheEntryOptions);
            }
            return Ok( list);
        }

        // GET: api/Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {


             Todo item ;
            var Cacheitem = await _cache.GetDataAsync<Todo>(id.ToString());
                //await _cache.GetStringAsync(id.ToString());


            if (!(Cacheitem is null) )
            {
                item = Cacheitem;

            }
            else
            {
                item =  await _context.Todos.FindAsync(id);
             _cache.SetDataAsync<Todo>(id.ToString(), item);
              //  _cache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(item), cacheEntryOptions);
            }


           

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        //// PUT: api/Todoes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodo(int id, Todo todo)
        //{
        //    if (id != todo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(todo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Library.Modes.Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        //// DELETE: api/Todoes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodo(int id)
        //{
        //    var todo = await _context.Todo.FindAsync(id);
        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Todo.Remove(todo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
