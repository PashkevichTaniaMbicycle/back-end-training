﻿using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories.Base;

namespace GamesLib.DataAccess.Repositories;

public interface IPublisherRepository : IRepository<Publisher>
{
    Task<int> AddAsync(string title, string description);

    Task<bool> ExistById(int id);

    Task<bool> ExistByTitle(string title);

    Task<int> UpdateAsync(int id, string title, string description);
}