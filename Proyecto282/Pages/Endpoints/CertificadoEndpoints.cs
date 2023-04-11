using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class CertificadoEndpoints
{
    public static void MapCertificadoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Certificado").WithTags(nameof(Certificado));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Certificados.ToListAsync();
        })
        .WithName("GetAllCertificados")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Certificado>, NotFound>> (int idcertificado, Proyecto282Context db) =>
        {
            return await db.Certificados.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdCertificado == idcertificado)
                is Certificado model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCertificadoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idcertificado, Certificado certificado, Proyecto282Context db) =>
        {
            var affected = await db.Certificados
                .Where(model => model.IdCertificado == idcertificado)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdCertificado, certificado.IdCertificado)
                  .SetProperty(m => m.IdUsuario, certificado.IdUsuario)
                  .SetProperty(m => m.IdEvento, certificado.IdEvento)
                  .SetProperty(m => m.FechaEmision, certificado.FechaEmision)
                  .SetProperty(m => m.NombreEvento, certificado.NombreEvento)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCertificado")
        .WithOpenApi();

        group.MapPost("/", async (Certificado certificado, Proyecto282Context db) =>
        {
            db.Certificados.Add(certificado);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Certificado/{certificado.IdCertificado}",certificado);
        })
        .WithName("CreateCertificado")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idcertificado, Proyecto282Context db) =>
        {
            var affected = await db.Certificados
                .Where(model => model.IdCertificado == idcertificado)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCertificado")
        .WithOpenApi();
    }
}
