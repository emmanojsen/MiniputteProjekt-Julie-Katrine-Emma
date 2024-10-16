using Microsoft.EntityFrameworkCore;
using MiniputteProjekt.Data;
using MiniputteProjekt.Model;
using MiniputteProjekt.Service;
using System.Xml.Linq;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DataServices>();
builder.Services.AddDbContext<PostsContext>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataServices>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}


//Endpoints

app.MapGet("/", () => "Velkommen til miniputte Reddit API!");
app.MapGet("/api/posts", async (PostsContext db) =>
                await db.Posts.Include(p => p.Comment).ToListAsync());

app.MapGet("/api/posts/{id}", async (int id, PostsContext db) =>
    await db.Posts.Include(p => p.Comment).FirstOrDefaultAsync(p => p.PostsId == id) is MiniputteProjekt.Model.Posts post
        ? Results.Ok(post)
        : Results.NotFound());

app.MapPost("/api/posts", async (MiniputteProjekt.Model.Posts newPost, PostsContext db) =>
{
    db.Posts.Add(newPost);
    await db.SaveChangesAsync();
    return Results.Created($"/api/posts/{newPost.PostsId}", newPost);
});

app.MapPut("/api/posts/{id}", async (int id, MiniputteProjekt.Model.Posts updatedPost, PostsContext db) =>
{
    if (id != updatedPost.PostsId)
        return Results.BadRequest("Post ID mismatch.");

    var existingPost = await db.Posts.FindAsync(id);
    if (existingPost == null)
        return Results.NotFound("Post not found.");

    existingPost.Text = updatedPost.Text;
    existingPost.Title = updatedPost.Title;
    existingPost.Author = updatedPost.Author;
    existingPost.Date = updatedPost.Date;
    existingPost.Votes = updatedPost.Votes;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/api/posts/{id}/comments", async (int id, Comment newComment, PostsContext db) =>
{
    var post = await db.Posts.FindAsync(id);
    if (post == null)
        return Results.NotFound();

    newComment.PostsId = id;
    db.Comments.Add(newComment);
    await db.SaveChangesAsync();
    return Results.Ok(newComment);
});

app.Run();
