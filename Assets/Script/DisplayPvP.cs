using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPvP: MonoBehaviour
{
    public GameObject instructionBoard;
    public GameObject instructionText;
    public GameObject turnBoard;
    public GameObject turnText;
    public GameObject spaceKeyBoard;
    public GameObject spaceKeyText;
    public Text turnTextMessage;
    public GameObject claimBoard;
    public Text claimText;
    public GameObject quitText;
    public GameObject giveUpText;
    public GameObject board;
    public GameObject greenBoard;
    public GameObject stone;
    public GameObject coordiDownText;
    public GameObject coordiUpText;
    public GameObject coordiLeftText;
    public GameObject coordiRightText;
    public static bool instruction = false; //操作説明を表示していない状態
    public static bool giveUp = false; //GiveUpGameで用いる変数。true:エンターを押したらギブアップできる状態


    public void SetInstructionBoard(GameObject g)
    {
      if(instruction == false)
      {
        instructionBoard.SetActive(true);
        g.SetActive(true);
        turnBoard.SetActive(false);
        turnText.SetActive(false);
        spaceKeyBoard.SetActive(false);
        spaceKeyText.SetActive(false);
        board.SetActive(false);
        greenBoard.SetActive(false);
        stone.SetActive(false);
        coordiDownText.SetActive(false);
        coordiUpText.SetActive(false);
        coordiLeftText.SetActive(false);
        coordiRightText.SetActive(false);
      }

      if(instruction == true)
      {
        instructionBoard.SetActive(false);
        instructionText.SetActive(false);
        quitText.SetActive(false);
        giveUpText.SetActive(false);
        turnBoard.SetActive(true);
        turnText.SetActive(true);
        spaceKeyBoard.SetActive(true);
        spaceKeyText.SetActive(true);
        board.SetActive(true);
        greenBoard.SetActive(true);
        stone.SetActive(true);
        coordiDownText.SetActive(true);
        coordiUpText.SetActive(true);
        coordiLeftText.SetActive(true);
        coordiRightText.SetActive(true);
      }

      instruction = !instruction; //instructionへの代入のタイミングに注意！！！
    }

    public void ChangeTurnIndication() //TurnTextのターン表示を変更する。
    {
      if(Game.turn == 1)
      {
        turnTextMessage.text = "黒のターンです";
      }else if(Game.turn == -1)
      {
        turnTextMessage.text = "白のターンです";
      }
    }

    public void DoNotPutClaim() //テキストを1秒間表示
    {
      claimBoard.SetActive(true);
      claimText.text = "そこには置けません";
      Invoke("ClaimTextClear", 1);
    }

    public void DoNotPassClaim() //テキストを1秒間表示
    {
      claimBoard.SetActive(true);
      claimText.text = "パスはできません";
      Invoke("ClaimTextClear", 1);
    }

    public void DoNotQuitClaim() //テキストを1秒間表示
    {
      claimBoard.SetActive(true);
      claimText.text = "試合は終わっていません";
      Invoke("ClaimTextClear", 1);
    }

    private void ClaimTextClear() //Invoke用
    {
      claimText.text = "";
      claimBoard.SetActive(false);
    }
}
