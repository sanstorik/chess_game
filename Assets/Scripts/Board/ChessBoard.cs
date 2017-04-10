namespace Shvetsov_Int_knowl_lab_4
{
    class ChessBoard
    {
        static ChessBoard instance;
        static BoardCell[,] board;
        readonly int BOARD_SIZE;
        static int cellIndex = 0;

        private ChessBoard(int size)
        {
            BOARD_SIZE = size;
            board = new BoardCell[size, size];
        }

        public static void CreateBoard(int boardSize)
        {
            if (instance != null)
                return;


            instance = new ChessBoard(boardSize);

            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    board[row, column] = new BoardCell(row, column, !CheckIfCellShouldBeBlack());
                    cellIndex++;
                }

                if(boardSize % 2 == 0)
                  cellIndex++;
            }
        } 

        public static ChessBoard INSTANCE
        {
            get { return instance; }
        }

        public int BoardSize
        {
            get { return BOARD_SIZE; }
        }

        public BoardCell this[int row,int column]
        {
            get
            {
                if (row >= 0 && column >= 0 && row < BOARD_SIZE && column < BOARD_SIZE)
                    return board[row, column];
                else
                    return null;
            }
        }

        static bool CheckIfCellShouldBeBlack()
        {
            return (cellIndex + 1) % 2 == 0;
        }

    }
}
