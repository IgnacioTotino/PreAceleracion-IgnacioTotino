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
using ChallengeDisney.ViewModel.Genre;

namespace ChallengeDisney.Controladores
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository s)
        {
            _genreRepository = s;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            var genders = _genreRepository.GetGenres();
            if (genders == null) return NotFound("Hubo un error o no existe la informacion que busca.");

            var gendersVM = new List<GenreGetRequestVM>();

            foreach(var x in genders)
            {
                gendersVM.Add( new GenreGetRequestVM
                { 
                    Id = x.Id,
                    Image = x.Image,
                    Name = x.Name
                
                });
            }

            return Ok(gendersVM);
        }
        
        [HttpPost]
        public IActionResult Post(GenrePostRequestVM gendersVM)
        {
            var genders = new Genre
            {
                Name = gendersVM.Name,
                Image = gendersVM.Image
            };
            _genreRepository.Add(genders);
            return Ok(genders);
        }

        [HttpPut]
        public IActionResult Put(GenrePutRequestVM gendersVM)
        {
            var genre = _genreRepository.GetGenre(gendersVM.Id);

            if (genre == null) return NotFound($"El genero con id: {gendersVM.Id} no existe.");

            genre.Image = gendersVM.Image;
            genre.Name = gendersVM.Name;

            _genreRepository.Update(genre);

            return Ok(genre);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var genre = _genreRepository.GetGenre(id);

            if (genre == null) return NotFound($"El genero con id: {id} no existe.");

            _genreRepository.Delete(id);

            return Ok(genre);
        }
    }
}
