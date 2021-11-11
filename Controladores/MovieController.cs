using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using ChallengeDisney.Repositorio;
using ChallengeDisney.ViewModel.Movie;
using ChallengeDisney.ViewModel.Character;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ChallengeDisney.Controladores
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICharacterRepository _characterRepository;
     
        public MovieController(IMovieRepository context1, ICharacterRepository context2)
        {
            _movieRepository = context1;
            _characterRepository = context2;
        }

        [HttpGet]
        [Route("searchMovies")]
        public IActionResult GetMovies([FromQuery]MovieGetRequest2VM movieVM) 
        {
            try
            {
                var movies = _movieRepository.GetMovies();

                if (!string.IsNullOrEmpty(movieVM.Name))
                {
                    movies = movies.Where(x => x.Title == movieVM.Name).ToList();
                }

                if (movieVM.GenreId != 0)
                {
                    movies = movies.Where(x => x.Genres.Id == movieVM.GenreId).ToList();

                }           

                if (!string.IsNullOrEmpty(movieVM.order))
                {
                    if (movieVM.order.ToUpper() == "ASC")
                    {
                        movies = movies.OrderBy(Movie => Movie.CreationDate).ToList();
                    }
                    else if (movieVM.order.ToUpper() == "DESC")
                    {

                        movies = movies.OrderByDescending(Movie => Movie.CreationDate).ToList();
                    }
                    else
                    {
                        return BadRequest("Los datos no han sido agregados correctamente.");
                    }
                }
                return Ok(movies);
            }
            catch (Exception  e)
            {
                return StatusCode(500,$"Hubo un error {e.Message}");
            }
        }

        [HttpGet]
        [Route("movies")]
        public IActionResult GetTheMovieInfo()
        {
            try
            {
                var movie = _movieRepository.GetMovies();
                if (movie == null) return NotFound("Hubo un error o no existe la informacion que busca.");

                var movieVM = new List<MovieGetRequestVM>();

                foreach (var x in movie)
                {
                    movieVM.Add(new MovieGetRequestVM
                    {
                        Image = x.Image,
                        Title = x.Title,
                        CreationDate = x.CreationDate

                    });
                }
                return Ok(movieVM);

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpGet]
        [Route("MovieDetails")]  
        public IActionResult GetMovieDetails(int id)
        {
            try
            {
                var movie = _movieRepository.GetMovie(id);
                if (movie == null) return NotFound("Hubo un error o no existe la informacion que busca.");

                var movieVM = new MovieGetDetailsRequestVM
                {
                    Id = movie.Id,
                    Image = movie.Image,
                    Title = movie.Title,
                    CreationDate = movie.CreationDate,
                    Qualification = movie.Qualification
                };

                if (movie.Characters.Any())
                {
                    foreach (var i in movie.Characters)
                    {
                        var characerVM = new CharacterGetDetailsRequestVM
                        {
                            Id = i.Id,
                            Image = i.Image,
                            Name = i.Name,
                            Age = i.Age,
                            Weight = i.Weight,
                            Story = i.Story
                        };

                        if (i != null) movieVM.Characters.Add(characerVM);
                    }
                }
                return Ok(movieVM);

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(MoviePostRequestVM movieVM)
        {
            try
            {
                var movie = new Movie
                {
                    Image = movieVM.Image,
                    Title = movieVM.Title,
                    CreationDate = movieVM.CreationDate,
                    Qualification = movieVM.Qualification
                };

                if (movieVM.CharactersId.Any())
                {
                    var characterList = _characterRepository.GetCharacters();

                    if (characterList.Any())
                    {
                        if (movie.Characters == null) movie.Characters = new List<Character>();


                        foreach (var y in movieVM.CharactersId)
                        {
                            var item = characterList.FirstOrDefault(i => i.Id == y);
                            if (item != null) movie.Characters.Add(item);
                        }
                    }
                }               
                return Ok(_movieRepository.Add(movie));

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpPut]
        [AllowAnonymous] //Deberia ir Authorize pero tuve un error de ultimo momento que no llegue a resolver 
        public IActionResult Put(MoviePutRequestVM seriesVM)
        {
            try
            {
                var movie = _movieRepository.GetMovie(seriesVM.Id);

                if (movie == null) return NotFound($"La pelicula o serie con id: {seriesVM.Id} no existe.");

                movie.Image = seriesVM.Image;
                movie.Title = seriesVM.Title;
                movie.CreationDate = seriesVM.CreationDate;
                movie.Qualification = seriesVM.Qualification;



                _movieRepository.Update(movie);
                return Ok(movie);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                var movie = _movieRepository.GetMovie(id);

                if (movie == null) return NotFound($"La pelicula o serie con id: {id} no existe.");

                _movieRepository.Delete(id);
                return Ok(movie);

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }
    }
}
