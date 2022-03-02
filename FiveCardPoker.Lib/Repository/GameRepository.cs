using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Repository
{
    interface IGameRepository
    {
        void Start();
    }
    public class GameRepository : IGameRepository
    {
        public GameRepository()
        {

        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
