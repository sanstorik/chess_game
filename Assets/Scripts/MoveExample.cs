using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveExample {
    CellExample from;
    CellExample to;
    public HashSet<MoveExample> movesAfterThisMove;

    BoardExample board;
    float moveValue;
    bool isRedMove;

    public MoveExample(CellExample from, CellExample to, BoardExample board, bool isRedMove)
    {
        movesAfterThisMove = new HashSet<MoveExample>();
        this.isRedMove = isRedMove;

        this.from = from;
        this.to = to;
        this.board = board;

        this.board.MoveFigure(from, to);

        moveValue = this.board.EvaluateBoardValue();
    }

    public HashSet<MoveExample> FindChilds(bool isRedMove)
    {
        if (board.WinnerIsFound())
        {
            Debug.Log("WINNER FOUND");
            return null;
        }
      //  Debug.Log("CHILDS");
        if (isRedMove)
            foreach (var move in board.GetPossibleRedMoves())
            {
                movesAfterThisMove.Add(move);
               // Debug.Log(move);
            }
        else
        {
            foreach (var move in board.GetPossibleBlueMoves())
            {
                movesAfterThisMove.Add(move);
              //  Debug.Log(move);
            }
        }

       // Debug.Log(movesAfterThisMove.Count);
        this.isRedMove = isRedMove;

        return movesAfterThisMove;
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
        return "from " + (from.row + 1) + " " + (from.column + 1) + "  | to " + (to.row + 1) + " " + (to.column + 1);
    }

    public override bool Equals(object obj)
    {
        MoveExample move = (MoveExample)obj;
        return move.from.column == from.column && move.from.row == from.row &&
            move.to.column == to.column && move.from.column == to.column;
    }

    public override int GetHashCode()
    {
        return from.row ^ to.row ^ from.column ^ to.column;
    }
}
