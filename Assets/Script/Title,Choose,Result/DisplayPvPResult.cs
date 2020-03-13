using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPvPResult : MonoBehaviour
{
    public Text resultText;
    public static string howToFinish;


    void Start()
    {
        if(howToFinish == "Quit")
        {
          int b = Judge.CountBlackStones();
          int w = Judge.CountWhiteStones();
          string winner;
          if(w<b)
          {
            winner = "で\n黒の勝ち";
          }else if(b<w)
          {
            winner = "で\n白の勝ち";
          }else{winner = "で\n引き分け";}
          resultText.text = b + " 対 " + w + winner;
        }else if(howToFinish == "GiveUp")
        {
          if(Game.turn == 1)
          {
            resultText.text = "白の勝ち";
          }else if(Game.turn == -1)
          {
            resultText.text = "黒の勝ち";
          }
        }
    }
}
