using AutoMapper;
using Fiap.Api.Donation4;
using Fiap.Api.Donation4.Data;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Fiap.Api.Donation3.ViewModel;
using Fiap.Api.Donation3;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

#region CORS
builder.Services.AddCors( options =>
{
    options.AddPolicy("AllAccess", policy => {
        //policy.WithOrigins(new[] { "fiap.com.br" , "flavio.com.br" });
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
#endregion


// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging()
);

#region Repository
builder.Services.AddScoped<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
#endregion


#region autenticacao
var secretToken = Encoding.UTF8.GetBytes(Settings.SECRET_TOKEN);

bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
{
    if (expires != null)
    {
        return expires > DateTime.UtcNow;
    }
    return false;
}


builder.Services
    .AddAuthentication( a =>
    {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer( opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = "fiap",
            IssuerSigningKey = new SymmetricSecurityKey(secretToken),
            //RequireExpirationTime = true,
            //LifetimeValidator = CustomLifetimeValidator
        };

    });
#endregion


#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(m =>
{
    m.AllowNullCollections = true;
    m.AllowNullDestinationValues = true;

    m.CreateMap<UsuarioModel, LoginRequestViewModel>();
    m.CreateMap<LoginRequestViewModel, UsuarioModel>();

    m.CreateMap<UsuarioModel, LoginResponseViewModel>();
    m.CreateMap<LoginResponseViewModel, UsuarioModel>();

    m.CreateMap<CategoriaModel, CategoriaResponseViewModel>();
    m.CreateMap<CategoriaRequestViewModel, CategoriaModel>();

    m.CreateMap<UsuarioModel, UsuarioResponseViewModel>();
    m.CreateMap<UsuarioRequestViewModel, UsuarioModel>();

    m.CreateMap<UsuarioModel, UsuarioPatchViewModel>();
    m.CreateMap<UsuarioPatchViewModel, UsuarioModel>();


    m.CreateMap<ProdutoRequestViewModel, ProdutoModel>();

    m.CreateMap<ProdutoPatchViewModel, ProdutoModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    m.CreateMap<ProdutoModel, ProdutoPatchViewModel>();


    m.CreateMap<ProdutoModel, ProdutoResponseViewModel>()
            .ForMember(dest => dest.NomeCategoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.NomeCategoria : string.Empty))
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.NomeUsuario : string.Empty));


    m.CreateMap<TrocaRequestViewModel, TrocaModel>();

    m.CreateMap<TrocaModel, TrocaResponseViewModel>()
        .ForMember(dest => dest.NomeProdutoMeu, opt => opt.MapFrom(src => src.ProdutoMeu.Nome))
        .ForMember(dest => dest.NomeProdutoEscolhido, opt => opt.MapFrom(src => src.ProdutoEscolhido.Nome));

});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


#region versao
builder.Services.AddApiVersioning(options =>
{
    options.UseApiBehavior = false;
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(3, 0);
    options.ApiVersionReader =
        ApiVersionReader.Combine(
            new HeaderApiVersionReader("x-api-version"),
            new QueryStringApiVersionReader(),
            new UrlSegmentApiVersionReader());
});

builder.Services.AddVersionedApiExplorer(setup => {
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

app.UseApiVersioning();
// Ajustando versionamento no Swagger
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Ajustando versionamento no Swagger
    app.UseSwaggerUI(c =>
    {
        foreach (var d in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint(
                $"/swagger/{d.GroupName}/swagger.json",
                d.GroupName.ToUpperInvariant());
        }

        c.DocExpansion(DocExpansion.List);
    });
}

app.UseCors("AllAccess");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
