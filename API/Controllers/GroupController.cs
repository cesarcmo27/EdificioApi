using System.Text.Json;
using Application.Groups;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GroupController : BaseApiController
    {
        private readonly ILogger<GroupController> _logger;

        public GroupController(ILogger<GroupController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetGroups()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

         [HttpPost]
        public async Task<ActionResult> CreateGroup(Group group)
        {
            return HandleResult(await Mediator.Send(new Create.Command{Group = group}));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> EditGroup(Guid id,Group group)
        {
            group.Id = id;
             _logger.LogInformation("inicio de editar");
            _logger.LogInformation("data = > "+ JsonSerializer.Serialize( group));
            return HandleResult(await Mediator.Send(new Edit.Command{Group = group}));
        }
    }
}