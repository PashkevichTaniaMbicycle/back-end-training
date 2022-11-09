﻿using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    private readonly GamesLibContext _context;

    public GameRepository(GamesLibContext context) : base(context)
    {
        _context = context;
    }

    protected override Game CreateEntity(int id)
    {
        return new Game { Id = id };
    }

    public override async Task<ICollection<Game>> GetAsync()
    {
        var query = _context.Games
            .Select(x => new Game
            {
                Id = x.Id,
                Dev = x.Dev,
                Publisher = x.Publisher,
                ReleaseDate = x.ReleaseDate,
                Rating = x.Rating,
                Title= x.Title,
                Description= x.Description,
            });
            
        var result = await query.ToListAsync();
        
        return result;
    }
    
    public async Task<ICollection<Game>> GetAllGamesAsync()
    {
        var query = _context.Games
            .Select(x => new Game
            {
                Id = x.Id,
                Dev = new Dev
                {
                    Title = x.Dev.Title,
                },
                Publisher = new Publisher
                {
                    Title = x.Publisher.Title,
                },
                Rating = x.Rating,
                ReleaseDate = x.ReleaseDate,
                Title= x.Title,
                Description = x.Description,
            });
            
        var result = await query.ToListAsync();
        
        return result;
    }

    public async Task<int> AddAsync(
        int devId, 
        int publisherId, 
        DateTime releaseDate, 
        int rating, 
        string title,
        string description
        )
    {
        var game = new Game
        {
            Dev = new Dev {Id = devId},
            Publisher = new Publisher {Id = publisherId},
            ReleaseDate = releaseDate,
            Rating = rating,
            Title = title,
            Description = description,
        };
       
        _context.Attach(game.Dev);
        _context.Attach(game.Publisher);
        var result = await AddAsync(game);
        _context.Entry(game.Dev).State = EntityState.Detached;
        _context.Entry(game.Publisher).State = EntityState.Detached;
        
        return result.Id;
    }
}