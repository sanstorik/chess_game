using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TracingAlgorythm {
    static TracingAlgorythm instance;
    List<MoveExample> moves;

    private TracingAlgorythm()
    {
        moves = new List<MoveExample>();
    }

    public void Solve() 
    {
        BoardExample mainBoard = new BoardExample();

        var firstGeneration = mainBoard.GetPossibleRedMoves();

        MoveExample move = firstGeneration.ElementAt(0);
        float max = move.GetBoardValueAfterMove();

        foreach (var newMove in firstGeneration)
            if (newMove.GetBoardValueAfterMove() > max)
            {
                max = newMove.GetBoardValueAfterMove();
                move = newMove;
            }

        Debug.Log("max in first = " + move);

        for (int i = 0; i < 1000; i++)
        {
            move = move.FindChilds(!move.IsRedMove());
            Debug.Log(move);
        }




        // 25^4 = 390k possible moves

    }

    public static TracingAlgorythm GetInstance()
    {
        if (instance == null)
            instance = new TracingAlgorythm();

        return instance;
    }
}
