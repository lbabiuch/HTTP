using Cinematography.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Cinematography.Infrastructure
{
    public class MovieRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MovieRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ICollection<Movie>> GetMoviesAsync() => await _databaseContext.Database.GetCollection<Movie>("Movies")
                                                                                            .AsQueryable()
                                                                                            .ToListAsync();

        public async Task<Movie> GetMovieAsync(Guid id) => await _databaseContext.Database.GetCollection<Movie>("Movies")
                                                                                     .AsQueryable()
                                                                                     .FirstOrDefaultAsync(x => x.Id == id);

        public async Task Create(Movie movie) 
        {
           await _databaseContext.Database
                .GetCollection<Movie>("Movies")
                .InsertOneAsync(movie);
        }
        public async Task<bool> Update(Movie movie)
        {
            var entity = await GetMovieAsync(movie.Id);

            if (entity == null) return false;

            entity.TitlePl = movie.TitlePl;
            entity.TitleEng = movie.TitleEng;
            entity.ReleaseDate = movie.ReleaseDate;
            entity.Categories = movie.Categories;
            entity.Director = movie.Director;
            entity.Cast = movie.Cast;

            await _databaseContext.Database.GetCollection<Movie>("Movies").ReplaceOneAsync(x => x.Id == movie.Id, movie);

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _databaseContext.Database.GetCollection<Movie>("Movies").DeleteOneAsync(x => x.Id == id);

            return result.DeletedCount > 0;
        }
    }
}
