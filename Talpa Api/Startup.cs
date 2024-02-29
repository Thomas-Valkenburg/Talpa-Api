﻿using Talpa_Api.Context;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen();

        services.AddDbContext<SuggestionContext>(options =>
        {
            options.UseMySQL(Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
        });
        services.AddDbContext<UserContext>(options =>
        {
            options.UseMySQL(Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
        });
        services.AddDbContext<TeamContext>(options =>
        {
            options.UseMySQL(Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
        });
        services.AddDbContext<TagContext>(options =>
        {
            options.UseMySQL(Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}