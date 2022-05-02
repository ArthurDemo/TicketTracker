global using FastEndpoints;

using FastEndpoints.Swagger;

using TicketTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi(); //add this
app.UseSwaggerUi3(c => c.ConfigureDefaults()); //add this

app.Run();