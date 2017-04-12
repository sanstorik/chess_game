using UnityEngine;
using System.Collections;

public class BoardExample  {

    CellExample[,] board;

    public BoardExample()
    {
        board = new CellExample[Board.BOARD_SIZE, Board.BOARD_SIZE];
        CreateCells();
    }

    void CreateCells()
    {
        for (int row = 0; row < board.GetLength(0); row++)
            for (int column = 0; column < board.GetLength(0); column++)
                board[row, column] = new CellExample(row, column);
    }

    public void CreateDefaultFigures()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j].SetFigure();

        for (int i = Board.BOARD_SIZE - 1; i >= Board.BOARD_SIZE - 3; i--)
            for (int j = Board.BOARD_SIZE - 1; j >= Board.BOARD_SIZE - 3; j--)
                board[i, j].SetFigure();
    }

    public void MoveFigure(CellExample from, CellExample to)
    {
        from.RemoveFigure();
        to.SetFigure();
    }
}
