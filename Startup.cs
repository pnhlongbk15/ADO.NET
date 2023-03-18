
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //Swagger
        services.AddSwaggerGen();
        //Controller 
        services.AddControllers();
        //Cors
        services.AddCors(configs =>
        {
            configs.AddPolicy(
                "AllowOrigin",
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
        });
        services
           .AddControllersWithViews()
           .AddNewtonsoftJson(
               options =>
                   options.SerializerSettings.ReferenceLoopHandling =
                       ReferenceLoopHandling.Ignore
           )
           .AddNewtonsoftJson(
               options =>
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver()
           );


        // Services custom
        services.AddTransient<DbContext>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Cors
        app.UseCors(configs => configs.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}