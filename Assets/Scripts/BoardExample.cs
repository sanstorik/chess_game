using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardExample  {

    CellExample[,] board;

    public BoardExample()
    {
        Init();

        CreateCells();
        CreateDefaultFigures();
    }

    public BoardExample(BoardExample board)
    {
        Init();

        for (int i = 0; i < board.GetCells().GetLength(0); i++)
            for (int j = 0; j < board.GetCells().GetLength(0); j++)
                this.board[i, j] = board.GetCells()[i, j].Clone();
    }

    void Init()
    {
        this.board = new CellExample[Board.BOARD_SIZE, Board.BOARD_SIZE];
    }

    void CreateCells()
    {
        for (int row = 0; row < board.GetLength(0); row++)
            for (int column = 0; column < board.GetLength(0); column++)
                board[row, column] = new CellExample(row, column);
    }

    void CreateDefaultFigures()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                FigureExample figure = new FigureExample(true,i,j);
                board[i, j].SetFigure(figure);
            }

        for (int i = Board.BOARD_SIZE - 1; i >= Board.BOARD_SIZE - 3; i--)
            for (int j = Board.BOARD_SIZE - 1; j >= Board.BOARD_SIZE - 3; j--)
            {
                FigureExample figure = new FigureExample(false,i,j);
                board[i, j].SetFigure(figure);
            }
    }

    public IEnumerable<MoveExample> GetPossibleRedMoves()
    {
        foreach (var figure in GetRedMoves())
            foreach (var cell in board)
                if (figure.IsPossibleMove(this, board[figure.row, figure.column], cell))
                    yield return new MoveExample(figure.row,figure.column, cell.row,cell.column, new BoardExample(this), true);
    }

    public IEnumerable<MoveExample> GetPossibleBlueMoves()
    {
        foreach (var figure in GetBlueMoves())
            foreach (var cell in board)
                if (figure.IsPossibleMove(this, board[figure.row, figure.column],cell))
                    yield return new MoveExample(figure.row, figure.column, cell.row, cell.column, this, false);
    }

    public bool WinnerIsFound()
    {
        Debug.Log("red value = " + EvaluateBoardValue(true) + "|  blue value =" + EvaluateBoardValue(false));
        return EvaluateBoardValue(true) == 0 ||
            EvaluateBoardValue(false) == 0;
    }

    public bool MoveFigure(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        CellExample from = board[fromRow, fromColumn];
        CellExample to = board[toRow, toColumn];

        if (!from.IsFigureOnCell() ||
            !from.GetFigureOrDefault().IsPossibleMove(this,from, to))
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

    IEnumerable<FigureExample> GetRedMoves()
    {
        foreach (var cell in board)
            if (cell.IsFigureOnCell() &&
                cell.GetFigureOrDefault().IsRed())
                yield return cell.GetFigureOrDefault();
    }

    IEnumerable<FigureExample> GetBlueMoves()
    {
        foreach (var cell in board)
            if (cell.IsFigureOnCell() &&
                !cell.GetFigureOrDefault().IsRed())
                yield return cell.GetFigureOrDefault();
    }


    public float EvaluateBoardValue()
    {
        return EvaluateBoardValue(false) - EvaluateBoardValue(true);
    }

    public float EvaluateBoardValue(bool isRed)
    {
        int[,] values = {
            {10,9,8,7,5,5,5,5},
            {9,8,7,5,5,4,4,4},
            {8,7,5,5,4,3,3,3 },
            {7,5,5,4,3,2,2,2 },
            {6,5,4,3,2,1,1,1 },
            {5,4,3,2,1,0,0,0 },
            {5,4,3,2,1,0,0,0 },
            {5,4,3,2,1,0,0,0}
        };

        float value = 0;

        if (isRed)
            foreach (var figure in GetRedMoves())
                value += values[figure.row, figure.column];
        else
            foreach (var figure in GetBlueMoves())
                value += values[(Board.BOARD_SIZE - 1) - figure.row, 
                    (Board.BOARD_SIZE - 1) - figure.column];

        return value;
    }
}
