using GGCasualNote.Models;
using GGCasualNote.Repositories;
using GGCasualNote.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GgNoteContext>();
builder.Services.AddTransient<CharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ComboNoteRepository, ComboNoteRepository>();
builder.Services.AddTransient<MoveRepository, MoveRepository>();
builder.Services.AddTransient<MoveListTimestampRepository, MoveListTimestampRepository>();
builder.Services.AddTransient<ScrapService, ScrapService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    // {
    //     options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    //     options.RoutePrefix = string.Empty;
    // });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
