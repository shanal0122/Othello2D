using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject blackStone;
    public GameObject whiteStone;
    public GameObject stones;


    //stone{1,-1}を座標(x,z)に置いた時vec方向のコマを返せる個数を返す
    public int FlipNum(int stone, int x, int z, int vec)
    {
      int flipNum = 0;
      int myStone = stone;
      int yourStone = stone * -1;
      for(int s=0; s<7; s++)
      {
        x += Game.vector[vec,0];
        z += Game.vector[vec,1];
        try
        {
          if(Game.square[x,z] == yourStone)
          {
            flipNum++;
          }else if(Game.square[x,z] == myStone)
          {
            break;
          }else
          {
            flipNum = 0;
            break;
          }
        }catch(IndexOutOfRangeException)
        {
          flipNum = 0;
          break;
        }
      }
      return flipNum;
    }

    //stone{-1,1}を座標(x,z)に置く
    public void PutStone(int stone, int x, int z)
    {
      if(stone == 1)
      {
        GameObject s = Instantiate(blackStone, stones.transform);
        s.transform.position = new Vector3(x, 0.05f, z);
        int num = x + 8 * z;
        s.tag = "tagS" + num.ToString();
        Game.square[x,z] = 1;
      }
      if(stone == -1)
      {
        GameObject s = Instantiate(whiteStone, stones.transform);
        s.transform.position = new Vector3(x, 0.05f, z);
        int num = x + 8 * z;
        s.tag = "tagS" + num.ToString();
        Game.square[x,z] = -1;
      }
    }

    //座標(x,z)の石を取り除く
    private void RemoveStone(int x, int z)
    {
      int num = x + 8 * z;
      GameObject s = GameObject.FindGameObjectWithTag("tagS" + num.ToString());
      Destroy(s);
      Game.square[x,z] = 0;
    }

    //stone{-1,1}を座標(x,z)に置いた時のvec方向のstoneを裏返す。戻り値は
    public int VecFlipStone(int stone, int x, int z, int vec)
    {
      int flipNum = FlipNum(stone, x, z, vec);
      for(int n=1; n<=flipNum; n++)
      {
        RemoveStone(x+n*Game.vector[vec,0], z+n*Game.vector[vec,1]);
        PutStone(stone, x+n*Game.vector[vec,0], z+n*Game.vector[vec,1]);
      }
      return flipNum;
    }

    //stone{-1,1}を座標(x,z)に置き、周りのstoneを裏返す
    public void FlipStone(int stone, int x, int z)
    {
      if(Game.square[x,z] == 0)
      {
        int SumOfFlipNum = 0;
        for(int n=0; n<8; n++)
        {
          SumOfFlipNum += VecFlipStone(stone, x, z, n);
        }
        if(SumOfFlipNum != 0) //置いた場所が一つも裏返せないなら無効
        {
          PutStone(stone, x, z);
          Game.turn *= -1;
        }else
        {
          GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().DoNotPutClaim();
        }
      }else{GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().DoNotPutClaim();}
    }
}
