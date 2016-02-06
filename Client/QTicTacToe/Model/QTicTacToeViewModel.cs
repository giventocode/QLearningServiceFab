using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TicTacToeGameUtil;

namespace QTicTacToe.Model
{
    public class QTicTacToeViewModel : Bindable
    {

        private readonly string[] _cellValues = new string[9];
        private string _gameStatus;


        private string _displayChar;
               
        private readonly QService _qService = new QService();


        private string DisplayChar
        {
            get
            {
                var newValue = "X";

                if (_displayChar == "O")
                    newValue = "X";

                if (_displayChar == "X")
                    newValue = "O";
                
                _displayChar = newValue;
                return _displayChar;
            }
        }


        public string Cell1
        {
            get { return _cellValues[0]; }
            private set { SetProperty(ref _cellValues[0], value, "Cell1"); }
        }

        public string Cell2
        {
            get { return _cellValues[1]; }
            private set { SetProperty(ref _cellValues[1], value, "Cell2"); }
        }

        public string Cell3
        {
            get { return _cellValues[2]; }
            private set { SetProperty(ref _cellValues[2], value, "Cell3"); }
        }

        public string Cell4
        {
            get { return _cellValues[3]; }
            private set { SetProperty(ref _cellValues[3], value, "Cell4"); }
        }

        public string Cell5
        {
            get { return _cellValues[4]; }
            private set { SetProperty(ref _cellValues[4], value, "Cell5"); }
        }

        public string Cell6
        {
            get { return _cellValues[5]; }
            private set { SetProperty(ref _cellValues[5], value, "Cell6"); }
        }

        public string Cell7
        {
            get { return _cellValues[6]; }
            private set { SetProperty(ref _cellValues[6], value, "Cell7"); }
        }

        public string Cell8
        {
            get { return _cellValues[7]; }
            private set { SetProperty(ref _cellValues[7], value, "Cell8"); }
        }

        public string Cell9
        {
            get { return _cellValues[8]; }
            private set { SetProperty(ref _cellValues[8], value, "Cell9"); }
        }

        public string GameStatus
        {
            get { return _gameStatus; }
            set { SetProperty(ref _gameStatus, value, "GameStatus"); }
        }

        public async Task PlayAsync(int cellId)
        {
            GameStatus = _qService.GetStatus(cellId);
            
            SetCell(cellId);

            if (!string.IsNullOrEmpty(GameStatus))
                return;

            var machinePlay =  await _qService.GetSuggestedPlayAsync(cellId);
            
            if (machinePlay > 0)
            SetCell(machinePlay);

            GameStatus = _qService.GetStatus();

        }

        public void Reset(string value)
        {
            Cell1 = string.Empty;
            Cell2 = string.Empty;
            Cell3 = string.Empty;
            Cell4 = string.Empty;
            Cell5 = string.Empty;
            Cell6 = string.Empty;
            Cell7 = string.Empty;
            Cell8 = string.Empty;
            Cell9 = string.Empty;

            
            GameStatus = "";
            _displayChar = string.Empty;
            _qService.ResetGame();

        }

        public void SetCell(int cellId)
        {
            switch (cellId)
            {
                case 1:
                    Cell1 = DisplayChar;
                    break;

                case 2:
                    Cell2 = DisplayChar;
                    break;

                case 3:
                    Cell3 = DisplayChar;
                    break;

                case 4:
                    Cell4 = DisplayChar;
                    break;

                case 5:
                    Cell5 = DisplayChar;
                    break;

                case 6:
                    Cell6 = DisplayChar;
                    break;

                case 7:
                    Cell7 = DisplayChar;
                    break;

                case 8:
                    Cell8 = DisplayChar;
                    break;

                case 9:
                    Cell9 = DisplayChar;
                    break;
            }

        }

        internal async Task FirstPlayAsync()
        {
            SetCell(await _qService.PlayFirstAsync());
                       
        }
    }

    public class QService
    {
        private string _stateToken;
        private readonly Random _random= new Random();
        private bool _isMachineFirst;
        
        public Task<int> PlayFirstAsync()
        {
            _isMachineFirst = true;
            return GetSuggestedPlayAsync(0);
            
        }

        public void ResetGame()
        {
            _stateToken = string.Empty;
            _isMachineFirst = false;
        }

        public async Task<int> GetSuggestedPlayAsync(int cellId)
        {
            _stateToken = _stateToken + cellId;
            using (var client = new HttpClient())
            {
                var r = new Uri(App.QServiceUrl, _stateToken);
                _stateToken = await client.GetStringAsync(r);
            }

            return int.Parse(_stateToken.Last().ToString());

        }
        public string GetStatus()
        {
            var game = new TicTacToeGameBasic (int.Parse( _stateToken));

            return GetStatusImpl(game);
        }

        private string GetStatusImpl(TicTacToeGameBasic game)
        {
            if (game.IsWin)
            {
                var winner = "You ";

                if ((_isMachineFirst && game.AllGamePlays.Count%2 != 0)
                    ||
                    (!_isMachineFirst && game.AllGamePlays.Count%2 == 0)
                    )
                {
                    winner = "Machine ";
                }

                return winner + "Win!";
            }

            if (game.IsTie)
            {
                return "Tie!";
            }

            return String.Empty;
        }

        public string GetStatus(int nextPlay)
        {
            int? t=null;
            if (!string.IsNullOrEmpty( _stateToken))            
                t = int.Parse(_stateToken);
                        
            var game = new TicTacToeGameBasic(t, nextPlay );

            return GetStatusImpl(game);
        }
    }       
}
