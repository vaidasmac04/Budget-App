using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetAPI.Data;
using BudgetProject.Models;

namespace BudgetAPI.Controllers
{
    [Route("api/ClientIncome")]
    [ApiController]
    public class ClientIncomeController : ControllerBase
    {
        private readonly BudgetContext _context;

        public ClientIncomeController(BudgetContext context)
        {
            _context = context;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncome(int clientId)
        {
            var incomes = await _context.Income.Where(o => o.ClientId == clientId).ToListAsync();

            if (incomes == null)
            {
                return NotFound();
            }

            return incomes;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
            _context.Income.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { clientId = income.Id }, income);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Income>> DeleteIncome(int id)
        {
            var income = await _context.Income.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Income.Remove(income);
            await _context.SaveChangesAsync();

            return income;
        }

        private bool IncomeExists(int id)
        {
            return _context.Income.Any(e => e.Id == id);
        }
    }
}
