using Cinematography.Model;
using Cinematography.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinematography.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Cinematography.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _movieRepository;
        public MovieController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Movie>>> GetMovies()
        {
            return Ok(await _movieRepository.GetMoviesAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null) return BadRequest();

            return movie;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMovie([FromBody] CreateMovieViewModel movie)
        {
            ModelState.AddModelError("other", "Jakiś inny błąd");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _movieRepository.Create
            (

                new Movie
                (
                    movie.Id,
                    movie.TitlePl,
                    movie.TitleEng,
                    movie.ReleaseDate,
                    new Person(movie.Director.FirstName, movie.Director.LastName, movie.Director.BirthDate),
                    movie.Categories,
                    new Person[]
                    {
                        new Person(movie.Cast[0].FirstName, movie.Cast[0].LastName, movie.Cast[0].BirthDate),
                        new Person(movie.Cast[1].FirstName, movie.Cast[1].LastName, movie.Cast[1].BirthDate)
                    }
                )

            );

            return Created(string.Empty, null);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMovie([FromBody] Movie movie)
        {
            if (await _movieRepository.Update(movie))
                return Ok();
            else return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteMovie(Guid id)
        {
            if (await _movieRepository.Delete(id))
            return Ok();

            return BadRequest();
        }
    }
}
