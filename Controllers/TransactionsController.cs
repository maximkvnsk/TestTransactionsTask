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
using Swashbuckle.AspNetCore.Annotations;

namespace TestTransactionsTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [SwaggerOperation(Summary = "Read .csv file, upoaded to fileForm and import data to DB")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ImportTransactionsFromCsvCommand command, IFormFile fileForm)
        {
            return Ok(await Mediator.Send(new ImportTransactionsFromCsvCommand { uploadedFile = fileForm }));
        }

        [SwaggerOperation(Summary = "Download .xlsx file, containing all DB records")]
        [HttpGet]
        public async Task<FileResult> GetAll()
        {
            return await Mediator.Send(new GetAllTransactionsQuery());
        }

        [SwaggerOperation(Summary = "Read 3 filtering parameters(ClientName, Type, Status) from json. Download .xlsx file, containing filtered records." +
            " It`s not nessesary to apply all filters at the same time")]
        [HttpGet("Filter")]
        public async Task<FileResult> GetFilteredTransactions(string client, string status, string type)
        {
            return await Mediator.Send(new GetFilteredTransactionsQuery {ClientName =  client, Status = status, Type = type });
        }

        [SwaggerOperation(Summary = "Delete transaction from DB by entered Id")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTransactionByIdCommand { Id = id }));
        }

        [SwaggerOperation(Summary = "Update transaction status by entered Id")]
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
