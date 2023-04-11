using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class InfraestructuraEndpoints
{
    public static void MapInfraestructuraEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Infraestructura").WithTags(nameof(Infraestructura));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Infraestructuras.ToListAsync();
        })
        .WithName("GetAllInfraestructuras")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Infraestructura>, NotFound>> (int idinfraestructura, Proyecto282Context db) =>
        {
            return await db.Infraestructuras.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdInfraestructura == idinfraestructura)
                is Infraestructura model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetInfraestructuraById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idinfraestructura, Infraestructura infraestructura, Proyecto282Context db) =>
        {
            var affected = await db.Infraestructuras
                .Where(model => model.IdInfraestructura == idinfraestructura)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdInfraestructura, infraestructura.IdInfraestructura)
                  .SetProperty(m => m.IdEvento, infraestructura.IdEvento)
                  .SetProperty(m => m.Nombre, infraestructura.Nombre)
                  .SetProperty(m => m.Tipo, infraestructura.Tipo)
                  .SetProperty(m => m.Capacidad, infraestructura.Capacidad)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateInfraestructura")
        .WithOpenApi();

        group.MapPost("/", async (Infraestructura infraestructura, Proyecto282Context db) =>
        {
            db.Infraestructuras.Add(infraestructura);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Infraestructura/{infraestructura.IdInfraestructura}",infraestructura);
        })
        .WithName("CreateInfraestructura")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idinfraestructura, Proyecto282Context db) =>
        {
            var affected = await db.Infraestructuras
                .Where(model => model.IdInfraestructura == idinfraestructura)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteInfraestructura")
        .WithOpenApi();
    }
}
