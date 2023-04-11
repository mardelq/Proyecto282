using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Proyecto282.Models;
namespace Proyecto282.Pages.Endpoints;

public static class MaterialEndpoints
{
    public static void MapMaterialEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Material").WithTags(nameof(Material));

        group.MapGet("/", async (Proyecto282Context db) =>
        {
            return await db.Materials.ToListAsync();
        })
        .WithName("GetAllMaterials")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Material>, NotFound>> (int idmaterial, Proyecto282Context db) =>
        {
            return await db.Materials.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdMaterial == idmaterial)
                is Material model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMaterialById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idmaterial, Material material, Proyecto282Context db) =>
        {
            var affected = await db.Materials
                .Where(model => model.IdMaterial == idmaterial)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.IdMaterial, material.IdMaterial)
                  .SetProperty(m => m.IdEvento, material.IdEvento)
                  .SetProperty(m => m.NombreMaterial, material.NombreMaterial)
                  .SetProperty(m => m.Descripcion, material.Descripcion)
                  .SetProperty(m => m.Disponible, material.Disponible)
                  .SetProperty(m => m.IdExpositorSube, material.IdExpositorSube)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMaterial")
        .WithOpenApi();

        group.MapPost("/", async (Material material, Proyecto282Context db) =>
        {
            db.Materials.Add(material);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Material/{material.IdMaterial}",material);
        })
        .WithName("CreateMaterial")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idmaterial, Proyecto282Context db) =>
        {
            var affected = await db.Materials
                .Where(model => model.IdMaterial == idmaterial)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMaterial")
        .WithOpenApi();
    }
}
