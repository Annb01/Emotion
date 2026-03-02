using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RestAPI.Models;
using RestAPI.Repositories;
using RestAPI.Services;
using RestAPI.TokenGenerator;
using System.Text;
using System.Xml.Linq;



BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDbSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(mongoSettings.ConnectionString));

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoSettings.DatabaseName);
});

builder.Services.AddScoped<IMongoCollection<NewPsychCode>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return database.GetCollection<NewPsychCode>("NewPsychCode");
});
builder.Services.AddScoped<IMongoCollection<UserNote>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return database.GetCollection<UserNote>("Notes");
});

builder.Services.AddIdentity<User, MongoIdentityRole<string>>()
    .AddMongoDbStores<User, MongoIdentityRole<string>, string>(
        mongoSettings.ConnectionString, mongoSettings.DatabaseName)
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<Psychiatrist>()
    .AddRoles<MongoIdentityRole<string>>()
    .AddMongoDbStores<Psychiatrist, MongoIdentityRole<string>, string>(
        mongoSettings.ConnectionString, mongoSettings.DatabaseName)
    .AddDefaultTokenProviders();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<IEmotionRepository, EmotionRepository>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowExpo");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Urls.Clear();
app.Urls.Add("http://0.0.0.0:5046");
app.Run();