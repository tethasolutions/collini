﻿using Collini.GestioneInterventi.Application.Activities.Services;
using Collini.GestioneInterventi.Application.Customers.Services;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Application.Notes.Services;
using Collini.GestioneInterventi.Application.Orders.Services;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Session;
using Microsoft.Extensions.DependencyInjection;

namespace Collini.GestioneInterventi.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication<TAccessTokenProvider>(this IServiceCollection services)
        where TAccessTokenProvider : class, IAccessTokenProvider
    {
        services
            .AddScoped<ISecurityContextFactory, SecurityContextFactory>()
            .AddScoped<ISecurityService, SecurityService>()
            .AddScoped<IAccessTokenProvider, TAccessTokenProvider>()
            .AddScoped<IContactService, ContactService>()
            .AddScoped<IAddressService, AddressService>()
            .AddScoped<INotesService, NoteService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IActivityService, ActivityService>()
            .AddScoped<IJobService, JobService>();

        return services;
    }
}