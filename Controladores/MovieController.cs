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

namespace ChallengeDisney.Controladores
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
      

        public MovieController(IMovieRepository context1)
        {
            _movieRepository = context1;
            
        }

        [HttpGet]
        [Route("searchMovies")]
        public IActionResult GetMovies(string name, int idGenre, string order)
        {
            var movies = _movieRepository.GetMovies();

            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(x => x.Title == name).ToList();
            }

            if (idGenre != 0)
            {
                movies = movies.Where(x => x.Genres.Id == idGenre).ToList();
               
            }

            if (!string.IsNullOrEmpty(order))
            {
                if (order.ToUpper() == "ASC")
                {
                    movies = movies.OrderBy(Movie => Movie.CreationDate).ToList();
                }
                else if (order.ToUpper() == "DESC")
                {

                    movies = movies.OrderByDescending(Movie => Movie.CreationDate).ToList();
                }
                else
                {
                    return BadRequest("Los datos no han sido agregados correctamente.");
                }
            }

                //TODO: Hacer la funcion para ordenar de manera ASC o DESC
            return Ok(movies);
        }

        [HttpGet]
        [Route("movies")]
        public IActionResult GetTheMovieInfo()
        {
            var movie = _movieRepository.GetMovies();
            if (movie == null) return NotFound("Hubo un error o no existe la informacion qeu busca.");

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
        }

        [HttpGet]
        [Route("MovieDetails")]  //no anda
        public IActionResult GetMovieDetails(int id)
        {
            var movie = _movieRepository.GetMovie(id);
            if (movie == null) return NotFound("Hubo un error o no existe la informacion que busca.");

            MovieGetDetailsRequestVM movieVM = new MovieGetDetailsRequestVM
            {
                Id = movie.Id,
                Image = movie.Image,
                Title = movie.Title,
                CreationDate = movie.CreationDate,
                Qualification = movie.Qualification
            };

            if (movie.Characters.Any())
            {
                foreach (var x in movie.Characters)
                {
                    var newElement = movie.Characters.FirstOrDefault(y => y.Id == x.Id);
                    var characerVM = new CharacterGetDetailsRequestVM
                    {
                        Id = newElement.Id,
                        Image = newElement.Image,
                        Name = newElement.Name,
                        Age = newElement.Age,
                        Weight = newElement.Weight,
                        Story = newElement.Story
                    };

                    if (newElement != null) movieVM.Characters.Add(characerVM);
                }
            }
            return Ok(movieVM);
        }

        [HttpPost]
        public IActionResult Post(MoviePostRequestVM movieVM)
        {
            var movie = new Movie
            {
                Image = movieVM.Image,
                Title = movieVM.Title,
                CreationDate = movieVM.CreationDate,
                Qualification = movieVM.Qualification
            };

            _movieRepository.Add(movie);
            return Ok(movie);
        }

        [HttpPut]
        public IActionResult Put(MoviePutRequestVM seriesVM)
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

        [HttpDelete]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movieRepository.GetMovie(id);

            if (movie == null) return  NotFound($"La pelicula o serie con id: {id} no existe.");

            _movieRepository.Delete(id);
            return Ok(movie);
        }
    }
}
