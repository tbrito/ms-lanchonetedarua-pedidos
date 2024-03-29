﻿using Amazon.Runtime;
using Amazon.SQS;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.Database;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfraestructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMongo()
            .AddRepositories()
            .AddMessageBus(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }

    private static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services
            .AddOptions<MongoDbOptions>()
            .BindConfiguration("MongoDB")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var sp = services.BuildServiceProvider();
        var mongoDbOptions = sp.GetService<IOptions<MongoDbOptions>>().Value;

        services.AddSingleton<IMongoClient>(mc =>
        {
            return new MongoClient(mongoDbOptions.ConnectionString);
        });

        services.AddTransient(sp =>
        {
            var mongoClient = sp.GetService<IMongoClient>();

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(mongoDbOptions.ConnectionString));
            mongoClientSettings.GuidRepresentation = GuidRepresentation.Standard;

            return mongoClient.GetDatabase(mongoDbOptions.DatabaseName);
        });

        return services;
    }

    private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventProcessor, EventProcessor>();
        services.AddScoped<IMessageBusClient, AwsSqsClient>();

        services.AddSingleton<IAmazonSQS, AmazonSQSClient>(serviceProvider =>
        {
            var clientId = configuration.GetSection("AWS:ClientId").Value;
            var clientSecreat = configuration.GetSection("AWS:ClientSecret").Value;
            return new AmazonSQSClient(new BasicAWSCredentials(clientId, clientSecreat), Amazon.RegionEndpoint.USEast1);
        });
        return services;
    }
}