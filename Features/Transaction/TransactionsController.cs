using ItemShop.Features.Transaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction
{
    [Authorize]
    public class TransactionsController : ApiController
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateModel model)
        {
            var id = await _service.Create(model.CustomerId, model.ProductId, model.Quantity);
            return Created(nameof(Create), id);

        }

        [HttpGet]
        public async Task<IEnumerable<TransactionListingModel>> GetAllTransactions()
        {
            return await _service.GetAllTransactions();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailModel>> GetTransactionById(int id)
        {
            var transaction =  await _service.GetTransactionById(id);
            if(transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var deleted = await _service.DeleteTransaction(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionUpdateModel model)
        {
            var updated = await _service.UpdateTransaction(id, model.CustomerId, model.ProductId, model.Quantity);
            if (!updated)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
