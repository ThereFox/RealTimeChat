using Application;
using Chat.WebAPI.Configs;
using Infrastructure.BlobStorage;
using Microsoft.EntityFrameworkCore;
using Persistense;
using SignalRTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// var minioConfig = builder.Configuration
//     .GetSection("ConnectionInfo")
//     .GetValue<BlobConfig>("Blob");
//
// var databaseConnectionString = builder
//     .Configuration
//     .GetSection("ConnectionInfo")
//     .GetValue<string>("Database");
//
// builder
//     .Services
//     .AddAppServices()
//     .AddEFPersistense(config => config.UseNpgsql(databaseConnectionString))
//     .AddMinioS3(minioConfig.AccesKey, minioConfig.PrivateKey);

var app = builder.Build();


app.MapHub<ChatHub>("/chat");

app.Run();
