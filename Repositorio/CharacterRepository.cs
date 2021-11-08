using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Repositorio
{
    public class CharacterRepository : BaseRepository<Character, DisneyContext>, ICharacterRepository
    {
        public CharacterRepository(DisneyContext dbContext) : base(dbContext)
        {

        }

        public Character GetCharacter(int id)
        {
            return DbSet.Include(x => x.Movies).FirstOrDefault(y => y.Id == id);
        }

        public List<Character> GetCharacters() //especificar cada relacion por que entity no lo levanta
        {
            return DbSet.Include(x => x.Movies).ThenInclude(y => y.Genres).ToList();
        }

    }
}
