using Application.Commands.Batches.LoadBatch;
using Application.Queries.Batches.GetAll;
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
    }
}
