using ContactManager.Features.Abstractions;
using ContactManager.Features.Helpers;
using ContactManager.Features.Mappers;
using ContactManager.Features.Models.Requests;
using ContactManager.Features.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Features.Endpoints;

internal class GetAllContactsEndpoint : IEndPoints
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/contacts", GetAllContactsAsync)
            .Produces<ContactQueryResponse>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
	    
    private async Task<IResult> GetAllContactsAsync(
        [AsParameters] GetAllContactsRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var res = await sender.Send(query, cancellationToken);
        if (res.IsFailure)
            return ControllerResponse.ParseAndReturnMessage(res);

        var contactCommandResponse = res.Value!.ToResponse();
        return ControllerResponse.ParseAndReturnMessage(res, contactCommandResponse);
    }
}