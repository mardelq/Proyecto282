using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class AmbienteEndpoints
{
    public static void MapAmbienteEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Ambiente").WithTags(nameof(Ambiente));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Ambientes.ToListAsync();
        })
        .WithName("GetAllAmbientes")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Ambiente>, NotFound>> (int idambiente, Proyecto282Context db) =>
        {
            return await db.Ambientes.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdAmbiente == idambiente)
                is Ambiente model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAmbienteById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idambiente, Ambiente ambiente, Proyecto282Context db) =>
        {
            var affected = await db.Ambientes
                .Where(model => model.IdAmbiente == idambiente)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdAmbiente, ambiente.IdAmbiente)
                  .SetProperty(m => m.IdEvento, ambiente.IdEvento)
                  .SetProperty(m => m.NombreAmbiente, ambiente.NombreAmbiente)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAmbiente")
        .WithOpenApi();

        group.MapPost("/", async (Ambiente ambiente, Proyecto282Context db) =>
        {
            db.Ambientes.Add(ambiente);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Ambiente/{ambiente.IdAmbiente}",ambiente);
        })
        .WithName("CreateAmbiente")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idambiente, Proyecto282Context db) =>
        {
            var affected = await db.Ambientes
                .Where(model => model.IdAmbiente == idambiente)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAmbiente")
        .WithOpenApi();
    }
}
