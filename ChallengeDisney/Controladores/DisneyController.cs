using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Controladores
{
    [ApiController]
    [Route("api/ [Controller]")]
    public class DisneyController : ControllerBase
    {
        private readonly MoviesContext _context;
        public DisneyController(MoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("charaters")]
        public IActionResult Get(string name, int age, string IdMovie)
        {
            var characters = _context.Charaters.ToList();

            if (!string.IsNullOrEmpty(name)) //filtro
            {
                characters = characters.Where(x => x.Name == name).ToList();
            }
            
            //characters = characters.Where(x => x.Age == age).ToList();

            if (!string.IsNullOrEmpty(IdMovie))
            {
                characters = characters.Where(x => x.MoviesAssociated == IdMovie).ToList();
            }

            return Ok(characters);
        }

        [HttpGet]
        [Route("PruebaMovies")]
        public IActionResult GetMovies()
        {
            var movies = _context.Movies.ToList();

            return Ok(movies);
        }

        [HttpPost]
      
        //Actualiza o agrega cosas a lo que se especifica
        public IActionResult Post(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Charaters.FirstOrDefault(x => x.Id == id) == null)
            {
                return BadRequest("El id del personaje no existe.");
            }
            else
            {
                var x = _context.Charaters.Find(id);
                _context.Charaters.Remove(x);
                _context.SaveChanges();
                return Ok(_context.Charaters.ToList());
            }
            
        }
        
        public class Test<T>
        {
            public T Data { get; set; }
        }
    }
}
