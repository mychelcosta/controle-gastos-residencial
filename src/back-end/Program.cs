using ApiFinanceira;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddOpenApi("v1", o =>
{
    o.AddDocumentTransformer((document, context, CancellationToken) =>
    {
        document.Info = new()
        {
            Title = "ApiFinanceira",
            Description = "ApiFinanceira v1",
            Version = "v1",
        };
        
        document.Servers = 
        [
            new() { Url = "http://localhost:5139", Description = "Servidor Local (HTTP)" },
            new() { Url = "https://localhost:7163", Description = "Servidor Local (HTTPS)" }
        ];
        
        document.ExternalDocs = new()
        {
            Description = "Documentação externa",
            Url = new Uri("https://github.com/mychelcosta/controle-gastos-residencial")
        };
        
        return Task.CompletedTask;
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(Options =>
    Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/docs/{documentName}.json");
}

app.MapScalarApiReference("/scalar", options =>
{
    options.WithOpenApiRoutePattern("/docs/{documentName}.json");
    options.AddDocument("v1", "ApiFinanceira - v1");
    options.ShowDeveloperTools = DeveloperToolsVisibility.Never;
    options.DisableMcp();
    options.HideClientButton();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
