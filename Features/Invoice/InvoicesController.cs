﻿using ItemShop.Features.Invoice.Models;
using ItemShop.Features.Transaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Invoice
{
    [Authorize]
    public class InvoicesController : ApiController
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceCreateModel model)
        {
            var id = await _service.Create(model.CustomerId);
            if(id == null)
            {
                return NotFound();
            }
            return Created(nameof(Create), id);

        }

        [HttpGet]
        public async Task<IEnumerable<InvoiceListingModel>> GetAllInvoices()
        {
            return await _service.GetAllInvoices();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceListingModel>> GetInvoiceById(int id)
        {
            var transaction = await _service.GetInvoiceById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        [HttpGet("{id}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionListingModel>>> GetInvoiceTransactions(int id)
        {

            var invoice = await _service.GetInvoiceById(id);
            if(invoice == null)
            {
                return NotFound();
            }

            return Ok( await _service.GetInvoiceTransactions(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var deleted = await _service.DeleteInvoice(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }



    }
}
