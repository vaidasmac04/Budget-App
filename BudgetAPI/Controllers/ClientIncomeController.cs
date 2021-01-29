using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BudgetAPI.Services.Incomes;
using BudgetAPI.Models;
using BudgetAPI.ViewModels;
using BudgetAPI.Mapping;

namespace BudgetAPI.Controllers
{
    [Route("api/Income")]
    [ApiController]
    [Authorize]
    public class ClientIncomeController : ControllerBase
    {
        private readonly IIncomeHandler _incomeHandler;
        private readonly IEntityMapper<Income, IncomeViewModel> _mapper;

        public ClientIncomeController(IIncomeHandler incomeHandler,
            IEntityMapper<Income, IncomeViewModel> mapper)
        {
            _incomeHandler = incomeHandler;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetIncomes(int clientId)
        {

            var incomes = await _incomeHandler.GetByClientId(clientId);
            return _mapper.Map(incomes); 
        }


        [HttpGet("{clientId}/{source}")]
        public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetIncomes(int clientId, string source)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, source);
                return _mapper.Map(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
        }

        [HttpGet("{clientId}/{monthFrom}/{monthTo}")]
        public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetIncomes(int clientId, DateTime monthFrom, DateTime monthTo)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, monthFrom, monthTo);
                return _mapper.Map(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

        }

        [HttpGet("{clientId}/{monthFrom}/{monthTo}/{source}")]
        public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetIncomes(int clientId, DateTime monthFrom, DateTime monthTo, string source)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, monthFrom, monthTo, source);
                return _mapper.Map(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

        }

        [HttpPut("{clientId}/{id}")]
        public async Task<IActionResult> PutIncome(int clientId, int id, [FromBody]IncomeViewModel incomeViewModel)
        {
            if (id != incomeViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var income = _mapper.MapBack(incomeViewModel);
                await _incomeHandler.Update(income, clientId, incomeViewModel.SourceName);
            }
            catch(ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            
            return NoContent();
        }

        [HttpPost("{clientId}")]
        public async Task<ActionResult<Income>> PostIncome([FromBody]IncomeViewModel incomeViewModel, int clientId)
        {
            try
            {
                await _incomeHandler.Add(_mapper.MapBack(incomeViewModel), clientId, incomeViewModel.SourceName);
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
