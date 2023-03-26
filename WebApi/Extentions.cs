using Microsoft.AspNetCore.Mvc;

namespace WebApi;

public static class Extentions
{
    public static IEndpointRouteBuilder MapCRUDEndpoints<TEntity, TContext>(this IEndpointRouteBuilder builder)
        where TEntity : class
        where TContext : DbContext
    {
        var endpoint = typeof(TEntity).Name.ToLower();

        builder.MapGet(endpoint, async ([FromServices] TContext ctx) => Results.Ok(await ctx.Set<TEntity>().ToListAsync()));

        builder.MapGet($"{endpoint}/{{id}}", async ([FromRoute] int id, [FromServices] TContext ctx) =>
        {
            var entity = await ctx.FindAsync<TEntity>(new object[] { id });
            if (entity is null)
                return Results.NotFound();
            return Results.Ok(entity);
        });

        builder.MapPost(endpoint, async ([FromBody] TEntity entity, [FromServices] TContext ctx) =>
        {
            ctx.Add<TEntity>(entity);
            var r = await ctx.SaveChangesAsync();
            return Results.Created($"{r}", null);
        });

        builder.MapPut(endpoint, async ([FromBody] TEntity entity, [FromServices] TContext ctx) =>
        {
            ctx.Update<TEntity>(entity);
            await ctx.SaveChangesAsync();
            return Results.Accepted();
        });

        builder.MapDelete($"{endpoint}/{{id}}", async ([FromRoute] int id, [FromServices] TContext ctx) =>
        {
            var entity = await ctx.FindAsync<TEntity>(new object[] { id });
            if (entity is null)
                return Results.NotFound();
            ctx.Remove<TEntity>(entity);
            await ctx.SaveChangesAsync();
            return Results.Ok();
        });

        return builder;
    }
}
