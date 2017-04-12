using UnityEngine;
using System.Collections;

public class BoardExample  {

    CellExample[,] board;

    public BoardExample()
    {
        board = new CellExample[Board.BOARD_SIZE, Board.BOARD_SIZE];
        CreateCells();
    }

    public BoardExample(BoardExample board)
    {
        this.board = new CellExample[Board.BOARD_SIZE, Board.BOARD_SIZE];

        for (int i = 0; i < board.GetCells().GetLength(0); i++)
            for (int j = 0; j < board.GetCells().GetLength(0); j++)
                this.board[i, j] = board.GetCells()[i, j].Clone();
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
                board[i, j].SetFigure(new FigureExample(true));

        for (int i = Board.BOARD_SIZE - 1; i >= Board.BOARD_SIZE - 3; i--)
            for (int j = Board.BOARD_SIZE - 1; j >= Board.BOARD_SIZE - 3; j--)
                board[i, j].SetFigure(new FigureExample(false));
    }

    public bool MoveFigure(CellExample from, CellExample to)
    {
        if (!from.IsFigureOnCell() ||
            !from.GetFigureOrDefault().IsPossibleMove(this, from,to))
            return false;

        var figure = from.GetFigureOrDefault();

        from.RemoveFigure();
        to.SetFigure(figure);

        return true;
    }

    public CellExample this[int row, int column]
    {
        get
        {
            if (row >= 0 && column >= 0 && row < Board.BOARD_SIZE && column < Board.BOARD_SIZE)
                return board[row, column];
            else
                throw new System.Exception("Board out of range");
        }
    }

    public CellExample[,] GetCells()
    {
        return board;
    }
}
