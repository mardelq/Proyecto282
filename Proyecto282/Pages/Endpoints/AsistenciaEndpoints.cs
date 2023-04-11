using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class AsistenciaEndpoints
{
    public static void MapAsistenciaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Asistencia").WithTags(nameof(Asistencia));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Asistencia.ToListAsync();
        })
        .WithName("GetAllAsistencias")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Asistencia>, NotFound>> (int idasistencia, Proyecto282Context db) =>
        {
            return await db.Asistencia.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdAsistencia == idasistencia)
                is Asistencia model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAsistenciaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idasistencia, Asistencia asistencia, Proyecto282Context db) =>
        {
            var affected = await db.Asistencia
                .Where(model => model.IdAsistencia == idasistencia)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdAsistencia, asistencia.IdAsistencia)
                  .SetProperty(m => m.IdUsuario, asistencia.IdUsuario)
                  .SetProperty(m => m.IdEvento, asistencia.IdEvento)
                  .SetProperty(m => m.Fecha, asistencia.Fecha)
                  .SetProperty(m => m.Hora, asistencia.Hora)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAsistencia")
        .WithOpenApi();

        group.MapPost("/", async (Asistencia asistencia, Proyecto282Context db) =>
        {
            db.Asistencia.Add(asistencia);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Asistencia/{asistencia.IdAsistencia}",asistencia);
        })
        .WithName("CreateAsistencia")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idasistencia, Proyecto282Context db) =>
        {
            var affected = await db.Asistencia
                .Where(model => model.IdAsistencia == idasistencia)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAsistencia")
        .WithOpenApi();
    }
}
