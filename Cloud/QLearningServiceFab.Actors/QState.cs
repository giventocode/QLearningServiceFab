using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLearningServiceFab.Actors.Interfaces;

namespace QLearningServiceFab.Actors
{
public abstract class QState : StatefulActor, IQState, IRemindable
{

 internal abstract IReward GetReward(int? previousStateToken, int transitionValue);
 internal abstract IEnumerable<int> GetTransitions(int stateToken);
 internal abstract IEnumerable<IPastState> GetRewardingQStates(int stateToken);

 
 public Task StartTrainingAsync(int initialTransitionValue)
 {
     return RegisterReminderAsync("StartTransition",
         Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { TransitionValue = initialTransitionValue }))
         , TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(-1), ActorReminderAttributes.Readonly);
 }

 public Task TransitionAsync(int? previousStateToken, int transitionValue)
 {
     var rwd = GetReward(previousStateToken, transitionValue);
     

     var stateToken = transitionValue;
     if (previousStateToken != null)
         stateToken = int.Parse(previousStateToken.Value + stateToken.ToString());
   

     var ts = new List<Task>();

     if (rwd == null || !rwd.IsAbsorbent)
         ts.AddRange(GetTransitions(stateToken).Select(p => ActorProxy.Create<IQState>(ActorId.NewId(), "fabric:/QLearningServiceFab").TransitionAsync(stateToken, p)));

     if (rwd != null)
         ts.Add(RegisterReminderAsync("SetReward", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rwd))
             , TimeSpan.FromMilliseconds(0)
             , TimeSpan.FromMilliseconds(-1), ActorReminderAttributes.Readonly));


         return Task.WhenAll(ts);
   }
   
public Task SetRewardAsync(int stateToken, double stateReward, double discount)
 {
     var t = new List<Task>();

     foreach (var pastState in GetRewardingQStates(stateToken))
     {
         var reward = stateReward;
         t.Add(ActorProxy
             .Create<IQTrainedState>(new ActorId(pastState.StateToken), "fabric:/QLearningServiceFab")
             .AddChildQTrainedStateAsync(pastState.NextStateToken, reward));

         stateReward = stateReward * discount;
     }

     return Task.WhenAll(t);

 }

public async Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
 {
     await UnregisterReminderAsync(GetReminder(reminderName));

     var state = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(context));

     if (reminderName == "SetReward")
     {
         await SetRewardAsync(state["StateToken"].ToObject<int>(), state["Value"].ToObject<double>(), state["Discount"].ToObject<double>());
     }

     if (reminderName == "StartTransition")
     {
         await TransitionAsync(null, state["TransitionValue"].ToObject<int>());
     }
 }
    }
}
