using System.Runtime.Serialization;
using QLearningServiceFab.Actors.Interfaces;

namespace QLearningServiceFab.Actors
{
    [DataContract]
    public class TicTacToeReward:IReward
    {
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public double Discount { get; set; }
        [DataMember]
        public bool IsAbsorbing { get; set; }
        [DataMember]
        public int StateToken { get; set; }
    }
}