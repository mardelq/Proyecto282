using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class ComentarioEndpoints
{
    public static void MapComentarioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Comentario");

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Comentarios.ToListAsync();
        })
        .WithName("GetAllComentarios");

        group.MapGet("/{id}", async Task<Results<Ok<Comentario>, NotFound>> (int idcomentario, Proyecto282Context db) =>
        {
            return await db.Comentarios.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdComentario == idcomentario)
                is Comentario model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetComentarioById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idcomentario, Comentario comentario, Proyecto282Context db) =>
        {
            var affected = await db.Comentarios
                .Where(model => model.IdComentario == idcomentario)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdComentario, comentario.IdComentario)
                  .SetProperty(m => m.IdUsuario, comentario.IdUsuario)
                  .SetProperty(m => m.IdEvento, comentario.IdEvento)
                  .SetProperty(m => m.Comentario1, comentario.Comentario1)
                  .SetProperty(m => m.IdExpositorResponde, comentario.IdExpositorResponde)
                  .SetProperty(m => m.FechaComentario, comentario.FechaComentario)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateComentario");

        group.MapPost("/", async (Comentario comentario, Proyecto282Context db) =>
        {
            db.Comentarios.Add(comentario);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Comentario/{comentario.IdComentario}",comentario);
        })
        .WithName("CreateComentario");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idcomentario, Proyecto282Context db) =>
        {
            var affected = await db.Comentarios
                .Where(model => model.IdComentario == idcomentario)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteComentario");
    }
}
