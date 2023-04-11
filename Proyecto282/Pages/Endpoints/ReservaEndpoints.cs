using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class ReservaEndpoints
{
    public static void MapReservaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Reserva").WithTags(nameof(Reserva));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Reservas.ToListAsync();
        })
        .WithName("GetAllReservas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Reserva>, NotFound>> (int idreserva, Proyecto282Context db) =>
        {
            return await db.Reservas.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdReserva == idreserva)
                is Reserva model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetReservaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idreserva, Reserva reserva, Proyecto282Context db) =>
        {
            var affected = await db.Reservas
                .Where(model => model.IdReserva == idreserva)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdReserva, reserva.IdReserva)
                  .SetProperty(m => m.IdUsuario, reserva.IdUsuario)
                  .SetProperty(m => m.IdAmbiente, reserva.IdAmbiente)
                  .SetProperty(m => m.FechaReserva, reserva.FechaReserva)
                  .SetProperty(m => m.HoraInicio, reserva.HoraInicio)
                  .SetProperty(m => m.HoraFin, reserva.HoraFin)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateReserva")
        .WithOpenApi();

        group.MapPost("/", async (Reserva reserva, Proyecto282Context db) =>
        {
            db.Reservas.Add(reserva);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Reserva/{reserva.IdReserva}",reserva);
        })
        .WithName("CreateReserva")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idreserva, Proyecto282Context db) =>
        {
            var affected = await db.Reservas
                .Where(model => model.IdReserva == idreserva)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteReserva")
        .WithOpenApi();
    }
}
