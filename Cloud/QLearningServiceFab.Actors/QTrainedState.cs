using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using QLearningServiceFab.Actors.Interfaces;

namespace QLearningServiceFab.Actors
{

public class QTrainedState : StatefulActor<QTrainedStateState>, IQTrainedState
{
 protected async override Task OnActivateAsync()
 {
     State = await ActorService.StateProvider.LoadStateAsync<QTrainedStateState>(Id, "qts") ??
                      new QTrainedStateState() { ChildrenQTrainedStates = new HashSet<int>() };

     await base.OnActivateAsync();
 }

 protected async override Task OnDeactivateAsync()
 {
     await ActorService.StateProvider.SaveStateAsync(Id, "qts", State);

     await base.OnDeactivateAsync();
 }

 [Readonly]
 public  Task AddChildQTrainedStateAsync(int stateToken, double reward)
 {
   

     if (reward < State.MaximumReward)
     {
         return Task.FromResult(true);
     }

     if (Math.Abs(reward - State.MaximumReward) < 0.10)
     {
                State.ChildrenQTrainedStates.Add(stateToken);
         return Task.FromResult(true);
     }

            State.MaximumReward = reward;
            State.ChildrenQTrainedStates.Clear();
            State.ChildrenQTrainedStates.Add(stateToken);

     return Task.FromResult(true);
 }

 [Readonly]
 public Task<List<int>> GetChildrenQTrainedStatesAsync()
 {
     return Task.FromResult(State.ChildrenQTrainedStates.ToList());
 }
}
}