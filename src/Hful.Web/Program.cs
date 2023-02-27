using Hful.Core.Extensions;
using Hful.EntityFrameworkCore;
using Hful.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModule<WebModule>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HfulContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();