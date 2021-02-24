using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BudgetAPI.Services.Incomes;
using AutoMapper;
using MediatR;
using Budget.Application.Incomes;
using Budget.Domain;

namespace BudgetAPI.Controllers
{
    
    public class IncomeController : ApiControllerBase
    {
        private readonly IIncomeHandler _incomeHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IncomeController(IIncomeHandler incomeHandler, IMapper mapper, IMediator mediator)
        {
            _incomeHandler = incomeHandler;
            _mapper = mapper;
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

        [HttpPut("{clientId}/{id}")]
        public async Task<IActionResult> PutIncome(int clientId, int id, [FromBody]IncomeDTO incomeDTO)
        {
            if (id != incomeDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var income = _mapper.Map<Income>(incomeDTO);
                await _incomeHandler.Update(income, clientId, incomeDTO.SourceName);
            }
            catch(ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            
            return NoContent();
        }

        [HttpPost("{clientId}")]
        public async Task<ActionResult<Income>> PostIncome([FromBody]IncomeDTO incomeDTO, int clientId)
        {
            try
            {
                await _incomeHandler.Add(_mapper.Map<Income>(incomeDTO), clientId, incomeDTO.SourceName);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }


            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Income>> DeleteIncome(int id)
        {
            try
            {
                await _incomeHandler.Delete(id);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

            return NoContent();
        }
    }
}
