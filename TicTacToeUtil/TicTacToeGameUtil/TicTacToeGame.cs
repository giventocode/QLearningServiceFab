using System.Collections.Generic;
using System.Linq;
using QLearningServiceFab.Actors.Interfaces;

namespace TicTacToeGameUtil
{
    public class TicTacToeGame:TicTacToeGameBasic
    {
    
        public IEnumerable<PastState> GetAllStateSequence()
        {
            for (var i = AllGamePlays.Count; i > 0; i--)
            {
                var nextsToken = int.Parse(string.Join(string.Empty, AllGamePlays.Take(i)));
                var sToken = string.Join(string.Empty, AllGamePlays.Take(i - 1));

                if (string.IsNullOrEmpty(sToken))
                    sToken = "0";

                yield return new PastState() { NextStateToken = nextsToken, StateToken = int.Parse(sToken) };
            }
        }

        public IEnumerable<PastState> GetLastPlayersStateSequence()
        {

            for (var i = AllGamePlays.Count; i > 0; i--)
            {
                //Only assign a reward for plays that belong to the last player in the sequence
                if ((!AllGamePlays.Count.IsEven() || !i.IsEven()) && (!AllGamePlays.Count.IsOdd() || !i.IsOdd()))
                    continue;

                var nextsToken = int.Parse(string.Join(string.Empty, AllGamePlays.Take(i)));
                var sToken = string.Join(string.Empty, AllGamePlays.Take(i - 1));

                if (string.IsNullOrEmpty(sToken))
                    sToken = "0";

                yield return new PastState() { NextStateToken = nextsToken, StateToken = int.Parse(sToken) };
            }
        }

        public TicTacToeGame(int? stateToken, int nextPlay) : base(stateToken, nextPlay)
        {
        }

        public TicTacToeGame(int fullStateToken) : base(fullStateToken)
        {
        }
    }

    public class PastState : IPastState
    {
        internal PastState()
        {
        }
        public int StateToken { get; set; }
        public int NextStateToken { get; set; }
    }
}
