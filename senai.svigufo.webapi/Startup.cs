using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace senai.svigufo.webapi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // No ConfigureServices é onde os serviços são adicionados
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona o MVC ao projeto
            services.AddMvc()
            
            // Adiciona as opções do JSON
            .AddJsonOptions(options => {
                // Ignora valores nulos
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            
            // Define a versão do Core
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //Adiciona o Cors ao projeto
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Adiciona o Swagger ao projeto passando algumas informações
            // https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SviGufo API", Version = "v1" });
            });

            // Define a forma de autenticação
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            // Define os parâmetros da validação do token
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Quem está solicitando
                    ValidateIssuer = true,

                    //Quem está validando
                    ValidateAudience = true,

                    //Definindo o tempo de expiração
                    ValidateLifetime = true,

                    //Forma de criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("svigufo-chave-autenticacao")),

                    //Tempo de expiração do Token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    //Nome da Issuer, de onde está vindo
                    ValidIssuer = "SviGufo.WebApi",

                    //Nome da audience, de onde está vindo
                    ValidAudience = "SviGufo.WebApi"
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SviGufo");
            });

            // Habilita a autenticação
            app.UseAuthentication();

            // Habilita o Cors
            app.UseCors("CorsPolicy");

            // Habilita o MVC
            app.UseMvc();

        }
    }
}
