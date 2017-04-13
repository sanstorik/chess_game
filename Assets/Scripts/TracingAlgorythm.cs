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
        //foreach (var rule in mainBoard.GetPossibleRedMoves())
        //    Debug.Log(rule);

        var firstGeneration = mainBoard.GetPossibleRedMoves();

        MoveExample move = firstGeneration.ElementAt(0);
        float max = move.GetBoardValueAfterMove();

        foreach (var newMove in firstGeneration)
            if (newMove.GetBoardValueAfterMove() > max)
            {
                max = newMove.GetBoardValueAfterMove();
                move = newMove;
            }

        Debug.Log("max in first = " + move + "  value= " + move.GetBoardValueAfterMove());

        MoveExample bestMove;

        //first RED
        foreach (var zeroMove in firstGeneration)
            //SECOND RED
            foreach (var firstMove in zeroMove.FindChilds(!zeroMove.IsRedMove(), out bestMove))
                //THIRD BLUE
                foreach (var moveSecond in firstMove.FindChilds(!firstMove.IsRedMove(), out bestMove))
                    //FOURTH RED
                    foreach (var moveThird in moveSecond.FindChilds(!moveSecond.IsRedMove(), out bestMove))
                        ;
                        //FIFTH BLUE
                        //foreach (var moveForuth in moveThird.FindChilds(!moveThird.IsRedMove()))
                        //    ;


        // 25^4 = 390k possible moves

    }

    public static TracingAlgorythm GetInstance()
    {
        if (instance == null)
            instance = new TracingAlgorythm();

        return instance;
    }
}
