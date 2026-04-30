using ContactManager.Features.Abstractions;
using ContactManager.Features.Helpers;
using ContactManager.Features.Mappers;
using ContactManager.Features.Models.Requests;
using ContactManager.Features.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Features.Endpoints;

internal class CreateContactEndpoint : IEndPoints
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/contacts", CreateContactAsync)
            .Produces<ContactCommandResponse>()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
	    
    private async Task<IResult> CreateContactAsync(
        [FromBody] CreateContactRequest request,
        [FromServices] ISender sender,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var res = await sender.Send(command, cancellationToken);
        return ControllerResponse.ParseAndReturnMessage(res);
    }
}