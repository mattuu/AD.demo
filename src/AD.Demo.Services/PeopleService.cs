using System;
using System.Collections.Generic;
using System.Linq;
using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AD.Demo.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly TechTestContext _context;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(ILogger<PeopleService> logger, TechTestContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PersonModel Create(CreatePersonModel model)
        {
            var entity = new People
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsAuthorised = model.IsAuthorised,
                IsEnabled = model.IsEnabled,
                IsValid = model.IsValid
            };

            foreach (var cid in model.ColourIds ?? new int[0])
                entity.FavouriteColours.Add(new FavouriteColours
                {
                    ColourId = cid
                });

            _context.Add(entity);
            _context.SaveChanges();

            return Find(entity.PersonId);
        }

        public void Delete(int id)
        {
            var entity = _context.People.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var favouriteColors = _context.FavouriteColours.Where(fc => fc.PersonId == id);
            _context.FavouriteColours.RemoveRange(favouriteColors);

            _context.Remove(entity);
            _context.SaveChanges();
        }

        public PersonModel Find(int id)
        {
            var entity = _context.People
                .Include("FavouriteColours.Colour")
                .SingleOrDefault(p => p.PersonId == id);

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var model = new PersonModel
            {
                Id = entity.PersonId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsAuthorised = entity.IsAuthorised,
                IsEnabled = entity.IsEnabled,
                IsValid = entity.IsValid,
                Colours = entity.FavouriteColours.Select(c => new ColourModel
                {
                    Id = c.ColourId,
                    Name = c.Colour.Name
                })
            };

            return model;
        }

        public IEnumerable<PersonModel> GetAll()
        {
            return _context.People
                .Include("FavouriteColours.Colour")
                .Select(p => new PersonModel
                {
                    Id = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IsAuthorised = p.IsAuthorised,
                    IsEnabled = p.IsEnabled,
                    IsValid = p.IsValid,
                    Colours = p.FavouriteColours.Select(c => new ColourModel
                    {
                        Id = c.ColourId,
                        Name = c.Colour.Name
                    })
                })
                .ToList();
        }

        public PersonModel Update(int id, UpdatePersonModel model)
        {
            var entity = _context.People.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.IsAuthorised = model.IsAuthorised;
            entity.IsEnabled = model.IsEnabled;
            entity.IsValid = model.IsValid;

            var favouriteColours = _context.FavouriteColours.Where(fc => fc.PersonId == id);
            _context.FavouriteColours.RemoveRange(favouriteColours);

            foreach (var cid in model.ColourIds ?? new int[0])
                entity.FavouriteColours.Add(new FavouriteColours
                {
                    ColourId = cid
                });

            _context.SaveChanges();

            return Find(id);
        }
    }
}