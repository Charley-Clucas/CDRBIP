using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CDRBIP.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CallDetailRecordController : Controller
    {
        private readonly IMediator _mediator;

        public CallDetailRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetByCallerId")]
        public async Task<IActionResult> GetByCallerId([FromQuery] GetAllByCallerId.Query query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("GetByReference")]
        public async Task<IActionResult> GetByReference([FromQuery] GetByReference.Query query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("GetMostExpensiveCalls")]
        public async Task<IActionResult> GetMostExpensiveCalls([FromQuery] GetMostExpensiveCalls.Query query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("GetCallCountAndDuration")]
        public async Task<IActionResult> GetCallCountAndDuration([FromQuery] GetCallCountAndDuration.Query query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
