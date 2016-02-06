using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace QLearningServiceFab.Actors.Interfaces
{
    public interface IQState : IActor
    {

        Task StartTrainingAsync(int initialTransitionValue);

        Task TransitionAsync(int? previousStateToken, int transitionValue);
    }
}
