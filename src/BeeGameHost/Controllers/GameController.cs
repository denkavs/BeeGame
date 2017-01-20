using GameLogic.Interfaces;
using GameLogic.Model;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeeGameHost.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
