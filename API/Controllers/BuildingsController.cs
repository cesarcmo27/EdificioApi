using Application.Buildings;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class BuildingsController : BaseApiController
    {
      
        [HttpGet]
        public async Task<ActionResult<List<Building>>> GetBuildings()
        {
            return await Mediator.Send(new Application.Buildings.List.Query());
        }

        


    }
}