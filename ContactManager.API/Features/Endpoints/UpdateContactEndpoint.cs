using ContactManager.Features.Abstractions;
using ContactManager.Features.Helpers;
using ContactManager.Features.Mappers;
using ContactManager.Features.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Features.Endpoints;

internal class UpdateContactEndpoint : IEndPoints
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/contacts/{id}", UpdateContactAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
	    
    private async Task<IResult> UpdateContactAsync(
        [FromRoute] string id,
        [FromBody] UpdateContactRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var res = await sender.Send(command, cancellationToken);
        return ControllerResponse.ParseAndReturnMessage(res);
    }
}