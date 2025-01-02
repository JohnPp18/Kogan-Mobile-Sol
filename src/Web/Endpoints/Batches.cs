using Application.Commands.Batches.DeleteBatch;
using Application.Commands.Batches.LoadBatch;
using Application.Queries.Batches.Common;
using Application.Queries.Batches.GetAll;
using Application.Queries.Batches.GetSingle;
using Kogan.Mobile.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kogan.Mobile.Web.Endpoints
{
    public class Batches : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapPost("Batches", LoadBatchCommandFromFile);
            app.MapGet("Batches", GetAll);
            app.MapGet("Batches/{id}", GetSingle);
            app.MapDelete("Batches/{id}", DeleteSingle);
        }

        public async Task<Created<int>> LoadBatchCommandFromFile(ISender sender, LoadBatchFromFileCommand command)
        {
            var id = await sender.Send(command);

            return TypedResults.Created($"/{nameof(Batches)}/{id}", id);
        }

        public async Task<GetAllBatchResult> GetAll(ISender sender, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            return await sender.Send(new GetAllBatchesQuery(page,pageSize));
        }

        public async Task<BatchResult> GetSingle(ISender sender, int id = 1)
        {
            return await sender.Send(new GetSingleBatchByIdQuery(id));
        }

        public async Task<NoContentResult> DeleteSingle(ISender sender, int id = 1)
        {
            await sender.Send(new DeleteBatchCommand(id));

            return new NoContentResult();
        }
    }
}
