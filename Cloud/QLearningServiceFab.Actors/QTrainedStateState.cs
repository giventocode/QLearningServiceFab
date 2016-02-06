using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QLearningServiceFab.Actors
{
 [DataContract]
 public class QTrainedStateState
 {
     [DataMember]
     public double MaximumReward { get; set; }

     [DataMember]
     public HashSet<int> ChildrenQTrainedStates { get; set; }
 }
}