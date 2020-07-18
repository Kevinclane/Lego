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
  public class LegoController : ControllerBase
  {
    private readonly LegoService _service;
    public LegoController(LegoService service)
    {
      _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Lego>> Get()
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
    public ActionResult<Lego> Get(int Id)
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
    //PUT
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Lego> Edit([FromBody] Lego newLego, int id)
    {
      try
      {
        string UserEmail = FindUserEmail();
        newLego.Id = id;
        return Ok(_service.Edit(newLego, UserEmail));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //POST
    [HttpPost]
    [Authorize]
    public ActionResult<Lego> Post([FromBody] Lego newLego)
    {
      try
      {
        newLego.Owner = FindUserEmail();
        return Ok(_service.Create(newLego));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //DEL
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Lego> Delete(int id)
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