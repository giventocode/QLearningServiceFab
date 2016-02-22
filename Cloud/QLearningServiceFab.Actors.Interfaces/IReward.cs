namespace QLearningServiceFab.Actors.Interfaces
{
    public interface IReward
    {
        double Value { get; set; }
        double Discount { get; set; }
        bool IsAbsorbing { get; set; }
        int StateToken { get; set; }
    }
}