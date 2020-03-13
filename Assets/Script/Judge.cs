using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{

    public static int CountBlackStones() //黒石の数を数える
    {
      int blackStoneNum = 0;
      foreach(int square in Game.square)
      {
        if(square == 1)
        {
          blackStoneNum++;
        }
      }
      return blackStoneNum;
    }

    public static int CountWhiteStones() //白石の数を数える
    {
      int whiteStoneNum = 0;
      foreach(int square in Game.square)
      {
        if(square == -1)
        {
          whiteStoneNum++;
        }
      }
      return whiteStoneNum;
    }

    public bool CanPutOrNot(int stone) //stoneを盤上の一箇所以上の場所に置けるかを判定する。置けるならtrueを返す。
    {
      for(int x = 0; x < 8; x++){
        for(int z = 0; z < 8; z++){
          if(Game.square[x,z] == 0)
          {
            for(int n=0; n<8; n++)
            {
              if(GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().FlipNum(stone, x, z, n) != 0){return true;}
            }
          }
        }
      }
      return false;
    }
}
