using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveExample {
    CellExample from;
    CellExample to;
    HashSet<MoveExample> movesAfterThisMove;

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

    public void FindChilds(int cycle, bool isRedMove)
    {
        if (cycle == 6)
            return;

        Debug.Log("CHILDS");
        if (isRedMove)
            foreach (var move in board.GetPossibleRedMoves())
            {
                movesAfterThisMove.Add(move);
                Debug.Log(move);
            }
        else
        {
            foreach (var move in board.GetPossibleBlueMoves())
            {
                movesAfterThisMove.Add(move);
                Debug.Log(move);
            }
        }

        movesAfterThisMove.ElementAt(0).FindChilds(cycle++, !isRedMove);
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
}
