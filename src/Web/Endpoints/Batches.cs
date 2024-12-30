using Application.Commands.Batches.LoadBatch;
using Kogan.Mobile.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kogan.Mobile.Web.Endpoints
{
    public class Batches : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapPost("Batches", LoadBatchCommandFromFile);
        }

        public async Task<Created<int>> LoadBatchCommandFromFile(ISender sender, LoadBatchFromFileCommand command)
        {
            var id = await sender.Send(command);

            return TypedResults.Created($"/{nameof(Batches)}/{id}", id);
        }
    }
}
