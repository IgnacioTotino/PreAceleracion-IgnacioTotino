using ChallengeDisney.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Interfaces
{
    public interface ICharacterRepository : IBaseRepository<Character>
    {
        Character GetCharacter(int id);
        List<Character> GetCharacters();
    }
}
