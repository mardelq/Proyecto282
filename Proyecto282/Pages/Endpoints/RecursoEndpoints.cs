using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class RecursoEndpoints
{
    public static void MapRecursoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Recurso").WithTags(nameof(Recurso));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Recursos.ToListAsync();
        })
        .WithName("GetAllRecursos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Recurso>, NotFound>> (int idrecurso, Proyecto282Context db) =>
        {
            return await db.Recursos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdRecurso == idrecurso)
                is Recurso model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetRecursoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idrecurso, Recurso recurso, Proyecto282Context db) =>
        {
            var affected = await db.Recursos
                .Where(model => model.IdRecurso == idrecurso)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdRecurso, recurso.IdRecurso)
                  .SetProperty(m => m.IdAmbiente, recurso.IdAmbiente)
                  .SetProperty(m => m.NombreRecurso, recurso.NombreRecurso)
                  .SetProperty(m => m.DescripcionRecurso, recurso.DescripcionRecurso)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateRecurso")
        .WithOpenApi();

        group.MapPost("/", async (Recurso recurso, Proyecto282Context db) =>
        {
            db.Recursos.Add(recurso);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Recurso/{recurso.IdRecurso}",recurso);
        })
        .WithName("CreateRecurso")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idrecurso, Proyecto282Context db) =>
        {
            var affected = await db.Recursos
                .Where(model => model.IdRecurso == idrecurso)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteRecurso")
        .WithOpenApi();
    }
}
