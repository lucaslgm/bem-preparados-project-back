using System;
using System.Text;
using Application.Models;
using Application.Token;
using AutoMapper;
using Domain.Entities;
using Domain.Tokens;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Dtos;
using Service.Interfaces;
using Services;

namespace Application
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      #region CORS
      services.AddCors(options =>
    {
      options.AddDefaultPolicy(
              builder =>
              {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
              });
    });
      #endregion

      services.AddControllers();

      #region Swagger

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "API Projeto Bem",
          Version = "v1",
          Description = "API construída no trabalho final do projeto Bem Preparados.",
          Contact = new OpenApiContact
          {
            Name = "Lucas Martins",
            Email = "lucas@bempromotora.com",
            Url = new Uri("https://www.bempromotora.com.br/")
          },
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Por favor utilize Bearer <TOKEN>",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
          });


      });

      #endregion

      #region Jwt Old
      /*
            // Aqui vai a key secreta, o recomendado é guarda-la no arquivo de configuração
            var secretKey = Configuration["Jwt:Key"];

            services.AddAuthentication(x =>
            {
              x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
              };
            });
      */
      #endregion

      #region Jwt

      Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
      Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
      Environment.SetEnvironmentVariable("Seconds", "28800");

      var accessConfiguration = new AccessConfigurations();
      services.AddSingleton(accessConfiguration);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = accessConfiguration.Key;
        paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
        paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

        // Valida a assinatura de um token recebido
        paramsValidation.ValidateIssuerSigningKey = true;

        // Verifica se um token recebido ainda é válido
        paramsValidation.ValidateLifetime = true;

        // Tempo de tolerância para a expiração de um token (utilizado
        // caso haja problemas de sincronismo de horário entre diferentes
        // computadores envolvidos no processo de comunicação)
        paramsValidation.ClockSkew = TimeSpan.Zero;
      });

      // Ativa o uso do token como forma de autorizar o acesso
      // a recursos deste projeto
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                  .RequireAuthenticatedUser().Build());
      });

      #endregion

      #region Dependency Injection
      /*
        => services.AddTransient<>();
        => Adiciona uma instância nova em cada ponto do código em que for necessária

        => services.AddSingleton<>()
        => Adiciona uma única instância durante todo o ciclo da aplicação independente do tempo em que esteja rodando

        => Adiciona uma instância única por requisição
        => services.AddScoped<>();
      */

      services.AddScoped<IS_Usuario, S_Usuario>();
      services.AddScoped<IS_Login, S_Login>();
      services.AddScoped<IS_Situacao, S_Situacao>();
      services.AddScoped<IS_Conveniada, S_Conveniada>();
      services.AddScoped<IS_LimitesIdade, S_LimitesIdade>();
      services.AddScoped<IS_Proposta, S_Proposta>();
      services.AddScoped<IS_Cliente, S_Cliente>();
      services.AddScoped<IS_ViaCep, S_ViaCep>();
      services.AddScoped<IS_Cadastro, S_Cadastro>();
      // services.AddScoped<IUserRepository, UserRepository>();

      services.AddScoped<IR_Usuario, R_Usuario>();
      services.AddScoped<IR_Login, R_Login>();
      services.AddScoped<IR_Situacao, R_Situacao>();
      services.AddScoped<IR_Conveniada, R_Conveniada>();
      services.AddScoped<IR_LimitesIdade, R_LimitesIdade>();
      services.AddScoped<IR_Proposta, R_Proposta>();
      services.AddScoped<IR_PropostaCompleta, R_PropostaCompleta>();
      services.AddScoped<IR_Cliente, R_Cliente>();
      services.AddScoped<IR_Parametros, R_Parametros>();

      services.AddScoped<ITokenGenerator, TokenGenerator>();
      // services.AddDbContext<ContextApi>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]), ServiceLifetime.Transient);
      #endregion

      #region AutoMapper
      var autoMapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<E_Usuario, D_Usuario>().ReverseMap();
        cfg.CreateMap<E_Login, D_Login>().ReverseMap();
        cfg.CreateMap<E_Situacao, D_Situacao>().ReverseMap();
        cfg.CreateMap<E_Conveniada, D_Conveniada>().ReverseMap();
        cfg.CreateMap<E_LimitesIdade, D_LimitesIdade>().ReverseMap();
        cfg.CreateMap<E_Proposta, D_Proposta>().ReverseMap();
        cfg.CreateMap<E_PropostaCompleta, D_PropostaCompleta>().ReverseMap();
        cfg.CreateMap<E_Cliente, D_Cliente>().ReverseMap();

        cfg.CreateMap<M_InsercaoUsuario, D_Usuario>().ReverseMap();
        cfg.CreateMap<M_AtualizacaoUsuario, D_Usuario>().ReverseMap();
        cfg.CreateMap<M_Login, D_Login>().ReverseMap();
        cfg.CreateMap<M_Situacao, D_Situacao>().ReverseMap();
        cfg.CreateMap<M_LimitesIdade, D_LimitesIdade>().ReverseMap();
        cfg.CreateMap<M_Conveniada, D_Conveniada>().ReverseMap();
        cfg.CreateMap<M_InsercaoProposta, D_Proposta>().ReverseMap();
        cfg.CreateMap<M_AtualizacaoProposta, D_Proposta>().ReverseMap();
        cfg.CreateMap<M_InsercaoCliente, D_Cliente>().ReverseMap();
        cfg.CreateMap<M_AtualizacaoCliente, D_Cliente>().ReverseMap();
        cfg.CreateMap<M_InsercaoCadastro, M_InsercaoCliente>().ReverseMap();
        cfg.CreateMap<M_InsercaoCadastro, M_InsercaoProposta>().ReverseMap();
        cfg.CreateMap<M_AtualizacaoCadastro, M_AtualizacaoProposta>().ReverseMap();
        cfg.CreateMap<M_AtualizacaoCadastro, M_AtualizacaoCliente>().ReverseMap();
      });

      services.AddSingleton(autoMapperConfig.CreateMapper());
      #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        // app.UseSwagger();
        // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application v1"));
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto Bem Preparadados");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseCors();

      app.UseAuthorization();

      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
