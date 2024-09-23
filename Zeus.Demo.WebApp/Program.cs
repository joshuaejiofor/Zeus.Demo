using Hangfire;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Zeus.Demo.ApplicationCore.DependencyInjection;
using Zeus.Demo.ApplicationCore.Extensions;
using Zeus.Demo.ApplicationCore.Factories;
using Zeus.Demo.ApplicationCore.Factories.Interfaces;
using Zeus.Demo.ApplicationCore.Mapping;
using Zeus.Demo.Core.Helpers;
using Zeus.Demo.Core.Helpers.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.EntityFrameworkCore;
using Zeus.Demo.WebApp.DependencyInjection;
using Zeus.Demo.WebApp.Filters;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = AppContext.BaseDirectory, Args = args });

// Add services to the container.
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddAutoMapper(typeof(MappingProfile))
                .AddHangfire(builder.Configuration)
                .AddHangfireServer(options => options.WorkerCount = Convert.ToInt32(builder.Configuration["Hangfire:WorkerCountByCores"]))

                .AddScoped<ZeusDemoDbContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IProductProviderFactory, ProductProviderFactory>()

                .AddServices()
                .AddRepositories()
                .AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()))
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IUriHelper>(o =>
                {
                    var accessor = o.GetRequiredService<IHttpContextAccessor>();
                    var request = accessor.HttpContext?.Request;
                    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
                    return new UriHelper(uri);
                });

builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Host.AddSerilog();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var zeusDemoDbContext = services.GetRequiredService<ZeusDemoDbContext>();
    await zeusDemoDbContext.Database.EnsureCreatedAsync();
    await zeusDemoDbContext.SeedDB();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error").UseHsts();
}

var swaggerPath = app.Environment.IsDevelopment() ? "/swagger/v1/swagger.json" : Path.Combine("v1", "swagger.json");
app.UseSwagger()
   .UseSwaggerUI(setupAction =>
   {
       setupAction.SwaggerEndpoint(swaggerPath, "Zeus Demo");
       setupAction.RoutePrefix = "swagger"; // To available at root
   })
   .UseSerilogRequestLogging()
   .UseHttpsRedirection()
   .UseStaticFiles()

   .UseRouting()
   .UseCors()

   .UseAuthorization()
   .UseHangfireDashboard("/hangfire", new DashboardOptions
   {
       Authorization = [new HangFireAuthorizeFilter()]
   })
   .UseEndpoints(endpoints =>
   {
       endpoints.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
       //endpoints.MapRazorPages();
       endpoints.MapHangfireDashboard();
   });

await app.UseOcelot();

app.Run();
