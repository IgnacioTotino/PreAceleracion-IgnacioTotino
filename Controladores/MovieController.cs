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
        private readonly ICharacterRepository _characterRepository;
     
        public MovieController(IMovieRepository context1, ICharacterRepository context2)
        {
            _movieRepository = context1;
            _characterRepository = context2;
        }

        [HttpGet]
        [Route("searchMovies")]
        public IActionResult GetMovies(string name, int idGenre, string order) //TODO cambiar y usar un view model para el parametro [FromQuery]
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
             
            return Ok(movies);
        }

        [HttpGet]
        [Route("movies")]
        public IActionResult GetTheMovieInfo()
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
        }

        [HttpGet]
        [Route("MovieDetails")]  
        public IActionResult GetMovieDetails(int id)
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

            if (movie.Characters.Any()) //TODO: comprobar lo de != null
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

            /*if (movieVM.CharactersId.Any())           //TODO: no se como poder relacionarlos
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

                
            }*/

            if (movieVM.CharactersId.Any())
            {
                foreach (var x in movieVM.CharactersId)
                {
                    var character = _characterRepository.GetCharacter(x);
                    if (character != null) movie.Characters.Add(character);
                }
            }

            _movieRepository.Add(movie);

            var culo = new MoviePostRequestVM
            {
                Image = movie.Image,
                Title = movie.Title
            };
            
            return Ok(culo);
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
