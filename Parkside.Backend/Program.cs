using exp.NET6.Services.DBServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parkside.Backend.Data;
using Parkside.Backend.Helpers;
using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Infrastructure.Repositories.Coaches;
using Parkside.Infrastructure.Repositories.Matches;
using Parkside.Infrastructure.Repositories.Newses;
using Parkside.Infrastructure.Repositories.Players;
using Parkside.Infrastructure.Repositories.PlayersHistories;
using Parkside.Infrastructure.Repositories.PlayersTrofees;
using Parkside.Infrastructure.Repositories.SocialMedias;
using Parkside.Infrastructure.Repositories.Sponsors;
using Parkside.Infrastructure.Repositories.Teams;
using Parkside.Infrastructure.Repositories.Trofees;
using Parkside.Models.Helpers;
using Parkside.Services.Championships;
using Parkside.Services.Coaches;
using Parkside.Services.Coachs;
using Parkside.Services.Email;
using Parkside.Services.Matches;
using Parkside.Services.Matchs;
using Parkside.Services.Newss;
using Parkside.Services.PlayerHistory;
using Parkside.Services.Players;
using Parkside.Services.PlayerTrofees;
using Parkside.Services.SocialMedias;
using Parkside.Services.Sponsors;
using Parkside.Services.Teams;
using Parkside.Services.Trofees;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("identityDatabase");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Initialiaze with your DB
builder.Services.AddDbContext<ParksideContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"))
                    );

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
// Configure expired refresh token time
_ = double.TryParse(builder.Configuration.GetSection("JWT:RefreshTokenValidityInDays").Value, out double refreshTokenValidityInDays);
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(refreshTokenValidityInDays);
    options.Name = "Default";
});

// caching
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    var cacheProfiles = builder.Configuration
            .GetSection("CacheProfiles")
            .GetChildren();
    foreach (var cacheProfile in cacheProfiles)
    {
        options.CacheProfiles
        .Add(cacheProfile.Key,
        cacheProfile.Get<CacheProfile>());
    }
});

builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// dependence injection
builder.Services.AddScoped<IBasicRegisterMethods, BasicRegisterMethods>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<ICoachRepo, CoachRepo>();
builder.Services.AddScoped<ICoachService, CoachService>();

builder.Services.AddScoped<INewsRepo, NewsRepo>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddScoped<ISocialMediaRepo, SocialMediaRepo>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();

builder.Services.AddScoped<ISponsorRepo, SponsorRepo>();
builder.Services.AddScoped<ISponsorService, SponsorService>();

builder.Services.AddScoped<ITeamRepo, TeamRepo>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<ITrofeeRepo, TrofeeRepo>();
builder.Services.AddScoped<ITrofeeService, TrofeeService>();

builder.Services.AddScoped<IChampionshipRepo, ChampionshipRepo>();
builder.Services.AddScoped<IChampionshipService, ChampionshipService>();

builder.Services.AddScoped<IMatchRepo, MatchRepo>();
builder.Services.AddScoped<IMatchService, MatchService>();

builder.Services.AddScoped<IPlayersHistoryRepo, PlayersHistoryRepo>();
builder.Services.AddScoped<IPlayerHistoryService, PlayerHistoryService>();

builder.Services.AddScoped<IPlayerTrofeeRepo, PlayerTrofeeRepo>();
builder.Services.AddScoped<IPlayerTrofeeService, PlayerTrofeeService>();

builder.Services.AddScoped<IGenericService, GenericService>();

Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
app.MapControllers();

app.Run();
