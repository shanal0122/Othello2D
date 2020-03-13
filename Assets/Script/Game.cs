using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    //private int stone = 0; //0:石を置いていない, 1:黒石, -1:白石
    public static int turn = 1;  //ターン...1:黒石, -1:白石
    public static int[,] square = new int[8,8]; //[x座標,z座標], int stoneを代入
    public static readonly int[,] vector = new int[,] {{1,0},{1,1},{0,1},{-1,1},{-1,0},{-1,-1},{0,-1},{1,-1}}; //各方向ベクトル, [7,1]
    public int xCoordi, zCoordi;


    void Start() //
    {
        GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().PutStone(-1, 3, 3);
        GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().PutStone(1, 4, 3);
        GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().PutStone(1, 3, 4);
        GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().PutStone(-1, 4, 4);
    }


    void Update()
    {
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().DisplayInstruction();
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().PassMyTurn();
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().QuitGame();
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().GiveUpGame();
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().GiveUpGameEnter();
        if(turn == 1 && DisplayPvP.instruction == false)
        {
            PlayGame();
        }
        if(turn == -1 && DisplayPvP.instruction == false)
        {
            PlayGame();
        }
    }


    private void PlayGame() //キーが入力されると石を置き裏返し、相手のターンに移る。TurnTextのターン表示を変更する。
    {
      if(xCoordi * zCoordi == 0)
      {
        GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().KeysDetector();
      }else
      {
        GameObject.FindGameObjectWithTag("stone").GetComponent<Stone>().FlipStone(turn, xCoordi-1, zCoordi-1);
        /*for(int zz=7; zz>=0; zz--)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
          Debug.Log(square[0,zz] + " " +square[1,zz] + " " +square[2,zz] + " " +square[3,zz] + " " +square[4,zz] + " " +square[5,zz] + " " +square[6,zz] + " " +square[7,zz]);
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
        GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().ChangeTurnIndication();
        xCoordi = zCoordi = 0;
      }
    }
}
