using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using ChallengeDisney.Repositorio;
using ChallengeDisney.ViewModel.Character;
using ChallengeDisney.ViewModel.Movie;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get([FromQuery] CharacterGetRequest2VM characterVM) 
        {
            try
            {
                var characters = _characterRepository.GetCharacters();

                if (!string.IsNullOrEmpty(characterVM.Name)) //filtro
                {
                    characters = characters.Where(x => x.Name == characterVM.Name).ToList();
                }

                if (characterVM.Age > 0)
                {
                    characters = characters.Where(x => x.Age == characterVM.Age).ToList();
                }

                if (characterVM.IdMovie != 0)
                {
                    characters = characters.Where(x => x.Movies.FirstOrDefault(y => y.Id == characterVM.IdMovie) != null).ToList();
                }


                if (!characters.Any()) return NoContent();

                return Ok(characters);

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }
        [HttpGet]
        [Route("characters")]
        public IActionResult GetCharactersInfo()
        {
            try
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

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }
        [HttpGet]
        [Route("CharactersDetails")]
        public IActionResult GetCharactersDetails(int id)
        {
            try
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
            catch (Exception e) 
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpPost] 
        [Route("charaters")]
        public IActionResult Post(CharacterPostRequestVM characterVM)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }
      

        [HttpPut]
        [AllowAnonymous] //Deberia ir Authorize pero tuve un error de ultimo momento que no llegue a resolver 
        public IActionResult Put(CharacterPutRequestVM characterVM)
        {
            try
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

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        public IActionResult DeleteCharacter(int id)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo {e.Message}");
            }
        }           
    }
}
