using System;
using System.Collections.Generic;
using System.Security.Claims;
using Legomaster.Models;
using Legomaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Legomaster.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KitController : ControllerBase
  {
    private readonly KitService _service;
    private readonly LegoKitService _legoKitService;
    public KitController(KitService service, LegoKitService legoKitService)
    {
      _service = service;
      _legoKitService = legoKitService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Kit>> Get()
    {
      try
      {
        return Ok(_service.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //GETBYID
    [HttpGet("{Id}")]
    public ActionResult<Kit> Get(int Id)
    {
      try
      {
        return Ok(_service.Get(Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //GETLegosByKitId
    [HttpGet("{kitId}/legos")]
    public ActionResult<IEnumerable<LegoKit>> GetLegosByKitId(int kitId)
    {
      try
      {
        return Ok(_legoKitService.GetLegosByKitId(kitId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //PUT
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Kit> Edit([FromBody] Kit newKit, int id)
    {
      try
      {
        string UserEmail = FindUserEmail();
        newKit.Id = id;
        return Ok(_service.Edit(newKit, UserEmail));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //POST
    [HttpPost]
    [Authorize]
    public ActionResult<Kit> Post([FromBody] Kit newKit)
    {
      try
      {
        newKit.Owner = FindUserEmail();
        return Ok(_service.Create(newKit));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }


    }
    //DEL
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Kit> Delete(int id)
    {
      try
      {
        string UserEmail = FindUserEmail();
        return Ok(_service.Delete(id, UserEmail));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    string FindUserEmail()
    {
      return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
  }
}