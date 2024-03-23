﻿using PWManager.Application.Context;
using PWManager.Data.Models;
using PWManager.Data.Persistance;
using PWManager.Domain.Entities;
using PWManager.Domain.Repositories;
using PWManager.Domain.Services.Interfaces;
using PWManager.Domain.ValueObjects;
using System;
using Microsoft.EntityFrameworkCore;
using PWManager.Application.Exceptions;
using PWManager.Data.Abstraction;
using PWManager.Data.Abstraction.Interfaces;

namespace PWManager.Data.Repositories; 

internal class SettingsRepository : ISettingsRepository {

    private readonly IHaveDataContext _dataContext;
    
    private ApplicationDbContext _dbContext => _dataContext.GetDbContext();
    private readonly IUserEnvironment _environment;
    private readonly ICryptService _cryptService;
    
    public SettingsRepository(IUserEnvironment env, ICryptService cryptService) : this(env, cryptService, HaveDataContextFactory.Create()) {}
    
    internal SettingsRepository(IUserEnvironment env, ICryptService cryptService, IHaveDataContext dataContext) {
        _environment = env;
        _cryptService = cryptService;
        _dataContext = dataContext;
    }
    
    public Settings GetSettings() {
        if (_environment.CurrentUser is null) {
            throw new UserFeedbackException("No user found! Are you in a session?");
        }
        var userId = _environment.CurrentUser.Id;
        var settingsList = _dbContext.Settings.Where(e => e.UserId == userId).AsNoTracking().ToList();
        var settingsModel = settingsList.Any() ? settingsList.First() : null;
        if (settingsModel is null) {
            settingsModel = new SettingsModel {
                UserId = _environment.CurrentUser.Id,
                Id = Guid.NewGuid().ToString(),
                MainGroupIdentifier = _cryptService.Encrypt("main")
            };
            _dbContext.Settings.Add(settingsModel);
            _dbContext.SaveChanges();
        }

        return SettingsModelToEntity(settingsModel);
    }

    public bool UpdateSettings(Settings settings) {
        if (_environment.CurrentUser is null) {
            throw new UserFeedbackException("No user found! Are you in a session?");
        }
        return UpdateSettings(SettingsEntityToModel(settings));
    }

    private bool UpdateSettings(SettingsModel model) {
        _dbContext.Settings.Update(model);
        return _dbContext.SaveChanges() > 0;
    }

    private Settings SettingsModelToEntity(SettingsModel e) {
        return new Settings(
            e.Id,
            e.Created,
            e.Updated,
            e.UserId,
            new PasswordGeneratorCriteria(e.IncludeLowerCase, e.IncludeUpperCase, e.IncludeNumeric, e.IncludeSpecial,
                e.IncludeBrackets, e.IncludeSpaces, e.MinLength, e.MaxLength),
            new ClipboardTimeoutSetting(e.TimeOutDuration),
            new MainGroupSetting(_cryptService.Decrypt(e.MainGroupIdentifier))
        );
    }

    private SettingsModel SettingsEntityToModel(Settings e) {
        return new SettingsModel {
            Id = e.Id,
            Created = e.Created,
            Updated = e.Updated,
            UserId = _environment.CurrentUser!.Id,
            IncludeLowerCase = e.PwGenCriteria.IncludeLowerCase,
            IncludeUpperCase = e.PwGenCriteria.IncludeUpperCase,
            IncludeSpaces = e.PwGenCriteria.IncludeSpaces,
            IncludeBrackets = e.PwGenCriteria.IncludeBrackets,
            IncludeNumeric = e.PwGenCriteria.IncludeNumeric,
            IncludeSpecial = e.PwGenCriteria.IncludeSpecial,
            MinLength = e.PwGenCriteria.MinLength,
            MaxLength = e.PwGenCriteria.MaxLength,
            TimeOutDuration = e.ClipboardTimeout.TimeOutDuration,
            MainGroupIdentifier = _cryptService.Encrypt(e.MainGroup.MainGroupIdentifier),
        };
    }
}