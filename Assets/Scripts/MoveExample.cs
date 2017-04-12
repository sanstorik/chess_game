using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        this.board = new BoardExample(board);
        //this.board.MoveFigure(from, to);

        moveValue = this.board.EvaluateBoardValue();
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
