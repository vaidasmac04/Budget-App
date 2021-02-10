using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BudgetAPI.Services.Incomes;
using BudgetAPI.Models;
using BudgetAPI.DTOs;
using BudgetAPI.Mapping;
using AutoMapper;
using System.Linq;

namespace BudgetAPI.Controllers
{
    [Route("api/Income")]
    [ApiController]
    [Authorize]
    public class ClientIncomeController : ControllerBase
    {
        private readonly IIncomeHandler _incomeHandler;
        private readonly IMapper _mapper;

        public ClientIncomeController(IIncomeHandler incomeHandler, IMapper mapper)
        {
            _incomeHandler = incomeHandler;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes(int clientId)
        {
            var incomes = await _incomeHandler.GetByClientId(clientId);
            return _mapper.Map<List<IncomeDTO>>(incomes);
        }


        [HttpGet("{clientId}/{source}")]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes(int clientId, string source)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, source);
                return _mapper.Map<List<IncomeDTO>>(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
        }

        [HttpGet("{clientId}/{monthFrom}/{monthTo}")]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes(int clientId, DateTime monthFrom, DateTime monthTo)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, monthFrom, monthTo);
                return _mapper.Map<List<IncomeDTO>>(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

        }

        [HttpGet("{clientId}/{monthFrom}/{monthTo}/{source}")]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes(int clientId, DateTime monthFrom, DateTime monthTo, string source)
        {
            try
            {
                var incomes = await _incomeHandler.GetByClientId(clientId, monthFrom, monthTo, source);
                return _mapper.Map<List<IncomeDTO>>(incomes);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }

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
