using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardExample  {

    CellExample[,] board;
    HashSet<FigureExample> redFigures;
    HashSet<FigureExample> blueFigures;

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

        foreach (var figure in board.redFigures)
            redFigures.Add(figure.Clone());

        foreach (var figure in board.blueFigures)
            blueFigures.Add(figure.Clone());
    }

    void Init()
    {
        this.board = new CellExample[Board.BOARD_SIZE, Board.BOARD_SIZE];
        redFigures = new HashSet<FigureExample>();
        blueFigures = new HashSet<FigureExample>();
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
                redFigures.Add(figure);
                board[i, j].SetFigure(figure);
            }

        for (int i = Board.BOARD_SIZE - 1; i >= Board.BOARD_SIZE - 3; i--)
            for (int j = Board.BOARD_SIZE - 1; j >= Board.BOARD_SIZE - 3; j--)
            {
                FigureExample figure = new FigureExample(false,i,j);
                blueFigures.Add(figure);
                board[i, j].SetFigure(figure);
            }
    }

    public IEnumerable<MoveExample> GetPossibleRedMoves()
    {
        foreach (var figure in redFigures)
            foreach (var cell in board)
                if (figure.IsPossibleMove(this, board[figure.row, figure.column], cell))
                    yield return new MoveExample(board[figure.row,figure.column], cell.Clone(), new BoardExample(this), true);
    }

    public IEnumerable<MoveExample> GetPossibleBlueMoves()
    {
        foreach (var figure in blueFigures)
            foreach (var cell in board)
                if (figure.IsPossibleMove(this, board[figure.row, figure.column],cell))
                    yield return new MoveExample(board[figure.row, figure.column], cell.Clone(), this, false);
    }

    public bool MoveFigure(CellExample from, CellExample to)
    {
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
            {5,5,4,3,2,1,1,1 },
            {5,4,3,2,1,0,0,0 },
            {5,4,3,2,1,0,0,0 },
            {5,4,3,2,1,0,0,0}
        };

        float value = 0;

        if (isRed)
            foreach (var figure in redFigures)
                value += values[figure.row, figure.column];
        else
            foreach (var figure in blueFigures)
                value += values[(Board.BOARD_SIZE - 1) - figure.row, 
                    (Board.BOARD_SIZE - 1) - figure.column];

        return value;
    }
}
