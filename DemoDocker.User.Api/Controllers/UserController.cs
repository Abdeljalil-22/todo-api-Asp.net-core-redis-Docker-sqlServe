using DemoDocker.Library.Data;
using DemoDocker.Library.Service;
using DemoDocker.Library.Modes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace DemoDocker.User.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
 [Authorize]
public class UserController : ControllerBase
{

    private readonly APIContext _context;
    private readonly ICacheService _cache;



    public UserController(APIContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;

    }
    // GET: api/User
    [HttpGet]
    public async Task<ActionResult< IEnumerable<DemoDocker.Library.Modes.User>>> Get()
    {
        IEnumerable<Library.Modes.User> list = [];
        var Cachelist = await _cache.GetDataAsync<IEnumerable<Library.Modes.User>>("listUser");


        if (!(Cachelist is null))
        {

            list = Cachelist;

        }
        else
        {
            list = await _context.Users.ToListAsync();
          await  _cache.SetDataAsync("listUser", list);
            //var cacheEntryOptions = new DistributedCacheEntryOptions()
            //       .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //       .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
            //_cache.SetStringAsync("list",JsonConvert.SerializeObject( list), cacheEntryOptions);
        }
        return Ok(list);
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DemoDocker.Library.Modes.User>> Get(int id)
    {
        Library.Modes.User item;
        var Cacheitem = await _cache.GetDataAsync<Library.Modes.User>(id.ToString() +"U");


        if (!(Cacheitem is null))
        {
            item = Cacheitem;

        }
        else
        {
            item = await _context.Users.FindAsync(id);
           await _cache.SetDataAsync<Library.Modes.User>(id.ToString()+"U", item);
        }




        if (item == null)
        {
            return NotFound();
        }

        return item;
       
    }

    // // POST api/<UserController>
    // [HttpPost]
    // public async Task<ActionResult<Library.Modes.User>> PostUser(Library.Modes.User user)
    // {
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction("Get", new { id = user.Id }, user);
    // }


}
