using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestTransactionsTask.Features.TransactionFeatures.Commands;
using TestTransactionsTask.Features.TransactionFeatures.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TestTransactionsTask.Controllers
{
  //  [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController: ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ImportTransactionsFromCsvCommand command, IFormFile file)
        {
            return Ok(await Mediator.Send(new ImportTransactionsFromCsvCommand { uploadedFile = file }));
        }


        [HttpGet]
        public async Task<FileResult> GetAll()
        {
            return await Mediator.Send(new GetAllTransactionsQuery());
        }

        [HttpGet("Filter")]
        public async Task<FileResult> GetFilteredTransactions(string client, string status, string type)
        {
            return await Mediator.Send(new GetFilteredTransactionsQuery {ClientName =  client, Status = status, Type = type });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTransactionByIdCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTransactionStatusByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

    }
}
