using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GoogleKeep.Models;
using GoogleKeep.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace GoogleKeep
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
		{
			Configuration = configuration;
			_currentEnvironment = currentEnvironment;
		}
		public IHostingEnvironment _currentEnvironment { get; }
		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddScoped<INoteService, NoteService>();
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});
			services.AddCors(corsOptions => corsOptions.AddPolicy("AppPolicy", builder =>
			   builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
			
			services.AddTransient<NoteService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}


			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
			app.UseCors("AppPolicy");
			app.UseHttpsRedirection();
			app.UseMvc();
			
		}
	}
}
