using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyDetect : MonoBehaviour
{
    public GameObject instructionText;
    public GameObject quitText;
    public GameObject giveUpText;
    int x = 0; //KeysDetectorで用いる変数
    int z = 0;
    bool xShined = true; //KeysDetectorで用いる変数、xを決定した時にボードを一度だけ光らせるために用いる。true:光ってない, false:光ってる
    bool zShined = true;


    public void NumKeyDetector(ref int n) //1~8のキーが押されたらnを書き換える
    {
      string[] Keys = {"1", "2", "3", "4", "5", "6", "7", "8"};
      if(Input.anyKeyDown)
      {
        foreach(string key in Keys)
        {
          if(Input.GetKeyDown(key))
          {
            Debug.Log("押されたキー : " + key);///////////////////////////////////////////////////////
            n = int.Parse(key);
          }
        }
      }
    }

    public void KeysDetector() //x座標、z座標、確定の順でキーが押されたらGameクラスの(xCoordi,zCoordi)に座標を代入し、ボードを光らせる。
    {
      if(x == 0)
      {
          GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().NumKeyDetector(ref x); //x座標を決定
      }else
      {
          if(z == 0)
          {
                GameObject.FindGameObjectWithTag("keydetector").GetComponent<KeyDetect>().NumKeyDetector(ref z); //z座標を決定
                if(xShined)
                {
                    for(int n=0; n<8; n++)
                    {
                        GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeBoardColorShiny((x-1) + 8 * n); //xの列を光らせる
                        GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeGreenBoardColorShiny((x-1) + 8 * n); //xの列を光らせる
                    }
                    xShined = false;
                }
                if(Input.GetKeyDown("backspace"))
                {
                    for(int n=0; n<8; n++)
                    {
                        GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoBoardColor((x-1) + 8 * n); //xの列の色を元に戻す
                        GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoGreenBoardColor((x-1) + 8 * n); //xの列の色を元に戻す
                    }
                    xShined = true;
                    x = 0;
                }
          }else
          {
              if(zShined)
              {
                  for(int n=0; n<8; n++)
                  {
                      GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoBoardColor((x-1) + 8 * n); //xの列の色を元に戻す
                      GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoGreenBoardColor((x-1) + 8 * n); //xの列の色を元に戻す
                  }
                  GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeBoardColorShiny((x-1) + 8 * (z-1)); //(x,z)を光らせる
                  GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeGreenBoardColorShiny((x-1) + 8 * (z-1)); //(x,z)を光らせる
                  zShined = false;
              }
              if(Input.GetKeyDown("backspace"))
              {
                  for(int n=0; n<8; n++)
                  {
                      GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeBoardColorShiny((x-1) + 8 * n); //xの列を光らせる
                      GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().ChangeGreenBoardColorShiny((x-1) + 8 * n); //xの列を光らせる
                  }
                  zShined = true;
                  z = 0;
              }
              if(Input.GetKeyDown("return"))
              {
                    GameObject.FindGameObjectWithTag("master").GetComponent<Game>().xCoordi = x;
                    GameObject.FindGameObjectWithTag("master").GetComponent<Game>().zCoordi = z;
                    GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoBoardColor((x-1) + 8 * (z-1)); //(x,z)の色を元に戻す
                    GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoGreenBoardColor((x-1) + 8 * (z-1)); //(x,z)の色を元に戻す
                    xShined = true;
                    zShined = true;
                    x = z = 0;
              }
          }
      }
    }

    public void DisplayInstruction() //spaceキーが押されたら操作説明を表示し、ターンとspaceキーの説明を非アクティブ化
    {
      if(Input.GetKeyDown("space"))
      {
        GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().SetInstructionBoard(instructionText);
      }
    }

    public void PassMyTurn() //一つも置けない時のみパス
    {
      if(Input.GetKeyDown("p") && DisplayPvP.instruction == false)
      {
        if(!GameObject.FindGameObjectWithTag("master").GetComponent<Judge>().CanPutOrNot(Game.turn))
        {
          x = z = 0; //KeyDetectorメソッドを初期化
          xShined = zShined = true;
          Game.turn *= -1; //ターン変更
          GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().ChangeTurnIndication(); //ターン表示を変更
          for(int n=0; n<64; n++) //色を戻す
          {
            GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoBoardColor(n);
            GameObject.FindGameObjectWithTag("color").GetComponent<BoardColor>().UndoGreenBoardColor(n);
          }
        }else
        {
          GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().DoNotPassClaim();
        }
      }
    }

    public void QuitGame() //両者置く場所がなくなった場合。ゲームをやめてResultシーンへ移る。
    {
      if(Input.GetKeyDown("q") && DisplayPvP.instruction == false)
      {
        if(!GameObject.FindGameObjectWithTag("master").GetComponent<Judge>().CanPutOrNot(1) && !GameObject.FindGameObjectWithTag("master").GetComponent<Judge>().CanPutOrNot(-1))
        {
          GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().SetInstructionBoard(quitText);
          DisplayPvP.giveUp = true;
          DisplayPvPResult.howToFinish = "Quit";
        }else{GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().DoNotQuitClaim();}
      }
    }

    public void GiveUpGame() //両者置く場所がなくなった場合。ゲームをやめてResultシーンへ移る。
    {
      if(Input.GetKeyDown("g") && DisplayPvP.instruction == false)
      {
        GameObject.FindGameObjectWithTag("master").GetComponent<DisplayPvP>().SetInstructionBoard(giveUpText);
        DisplayPvP.giveUp = true;
        DisplayPvPResult.howToFinish = "GiveUp";
      }
    }

    public void GiveUpGameEnter()
    {
      if(Input.GetKeyDown("return") && DisplayPvP.giveUp == true)
      {
        SceneManager.LoadScene("PvPResult");

      }
    }
}
