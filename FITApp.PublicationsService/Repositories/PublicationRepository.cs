using System.ComponentModel.Design;
using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FITApp.PublicationsService.Repositories
{
    public class PublicationRepository(IMongoDatabase database) : IPublicationRepository
    {
        const string CollectionName = "publications";
        private readonly IMongoCollection<Publication> _collection =
            database.GetCollection<Publication>(CollectionName);
        private readonly IMongoCollection<Author> _authorsCollection =
            database.GetCollection<Author>("authors");

        public async Task CreateAsync(Publication publication)
        {
            await _collection.InsertOneAsync(publication);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _collection.DeleteOneAsync<Publication>(p => p.Id == id);
        }

        public async Task<(IEnumerable<Publication>, int)> GetByAuthorAsync(
            string authorId,
            int pageNumber,
            int pageSize
        )
        {
            int total = (int)
                await _collection.CountDocumentsAsync<Publication>(p =>
                    p.Authors.Select(a => a.Id).Contains(authorId)
                );
            var publications = await _collection
                .Find<Publication>(p => p.Authors.Select(a => a.Id).Contains(authorId))
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var authorIds = publications.SelectMany(p => p.Authors).Select(a => a.Id).Distinct();
            var authors = await _authorsCollection.Find(a => authorIds.Contains(a.Id)).ToListAsync();
            var authorsDict = authors.ToDictionary(a => a.Id);
            foreach (var publication in publications)
            {
                for (int i = 0; i < publication.Authors.Count; i++)
                {
                    if (publication.Authors[i].Id is not null)
                    {
                        var author = authorsDict[publication.Authors[i].Id];
                        publication.Authors[i].FirstName = author.FirstName;
                        publication.Authors[i].LastName = author.LastName;
                        publication.Authors[i].Patronymic = author.Patronymic;
                    }
                }
            }

            return (publications, total);
        }

        public async Task<IEnumerable<Publication>> GetBetweenDates(
            string authorId,
            DateOnly startDate,
            DateOnly endDate
        )
        {
            var publications = await _collection
                .Find<Publication>(p =>
                    p.Authors.Select(a => a.Id).Contains(authorId)
                    && p.DateOfPublication > startDate
                    && p.DateOfPublication < endDate
                )
                .ToListAsync();
            var authorIds = publications.SelectMany(p => p.Authors).Select(a => a.Id).Distinct();
            var authors = await _authorsCollection.Find(a => authorIds.Contains(a.Id)).ToListAsync();
            var authorsDict = authors.ToDictionary(a => a.Id);
            foreach (var publication in publications)
            {
                for (int i = 0; i < publication.Authors.Count; i++)
                {
                    if (publication.Authors[i].Id is not null)
                    {
                        var author = authorsDict[publication.Authors[i].Id];
                        publication.Authors[i].FirstName = author.FirstName;
                        publication.Authors[i].LastName = author.LastName;
                        publication.Authors[i].Patronymic = author.Patronymic;
                    }
                }
            }

            return publications;
        }

        public async Task<Publication> GetByIdAsync(ObjectId id)
        {
            var publication = await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            var authorIds = publication.Authors.Select(a => a.Id);
            var authors = await _authorsCollection.Find(a => authorIds.Contains(a.Id)).ToListAsync();
            var authorsDict = authors.ToDictionary(a => a.Id);
            for (int i = 0; i < publication.Authors.Count; i++)
            {
                if (publication.Authors[i].Id is not null)
                {
                    var author = authorsDict[publication.Authors[i].Id];
                    publication.Authors[i].FirstName = author.FirstName;
                    publication.Authors[i].LastName = author.LastName;
                    publication.Authors[i].Patronymic = author.Patronymic;
                }
            }

            return publication;
        }

        public async Task UpdateAsync(ObjectId id, Publication publication)
        {
            await _collection.UpdateOneAsync<Publication>(
                p => p.Id == id,
                Builders<Publication>
                    .Update.Set(p => p.Name, publication.Name)
                    .Set(p => p.Type, publication.Type)
                    .Set(p => p.Authors, publication.Authors)
                    .Set(p => p.Annotation, publication.Annotation)
                    .Set(p => p.EVersionLink, publication.EVersionLink)
                    .Set(p => p.PagesTotal, publication.PagesTotal)
                    .Set(p => p.DateOfPublication, publication.DateOfPublication)
            );
        }
    }
}
