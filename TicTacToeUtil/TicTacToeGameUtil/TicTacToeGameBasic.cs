using System.Collections.Generic;
using System.Linq;

namespace TicTacToeGameUtil
{
    public class TicTacToeGameBasic
    {
        private readonly List<int> _allGamePlays;
        private List<int> _p1Plays;
        private List<int> _p2Plays;
        private int? _stateToken;
        private bool? _isTie;
        private bool? _isWin;
        private bool? _isBlock;

        public int StateToken
        {
            get
            {
                if (!_stateToken.HasValue)
                {
                    _stateToken = int.Parse(string.Join(string.Empty, AllGamePlays.Select(p => p.ToString()).ToArray()));
                }
                return _stateToken.Value;
            }
        }

        public List<int> AllGamePlays => _allGamePlays;

        public TicTacToeGameBasic(int? stateToken, int nextPlay)
        {
            _allGamePlays = new List<int>();
            if (stateToken != null)
                _allGamePlays = stateToken.ToString().Select(d => int.Parse(d.ToString())).ToList();

            _allGamePlays.Add(nextPlay);
            NextPlayer = AllGamePlays.Count % 2 != 0 ? 1 : 2;
        }
        public TicTacToeGameBasic(int fullStateToken)
        {
            _allGamePlays = fullStateToken.ToString().Select(d => int.Parse(d.ToString())).ToList();
            NextPlayer = AllGamePlays.Count % 2 != 0 ? 1 : 2;
        }

        public bool IsTie
        {
            get
            {
                if (_isTie != null) return _isTie.Value;

                if (IsWin)
                {
                    _isTie = false;
                    return _isTie.Value;
                }

                _isTie = AllGamePlays.Count == 9;


                return _isTie.Value;
            }
        }

        public bool IsWin
        {
            get
            {

                if (_isWin != null) return _isWin.Value;

                if (AllGamePlays.Count < 4)
                {
                    _isWin = false;
                    return _isWin.Value;
                }


                List<List<int>> scenarios = new List<List<int>>()
                {
                    new List<int>() {1, 2, 3},
                    new List<int>() {4, 5, 6},
                    new List<int>() {7, 8, 9},
                    new List<int>() {1, 4, 7},
                    new List<int>() {2, 5, 8},
                    new List<int>() {3, 6, 9},
                    new List<int>() {1, 5, 9},
                    new List<int>() {3, 5, 7}
                };


                //Winning scenario
                _isWin = scenarios.Any(scenario => !scenario.Except(P1Plays).Any())
                            ||
                         scenarios.Any(scenario => !scenario.Except(P2Plays).Any())
                            ;


                return _isWin.Value;

            }
        }

        public bool IsBlock
        {
            get
            {
                if (_isBlock != null) return _isBlock.Value;

                if (IsWin || AllGamePlays.Count < 4)
                {
                    _isBlock = false;
                    return _isBlock.Value;
                }


                List<List<int>> scenarios = new List<List<int>>()
                {
                    new List<int>() {1, 2, 3},
                    new List<int>() {4, 5, 6},
                    new List<int>() {7, 8, 9},
                    new List<int>() {1, 4, 7},
                    new List<int>() {2, 5, 8},
                    new List<int>() {3, 6, 9},
                    new List<int>() {1, 5, 9},
                    new List<int>() {3, 5, 7}
                };

                var nextPlayerPlays = P1Plays;
                if (NextPlayer == 1)
                    nextPlayerPlays = P2Plays;


                var blocks = scenarios.Where(s => !s.Except(AllGamePlays).Any());
                var lastPblocks = blocks.Select(b => b.Except(nextPlayerPlays))
                    .ToList();

                var lastPlay = AllGamePlays.Last();
                var isBlock = lastPblocks.Any(b =>
                {
                    var blcks = b as int[] ?? b.ToArray();
                    return blcks.Length == 1 && blcks.First() == lastPlay;
                });
                _isBlock = isBlock;

                return _isBlock.Value;

            }
        }

        public List<int> P1Plays
        {
            get { return _p1Plays ?? (_p1Plays = AllGamePlays.Where((p, i) => i % 2 == 0).ToList()); }
        }

        public List<int> P2Plays
        {
            get { return _p2Plays ?? (_p2Plays = AllGamePlays.Where((p, i) => i % 2 != 0).ToList()); }
        }

        public int NextPlayer { get; }

        public List<int> GetPossiblePlays()

        {
            var allPlays = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            return allPlays.Except(AllGamePlays).ToList();
        }

    }
}
