using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using ChallengeDisney.Repositorio;
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
    public class CharacterController : ControllerBase
    {
       /* private readonly DisneyContext _context;
        public CharacterController(DisneyContext context)
        {
            _context = context;
        }*/

        private readonly ICharacterRepository _characterRepository;
        
        public CharacterController(ICharacterRepository context)
        {
            _characterRepository = context;
        }

        [HttpGet]
        [Route("charaterss")]
        public IActionResult Get(string name, int age, int idMovie) //TODO: no puedo filtrar por idmovie
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

            /*if (IdMovie != 0 )
            {
                characters = characters.Where(x => x.Movies.id == idMovie).ToList(); //TODO: tendira que ser Movies.
            }           
            */

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

        [HttpPost] //agrega
        [Route("charaters")]
        //Agrega cosas a lo que se especifica
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
