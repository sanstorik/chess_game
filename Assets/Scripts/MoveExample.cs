using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveExample {
    int fromRow;
    int fromColumn;
    int toRow;
    int toColumn;

    public HashSet<MoveExample> movesAfterThisMove;

    BoardExample board;
    float moveValue;
    bool isRedMove;

    public MoveExample(int fromRow, int fromColumn, int toRow, int toColumn, BoardExample board, bool isRedMove)
    {
        movesAfterThisMove = new HashSet<MoveExample>();
        this.isRedMove = isRedMove;

        this.fromRow = fromRow;
        this.fromColumn = fromColumn;
        this.toRow = toRow;
        this.toColumn = toColumn;

        this.board = board;

        this.board.MoveFigure(fromRow, fromColumn, toRow, toColumn);

        moveValue = this.board.EvaluateBoardValue();
    }

    public MoveExample FindChilds(bool isRedMove)
    {
        if (board.WinnerIsFound())
        {
            Debug.Log("WINNER FOUND");
            return null;
        }

        this.isRedMove = isRedMove;

        // Debug.Log("CHILDS");
        if (isRedMove)
        {
            foreach (var move in board.GetPossibleRedMoves())
                movesAfterThisMove.Add(move);

            return FindBestRedMove();
        }
        else
        {
            foreach (var move in board.GetPossibleBlueMoves())
                movesAfterThisMove.Add(move);

            return FindBestBlueMove();
        }

    }


    MoveExample FindBestRedMove()
    {
        var move = movesAfterThisMove.ElementAt(0);
        float max = move.GetBoardValueAfterMove();

        foreach (var newMove in movesAfterThisMove)
            if (newMove.GetBoardValueAfterMove() > max)
            {
                max = newMove.GetBoardValueAfterMove();
                move = newMove;
            }

        MoveExample bestMove = new MoveExample(move.fromRow, move.fromColumn, move.toRow, move.toColumn, new BoardExample(board), isRedMove);

        return bestMove;
    }

    MoveExample FindBestBlueMove()
    {
        var move = movesAfterThisMove.ElementAt(0);
        float min = move.GetBoardValueAfterMove();

        foreach (var newMove in movesAfterThisMove)
            if (newMove.GetBoardValueAfterMove() < min)
            {
                min = newMove.GetBoardValueAfterMove();
                move = newMove;
            }

        MoveExample bestMove = new MoveExample(move.fromRow, move.fromColumn, move.toRow, move.toColumn, new BoardExample(board), isRedMove);

        return bestMove;
    }

    public bool FindChildInChilds()
    {
        if (board.WinnerIsFound())
        {
            Debug.Log("winner");
            return true;
        }

        Debug.Log(movesAfterThisMove.Count);
        foreach (var move in movesAfterThisMove)
            move.FindChilds(!isRedMove);

        return false;
    }

    public bool IsRedMove()
    {
        return isRedMove;
    }

    public float GetBoardValueAfterMove()
    {
        return moveValue;
    }

    public override string ToString()
    {
        return "from " + ( fromRow + 1 ) + " " + ( fromColumn + 1 ) + "  | to " + ( toRow + 1 ) + " " + ( toColumn + 1 ) + "| value =" + GetBoardValueAfterMove() 
            + " | isRed = " + isRedMove;
    }

    public override bool Equals(object obj)
    {
        MoveExample move = (MoveExample)obj;
        return move.fromColumn == fromColumn && move.fromRow == fromRow &&
            move.toColumn == toColumn && move.fromColumn == fromColumn;
    }

    public override int GetHashCode()
    {
        return fromRow ^ toRow ^ fromColumn ^ toColumn;
    }
}
