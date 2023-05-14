using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GratShiftSaveApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace GratShiftSaveApiController.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class GratShiftController : ControllerBase
  {
    private readonly GratShiftSaveApiContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    public GratShiftController(GratShiftSaveApiContext db, UserManager<IdentityUser> userManager)
    {

      _db = db;
      _userManager = userManager;
    }

    //GET: api/GratShift
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GratShift>>> Get(int cashTip, int creditTip, int shiftSales, DateTime shiftDate, string userId)
    {
      // string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      IdentityUser UserId = await _userManager.FindByIdAsync(userId);
      IQueryable<GratShift> query = _db.GratShifts.AsQueryable();
      //.Where(entry => entry.UserId == userId);

      if (cashTip >= 0)
      {
        query = query.Where(entry => entry.CashTip >= cashTip);
      }

      if (creditTip >= 0)
      {
        query = query.Where(entry => entry.CreditTip >= creditTip);
      }

      if (shiftSales >= 0)
      {
        query = query.Where(entry => entry.ShiftSales >= shiftSales);
      }

      if (shiftDate != default)
      {
        query = query.Where(entry => entry.ShiftDate == shiftDate);
      }

      var response = await query.ToListAsync();
      return Ok(response);
    }

    //Get: api/GratShift/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GratShift>> GetGratShift(int id)
    {
      GratShift gratShift = await _db.GratShifts.FindAsync(id);

      if (gratShift == null)
      {
        return NotFound();
      }

      return Ok(gratShift);
    }

    // POST api/GratShift
    [HttpPost]
    public async Task<IActionResult> Post(GratShift gratShift)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var userIdentity = User.Identity;
      if (userIdentity == null)
      {
        return Unauthorized();
      }
      gratShift.UserId = userIdentity.Name;

      _db.GratShifts.Add(gratShift);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(Get), new { id = gratShift.GratShiftId }, gratShift);
    }

    //PUT: api/GratShift/2
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, GratShift gratShift)
    {
      if (id != gratShift.GratShiftId)
      {
        return BadRequest();
      }

      _db.GratShifts.Update(gratShift);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GratShiftExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

    private bool GratShiftExists(int id)
    {
      return _db.GratShifts.Any(location => location.GratShiftId == id);
    }

    //DELETE: api/GratShift/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGratShift(int id)
    {
      GratShift gratShift = await _db.GratShifts.FindAsync(id);
      if (gratShift == null)
      {
        return NotFound();
      }

      _db.GratShifts.Remove(gratShift);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    // Pagination Code:
    [HttpGet("page/{page}")]
    public async Task<ActionResult<List<GratShift>>> GetPages(int page, int pageSize = 4)
    {
      if (_db.GratShifts == null)
        return NotFound();

      int pageCount = _db.GratShifts.Count();

      var gratShifts = await _db.GratShifts
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      var response = new GratShiftResponse
      {
        GratShifts = gratShifts,
        //page number inside the url
        CurrentPage = page,
        //the amount of parks returned from the database
        Pages = pageCount,
        //amnt of items on the page
        PageSize = pageSize
      };
      return Ok(response);
    }

    // Random GratShift Endpoint
    [HttpGet("random")]
    public async Task<ActionResult<GratShift>> RandomGratShift()
    {
      int gratShifts = await _db.GratShifts.CountAsync();

      if (gratShifts == 0)
      {
        return NotFound();
      }

      var random = new Random();
      int randoShift = random.Next(0, gratShifts);

      GratShift gratShiftRandom = await _db.GratShifts
        .OrderBy(gratShift => gratShift.GratShiftId)
        .Skip(randoShift)
        .FirstOrDefaultAsync();

      return Ok(gratShiftRandom);
    }
  }
}