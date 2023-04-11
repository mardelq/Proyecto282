using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class HorarioExposicionEndpoints
{
    public static void MapHorarioExposicionEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/HorarioExposicion").WithTags(nameof(HorarioExposicion));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.HorarioExposicions.ToListAsync();
        })
        .WithName("GetAllHorarioExposicions")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<HorarioExposicion>, NotFound>> (int idhorarioexposicion, Proyecto282Context db) =>
        {
            return await db.HorarioExposicions.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdHorarioExposicion == idhorarioexposicion)
                is HorarioExposicion model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetHorarioExposicionById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idhorarioexposicion, HorarioExposicion horarioExposicion, Proyecto282Context db) =>
        {
            var affected = await db.HorarioExposicions
                .Where(model => model.IdHorarioExposicion == idhorarioexposicion)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdHorarioExposicion, horarioExposicion.IdHorarioExposicion)
                  .SetProperty(m => m.IdEvento, horarioExposicion.IdEvento)
                  .SetProperty(m => m.IdAmbiente, horarioExposicion.IdAmbiente)
                  .SetProperty(m => m.IdExpositor, horarioExposicion.IdExpositor)
                  .SetProperty(m => m.FechaExposicion, horarioExposicion.FechaExposicion)
                  .SetProperty(m => m.HoraInicio, horarioExposicion.HoraInicio)
                  .SetProperty(m => m.HoraFin, horarioExposicion.HoraFin)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateHorarioExposicion")
        .WithOpenApi();

        group.MapPost("/", async (HorarioExposicion horarioExposicion, Proyecto282Context db) =>
        {
            db.HorarioExposicions.Add(horarioExposicion);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/HorarioExposicion/{horarioExposicion.IdHorarioExposicion}",horarioExposicion);
        })
        .WithName("CreateHorarioExposicion")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idhorarioexposicion, Proyecto282Context db) =>
        {
            var affected = await db.HorarioExposicions
                .Where(model => model.IdHorarioExposicion == idhorarioexposicion)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteHorarioExposicion")
        .WithOpenApi();
    }
}
