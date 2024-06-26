﻿using Microsoft.Extensions.DependencyInjection;
using PWManager.Application.Abstractions.Interfaces;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Abstraction;
using PWManager.Data.Abstraction.Interfaces;
using PWManager.Data.Repositories;
using PWManager.Data.Services;
using PWManager.Data.System;
using PWManager.Domain.Repositories;

namespace PWManager.Data; 

public static class DependencyInjection {


    public static IServiceCollection AddDataServices(this IServiceCollection services) {

        var dataContext = new DataContext();
        HaveDataContextFactory.Initialize(dataContext);
        services.AddSingleton<IDataContextInitializer>(dataContext);
        services.AddSingleton<IDeleteDataContext>(dataContext);
        
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();

        services.AddTransient<ILoginService, LoginService>();
        services.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
        
        services.AddSingleton<IClipboard, Clipboard>();
        return services;
    }
}