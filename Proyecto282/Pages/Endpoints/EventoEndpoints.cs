using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class EventoEndpoints
{
    public static void MapEventoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Evento").WithTags(nameof(Evento));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Eventos.ToListAsync();
        })
        .WithName("GetAllEventos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Evento>, NotFound>> (int idevento, Proyecto282Context db) =>
        {
            return await db.Eventos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdEvento == idevento)
                is Evento model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEventoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idevento, Evento evento, Proyecto282Context db) =>
        {
            var affected = await db.Eventos
                .Where(model => model.IdEvento == idevento)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdEvento, evento.IdEvento)
                  .SetProperty(m => m.NombreEvento, evento.NombreEvento)
                  .SetProperty(m => m.Fecha, evento.Fecha)
                  .SetProperty(m => m.Hora, evento.Hora)
                  .SetProperty(m => m.Lugar, evento.Lugar)
                  .SetProperty(m => m.TipoEvento, evento.TipoEvento)
                  .SetProperty(m => m.Publico, evento.Publico)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEvento")
        .WithOpenApi();

        group.MapPost("/", async (Evento evento, Proyecto282Context db) =>
        {
            db.Eventos.Add(evento);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Evento/{evento.IdEvento}",evento);
        })
        .WithName("CreateEvento")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idevento, Proyecto282Context db) =>
        {
            var affected = await db.Eventos
                .Where(model => model.IdEvento == idevento)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEvento")
        .WithOpenApi();
    }
}
