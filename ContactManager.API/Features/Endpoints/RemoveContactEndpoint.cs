using ContactManager.Application.Features.Commands.RemoveContact;
using ContactManager.Features.Abstractions;
using ContactManager.Features.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Features.Endpoints;

internal class RemoveContactEndpoint : IEndPoints
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/contacts/{id}", RemoveContactAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
	    
    private async Task<IResult> RemoveContactAsync(
        [FromRoute] string id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RemoveContactCommand(id);
        var res = await sender.Send(command, cancellationToken);
        return ControllerResponse.ParseAndReturnMessage(res);
    }
}