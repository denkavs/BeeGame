using GameLogic.Interfaces;
using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BeeGameHost.Controllers
{
    public class GameController : ApiController
    {
        private IService beeGameService;

        public GameController(IService service)
        {
            this.beeGameService = service;
        }

        public GameInfo GetGame()
        {
            GameInfo gi = this.beeGameService.ProcessHit(0);
            return gi;
        }

        public GameInfo PostHit(int id)
        {
            GameInfo gi = this.beeGameService.ProcessHit(id);
            return gi;
        }

    }
}
