using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class InscripcionEndpoints
{
    public static void MapInscripcionEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Inscripcion").WithTags(nameof(Inscripcion));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Inscripcions.ToListAsync();
        })
        .WithName("GetAllInscripcions")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Inscripcion>, NotFound>> (int idinscripcion, Proyecto282Context db) =>
        {
            return await db.Inscripcions.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdInscripcion == idinscripcion)
                is Inscripcion model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetInscripcionById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idinscripcion, Inscripcion inscripcion, Proyecto282Context db) =>
        {
            var affected = await db.Inscripcions
                .Where(model => model.IdInscripcion == idinscripcion)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdInscripcion, inscripcion.IdInscripcion)
                  .SetProperty(m => m.IdUsuario, inscripcion.IdUsuario)
                  .SetProperty(m => m.IdEvento, inscripcion.IdEvento)
                  .SetProperty(m => m.Estatus, inscripcion.Estatus)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateInscripcion")
        .WithOpenApi();

        group.MapPost("/", async (Inscripcion inscripcion, Proyecto282Context db) =>
        {
            db.Inscripcions.Add(inscripcion);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Inscripcion/{inscripcion.IdInscripcion}",inscripcion);
        })
        .WithName("CreateInscripcion")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idinscripcion, Proyecto282Context db) =>
        {
            var affected = await db.Inscripcions
                .Where(model => model.IdInscripcion == idinscripcion)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteInscripcion")
        .WithOpenApi();
    }
}
