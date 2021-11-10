using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using ChallengeDisney.Repositorio;
using ChallengeDisney.ViewModel.Character;
using ChallengeDisney.ViewModel.Movie;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Controladores
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
     
        private readonly ICharacterRepository _characterRepository;
        
        public CharacterController(ICharacterRepository context)
        {
            _characterRepository = context;
        }

        [HttpGet]
        [Route("SearchCharacters")]
        public IActionResult Get(string name, int age, int idMovie) 
        {
            var characters = _characterRepository.GetCharacters();

            if (!string.IsNullOrEmpty(name)) //filtro
            {
                characters = characters.Where(x => x.Name == name).ToList();
            }
            
            if (age > 0)
            {
                characters = characters.Where(x => x.Age == age).ToList();
            }

            if (idMovie != 0 )
            {
                characters = characters.Where(x => x.Movies.FirstOrDefault(y => y.Id == idMovie) != null).ToList();  
            }           
            

            if (!characters.Any()) return NoContent();

            return Ok(characters);
        }
        [HttpGet]
        [Route("characters")]
        public IActionResult GetCharactersInfo()
        {
            var character = _characterRepository.GetCharacters();
            if (character == null) return NotFound("Hubo un error o no existe la informacion qeu busca.");

            var characterVM = new List<CharacterGetRequestVM>();

            foreach (var x in character)
            {
                characterVM.Add(new CharacterGetRequestVM 
                {
                    Image = x.Image,
                    Name = x.Name
                });
            }

            return Ok(characterVM);
        }
        [HttpGet]
        [Route("CharactersDetails")]
        public IActionResult GetCharactersDetails(int id)
        {
            var character = _characterRepository.GetCharacter(id);
            if (character == null) return NotFound("Hubo un error o no existe la informacion qeu busca.");

            var characterVM = new CharacterGetDetailsRequestVM
            {
                Id = character.Id,
                Image = character.Image,
                Name = character.Name,
                Age = character.Age,
                Weight = character.Weight,
                Story = character.Story
            };

            if (character.Movies.Any())
            {
                foreach (var i in character.Movies)
                {
                    var movieVM = new MovieViewModel
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Qualification = i.Qualification
                    };

                    if (i != null) characterVM.Movies.Add(movieVM);
                }
            }
            return Ok(characterVM);
        }

        [HttpPost] //agrega
        [Route("charaters")]
        public IActionResult Post(CharacterPostRequestVM characterVM)
        {
            var character = new Character
            {
                Image = characterVM.Image,
                Name = characterVM.Name,
                Age = characterVM.Age,
                Weight = characterVM.Weight,
                Story = characterVM.Story,
               
            };

            _characterRepository.Add(character);
            return Ok(character);
        }
      

        [HttpPut] //Actualiza
        public IActionResult Put(CharacterPutRequestVM characterVM)
        {
            var character = _characterRepository.GetCharacter(characterVM.Id);

            if (character == null) return NotFound($"El personaje con id: {characterVM.Id} no existe.");

            character.Image = characterVM.Image;
            character.Name = characterVM.Name;
            character.Age = characterVM.Age;
            character.Weight = characterVM.Weight;
            character.Story = characterVM.Story;

            _characterRepository.Update(character);

            return Ok(character);
        }

        [HttpDelete]
        public IActionResult DeleteCharacter(int id)
        {
            var character = _characterRepository.GetCharacter(id);

            if (character == null)
            {
                return NotFound($"El personaje con id: {id} no existe.");
            }
            else
            {
                _characterRepository.Delete(id);
                return Ok(character);
            }
        }           
    }
}
