using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.ServiceFabric.Actors;
using QLearningServiceFab.Actors.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QAPI.Controllers
{
    [Route("api/[controller]")]
    public class QTrainerController : Controller
    {
        // GET: api/values
        [HttpGet()]
        [Route("[action]/{startTrans:int}")]
        public  async Task<IActionResult>  Start(int startTrans)
        {
            try
            {
                var actor = ActorProxy.Create<IQState>(ActorId.NewId(), "fabric:/QLearningServiceFab/");

                await actor.StartTrainingAsync(startTrans);

                return Ok(startTrans);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        [HttpGet()]
        [Route("[action]/{stateToken}")]
        public async Task<int> NextValue(int stateToken)
        {
            var actor = ActorProxy.Create<IQTrainedState>(new ActorId(stateToken), "fabric:/QLearningServiceFab");

            var qs = await actor.GetChildrenQTrainedStatesAsync();

            return qs[new Random().Next(0, qs.Count)];
        }

    }
}
