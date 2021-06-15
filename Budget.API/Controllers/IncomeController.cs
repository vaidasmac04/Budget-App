using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Budget.Application.Incomes;
using Budget.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Budget.API.Controllers
{
    [Authorize]
    public class IncomeController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public IncomeController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes([FromQuery]GetIncomesQueryParam param)
        {
            try
            {
                return await _mediator.Send(new GetIncomesQuery { Param = param });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeDTO>> GetIncome(int id)
        {
            var income = await _mediator.Send(new GetIncomeQuery { Id = id });

            if(income == null)
            {
                return NotFound();
            }

            return income;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, [FromBody]IncomeDTO incomeDTO)
        {
            if (id != incomeDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new UpdateIncomeCommand { IncomeDTO = incomeDTO });
                return Ok();
            }
            catch(ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome([FromBody]IncomeDTO incomeDTO)
        {
            await _mediator.Send(new CreateIncomeCommand { IncomeDTO = incomeDTO });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Income>> DeleteIncome(int id)
        {
            try
            {
                await _mediator.Send(new DeleteIncomeCommand { Id = id });
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

            return NoContent();
        }
    }
}
