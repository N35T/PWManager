﻿using PWManager.Application.Context;
using PWManager.Data.Models;
using PWManager.Data.Persistance;
using PWManager.Domain.Entities;
using PWManager.Domain.Repositories;
using PWManager.Domain.Services.Interfaces;

namespace PWManager.Data.Repositories; 

public class GroupRepository : IGroupRepository {

    private readonly ApplicationDbContext _dbContext;
    private readonly ICryptService _cryptService;
    private readonly IApplicationEnvironment _environment;
    
    internal GroupRepository(ApplicationDbContext dbContext, ICryptService cryptService, IApplicationEnvironment environment) {
        _cryptService = cryptService;
        _dbContext = dbContext;
        _environment = environment;
    }
    
    public GroupRepository(ICryptService cryptService, IApplicationEnvironment environment) : this(DataContext.GetDbContext(), cryptService, environment){
    }
    
    public Group GetGroup(string groupName) {
        var groups = _dbContext.Groups.Where(e => e.UserId == _environment.UserId).ToList();
        
        var group = groups.First(e => _cryptService.Decrypt(e.IdentifierCrypt).Equals(groupName));

        if (group is null) {
            throw new ArgumentException("Could not find group with name " + groupName);
        }

        var groupEntity = GroupModelToEntity(group);
        var accounts = _dbContext.Accounts.Where(e => e.GroupId == group.Id).ToList();

        var accountEntities = accounts.Select(AccountModelToEntity).ToList();

        groupEntity.Accounts = accountEntities;

        return groupEntity;
    }

    public List<string> GetAllGroupNames() {
        var groupNames = _dbContext.Groups
            .Where(e => e.UserId == _environment.UserId)
            .Select(e => e.IdentifierCrypt)
            .ToList();

        return groupNames.Select(_cryptService.Decrypt).ToList();
    }

    public bool AddGroup(Group group) {
        var groupModel = GroupEntityToModel(group);

        _dbContext.Groups.Add(groupModel);
        return _dbContext.SaveChanges() > 0;
    }

    public bool UpdateGroup(Group group) {
        var groupModel = GroupEntityToModel(group);

        _dbContext.Groups.Update(groupModel);
        return _dbContext.SaveChanges() > 0;
    }

    public bool RemoveGroup(string groupName) {
        var group = GroupEntityToModel(GetGroup(groupName));

        _dbContext.Groups.Remove(group);
        return _dbContext.SaveChanges() > 0;
    }



    private GroupModel GroupEntityToModel(Group e) {
        return new GroupModel {
            Id = e.Id,
            Created = e.Created,
            Updated = e.Updated,
            UserId = _environment.UserId,
            IdentifierCrypt = _cryptService.Encrypt(e.Identifier),
            Accounts = e.Accounts.Select(acc => AccountEntityToModel(acc, e.Id)).ToList()
        };
    }

    private AccountModel AccountEntityToModel(Account e, string groupId) {
        return new AccountModel {
            Id = e.Id,
            Created = e.Created,
            Updated = e.Updated,
            GroupId = groupId,
            IdentifierCrypt = _cryptService.Encrypt(e.Identifier),
            PasswordCrypt = _cryptService.Encrypt(e.Password),
            LoginNameCrypt = _cryptService.Encrypt(e.LoginName)
        };
    }
    
    private Group GroupModelToEntity(GroupModel e) {
        return new Group(
            e.Id,
            e.Created,
            e.Updated,
            e.UserId,
            _cryptService.Decrypt(e.IdentifierCrypt));
    }

    private Account AccountModelToEntity(AccountModel e) {
        return new Account(
            e.Id,
            e.Created,
            e.Updated,
            _cryptService.Decrypt(e.IdentifierCrypt),
            _cryptService.Decrypt(e.LoginNameCrypt),
            _cryptService.Decrypt(e.PasswordCrypt));
    }
}