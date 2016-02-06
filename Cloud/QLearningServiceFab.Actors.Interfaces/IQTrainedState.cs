using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace QLearningServiceFab.Actors.Interfaces
{
    public interface IQTrainedState:IActor
    {
        Task AddChildQTrainedStateAsync(int stateToken, double reward);

        Task<List<int>> GetChildrenQTrainedStatesAsync();
        
    }
}