using System.Collections.Generic;
using Microsoft.ServiceFabric.Actors;
using QLearningServiceFab.Actors.Interfaces;
using TicTacToeGameUtil;

namespace QLearningServiceFab.Actors
{
    [Reentrant(ReentrancyMode.LogicalCallContext)]
    public class QTicTacToeState : QState
    {
        internal override IReward GetReward(int? previousStateToken, int transitionValue)
        {
            var game = new TicTacToeGame(previousStateToken, transitionValue);
            IReward rwd = null;

            if (game.IsBlock)
            {
                rwd = new TicTacToeReward() { Discount = .5, Value = 95, IsAbsorbing = false, StateToken = game.StateToken };
            }
            if (game.IsWin)
            {
                rwd = new TicTacToeReward() { Discount = .9, Value = 100, IsAbsorbing = true, StateToken = game.StateToken };
            }
            if (game.IsTie)
            {
                rwd = new TicTacToeReward() { Discount = .9, Value = 50, IsAbsorbing = true, StateToken = game.StateToken };
            }
            return rwd;
        }

        internal override IEnumerable<int> GetTransitions(int stateToken)
        {
            var game = new TicTacToeGame(stateToken);

            return game.GetPossiblePlays();
        }

        internal override IEnumerable<IPastState> GetRewardingQStates(int stateToken)
        {
            var game = new TicTacToeGame(stateToken);

            if (game.IsTie)
                return game.GetAllStateSequence();

            return game.GetLastPlayersStateSequence();
        }
    }
}
