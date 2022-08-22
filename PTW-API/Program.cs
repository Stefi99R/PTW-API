using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PTW_API.Contracts;
using PTW_API.Services;

var builder = WebApplication.CreateBuilder(args);

#region Adding services to the container

builder.Services.AddControllers();

builder.Services.AddPTWSettings(builder.Configuration, builder.Environment)
                .AddForwardedHeaders()
                .RegisterSwaggerDocumentation()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddCors()
                .AddJobsConfig()
                .AddHealthChecks();

/*
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("PTW"));

conStrBuilder.Password = builder.Configuration["Db:Password"];
conStrBuilder.UserID = builder.Configuration["Db:User"];

var connection = conStrBuilder.ConnectionString;

builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connection));*/

#endregion

var app = builder.Build();

#region Configuring the request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/1.0/swagger.json", "PTW Api"));
}
else
{
    app.UseHsts();
}

app.MapControllers();

app.UseStaticFiles()
   .UseRouting()
   .UseEndpoints(endpoints =>
   {
       endpoints.MapControllers();
       endpoints.MapDefaultControllerRoute();
       endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
       {
           Predicate = r => r.Name.Contains("self")
       });
   })
   .UseSwagggerDocumentation()
   .UseHttpsRedirection()
   .UseAuthorization()
   .UseHealthCheck()
   .UseForwardedHeaders()
   .UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

app.UseHangfire();

app.AddBackgroundJobs();

#endregion

app.Run();

