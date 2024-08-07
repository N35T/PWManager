﻿using Microsoft.EntityFrameworkCore;
using PWManager.Application.Exceptions;
using PWManager.Data.Abstraction;
using PWManager.Data.Abstraction.Interfaces;
using PWManager.Data.Models;
using PWManager.Data.Persistance;
using PWManager.Domain.Entities;
using PWManager.Domain.Repositories;
using PWManager.Domain.Services.Interfaces;

namespace PWManager.Data.Repositories; 

public class UserRepository : IUserRepository {

    private readonly IHaveDataContext _dataContext;
    private ApplicationDbContext _dbContext => _dataContext.GetDbContext();
    private readonly ICryptService _cryptService;

    public UserRepository(ICryptService cryptService) : this(cryptService, HaveDataContextFactory.Create()) {}
    
    internal UserRepository(ICryptService cryptService, IHaveDataContext dataContext) {
        _cryptService = cryptService;
        _dataContext = dataContext;
    }

    public User AddUser(string username, string password) {
        var salt = _cryptService.GenerateSalt();
        var passwordHash = _cryptService.Hash(password, salt);

        var userModel = new UserModel {
            Id = Guid.NewGuid().ToString(),
            UserName = username,
            MasterHash = passwordHash,
            Salt = salt,
            Created = DateTimeOffset.Now,
            Updated = DateTimeOffset.Now
        };
        _dbContext.Users.Add(userModel);
        var res = _dbContext.SaveChanges();
        if (res == 0) {
            throw new UserFeedbackException("Failed creating the user");
        }

        return UserModelToEntity(userModel);
    }

    public User? CheckPasswordAttempt(string username, string password) {
        var user = _dbContext.Users.AsNoTracking().Where(e => e.UserName == username).FirstOrDefault();

        if(user is null) {
            throw new UserFeedbackException("No such User found.");
        }

        var hash = _cryptService.Hash(password, user.Salt);

        if(hash.Equals(user.MasterHash)) {
            return UserModelToEntity(user);
        }
        return null;
    }

    private User UserModelToEntity(UserModel e) {
        return new User(e.Id, e.Created, e.Updated, e.UserName);
    }
}