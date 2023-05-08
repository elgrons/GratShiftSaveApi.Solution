using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GratShiftSaveApi.Models;
using System;

namespace GratShiftSaveApiController.Controllers

{
  [Route("api/[controller]")]
  [ApiController]
  public class GratShiftController : ControllerBase
  {
    private readonly GratShiftSaveApiContext _db;

    public GratShiftController(GratShiftSaveApiContext db)
    {
      _db = db;
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
	
    [HttpGet]
    public async Task<List<GratShift>> Get( int cashTip, int creditTip, int shiftSales, DateTime shiftDate)
    {
      
      IQueryable<GratShift> query = _db.GratShifts.AsQueryable();

      if (cashTip >= 0)
      {
        query = query.Where(entry => entry.CashTip == cashTip);
      }

      if (creditTip >= 0)
      {
        query = query.Where(entry => entry.CreditTip == creditTip);
      }

      if (shiftSales >= 0)
      {
        query = query.Where(entry => entry.ShiftSales == shiftSales);
      }

      if (shiftDate != null)
      {
        query = query.Where(entry => entry.ShiftDate >= shiftDate);
      }

      return await query.ToListAsync();
    }

    //Get: api/GratShifts/1
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

    //POST api/GratShifts
    [HttpPost]
    public async Task<ActionResult<GratShift>> Post(GratShift gratShift)
    {
      _db.GratShifts.Add(gratShift);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetGratShift), new { id = gratShift.GratShiftId }, gratShift);
    }

    //PUT: api/GratShifts/2
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

    //DELETE: api/GratShifts/5
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