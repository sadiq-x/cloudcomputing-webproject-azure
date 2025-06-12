using backend_api.Context;
using backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repositories
{
    public interface ICPersonRepository
    {
        Task<List<PersonModel>?> GetPersons();
        Task<PersonModel?> CreatePerson(PersonModel t);
        Task<bool> DeletePerson(PersonModel t);
        Task<bool> UpdatePerson(PersonModel t);
    }

    public class PersonRepository : ICPersonRepository
    {
        private readonly IDbContextFactory<MasterDbContext> _readContextFactory;
        public PersonRepository(IDbContextFactory<MasterDbContext> readContextFactory)
        {
            _readContextFactory = readContextFactory;
        }
        public async Task<List<PersonModel>?> GetPersons()
        {
            var dbContext = _readContextFactory.CreateDbContext();
            var personList = await dbContext.Person.Select(list => new PersonModel
            {
                Id = list.Id,
                Name = list.Name,
                Age = list.Age,
                JobPosition = list.JobPosition,
                Gender = list.Gender,
                Location = list.Location,
                Hobby = list.Hobby
            }).ToListAsync();

            if (personList == null)
                return null;

            return personList;
        }
        public async Task<bool> DeletePerson(PersonModel t)
        {
            var dbContext = _readContextFactory.CreateDbContext();
            var deletePerson = await dbContext.Person.FirstOrDefaultAsync(a => a.Id == t.Id);

            if (deletePerson == null)
                return false;

            dbContext.Person.Remove(deletePerson);
            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePerson(PersonModel t)
        {
            var dbContext = _readContextFactory.CreateDbContext();
            var updatePerson = await dbContext.Person.FirstOrDefaultAsync(a => a.Id == t.Id);

            if (updatePerson == null)
                return false;

            updatePerson.Name = string.IsNullOrEmpty(t.Name) ? updatePerson.Name : t.Name;
            updatePerson.Age = string.IsNullOrEmpty(t.Age) ? updatePerson.Age : t.Age;
            updatePerson.JobPosition = string.IsNullOrEmpty(t.JobPosition) ? updatePerson.JobPosition : t.JobPosition;
            updatePerson.Gender = string.IsNullOrEmpty(t.Gender) ? updatePerson.Gender : t.Gender;
            updatePerson.Location = string.IsNullOrEmpty(t.Location) ? updatePerson.Location : t.Location;
            updatePerson.Hobby = string.IsNullOrEmpty(t.Hobby) ? updatePerson.Hobby : t.Hobby;

            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<PersonModel?> CreatePerson(PersonModel t)
        {
            if (t == null)
                return null;

            var dbContext = _readContextFactory.CreateDbContext();

            var person = new Person
            {
                Name = t.Name,
                Age = t.Age,
                JobPosition = t.JobPosition,
                Gender = t.Gender,
                Location = t.Location,
                Hobby = t.Hobby
            };

            dbContext.Person.Add(person);
            var personCreated = await dbContext.SaveChangesAsync();
            Console.WriteLine(personCreated);
            return t;
        }
    }
}