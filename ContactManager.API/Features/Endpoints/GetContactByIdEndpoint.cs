using ContactManager.Application.Features.Queries.GetContactById;
using ContactManager.Features.Abstractions;
using ContactManager.Features.Helpers;
using ContactManager.Features.Mappers;
using ContactManager.Features.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Features.Endpoints;

internal class GetContactByIdEndpoint : IEndPoints
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/contacts/{id}", GetContactByIdAsync)
            .Produces<ContactQueryResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
	    
    private async Task<IResult> GetContactByIdAsync(
        [FromRoute] string id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetContactByIdQuery(id);
        var res = await sender.Send(query, cancellationToken);
        if (res.IsFailure)
            return ControllerResponse.ParseAndReturnMessage(res);

        var queryResponse = res.Value!.ToResponse();
        return ControllerResponse.ParseAndReturnMessage(res, queryResponse);
    }
}