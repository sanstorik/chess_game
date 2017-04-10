using System;

namespace Assets.Scripts.Board
{
    static class GameData
    {
        public static int whiteFiguresRemoved = 0;
        public static int blackFiguresRemoved = 0;

        static public bool isGameEnded = false;
        static public bool isWhiteTurn = true;

        public static int turnsPassed = 0;

        /// <summary>
        /// Check if White Figures have Won
        /// </summary>
        static public event Action<bool> OnGameEndedEvent;
        /// <summary>
        /// bool - If turn goes to white
        /// </summary>
        static public event Action<bool> OnChangeTurnEvent;

        static public void EndGame(bool whiteFiguresWon)
        {
            isGameEnded = true;
            OnGameEndedEvent(whiteFiguresWon);
        }

        static public void ChangeTurn()
        {
            //isWhiteTurn = !isWhiteTurn;
            //turnsPassed++;
            //OnChangeTurnEvent(isWhiteTurn);
        }
    }
}
