using System;
using Legomaster.Models;
using Legomaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace Legomaster.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LegoKitController : ControllerBase
  {
    private readonly LegoKitService _service;
    public LegoKitController(LegoKitService service)
    {
      _service = service;
    }

    //POST
    [HttpPost]
    public ActionResult<DTOLegoKit> Post([FromBody] DTOLegoKit newDTOLegoKit)
    {
      try
      {
        return Ok(_service.Create(newDTOLegoKit));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //DEL
    [HttpDelete("{id}")]
    public ActionResult<DTOLegoKit> Delete(int id)
    {
      try
      {
        return Ok(_service.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}