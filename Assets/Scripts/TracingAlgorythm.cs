using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        foreach (var move in mainBoard.GetPossibleRedMoves())
        {
            moves.Add(move);
            Debug.Log(move);
        }

        moves[0].FindChilds(0, moves[0].IsRedMove());
    }

    public static TracingAlgorythm GetInstance()
    {
        if (instance == null)
            instance = new TracingAlgorythm();

        return instance;
    }
}
