using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CDRBIP.Controllers
{
    [Route("/api/[controller]")]
    public class CallDetailRecordController : Controller
    {
        private readonly IMediator _mediator;

        public CallDetailRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
