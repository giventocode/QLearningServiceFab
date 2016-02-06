namespace QLearningServiceFab.Actors.Interfaces
{
    public interface IReward
    {
        double Value { get; set; }
        double Discount { get; set; }
        bool IsAbsorbent { get; set; }
        int StateToken { get; set; }
    }
}