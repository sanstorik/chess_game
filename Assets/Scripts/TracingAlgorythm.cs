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
        foreach (var rule in mainBoard.GetPossibleRedMoves())
            Debug.Log(rule);

        //first RED
        foreach (var zeroMove in mainBoard.GetPossibleRedMoves())
            //SECOND RED
            foreach (var firstMove in zeroMove.FindChilds(!zeroMove.IsRedMove()))
                //THIRD BLUE
                foreach (var moveSecond in firstMove.FindChilds(!firstMove.IsRedMove()))
                    //FOURTH RED
                    foreach (var moveThird in moveSecond.FindChilds(!moveSecond.IsRedMove()))
                        ;

        // 25^4 = 390k possible moves

    }

    public static TracingAlgorythm GetInstance()
    {
        if (instance == null)
            instance = new TracingAlgorythm();

        return instance;
    }
}
