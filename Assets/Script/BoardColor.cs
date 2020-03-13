using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardColor : MonoBehaviour
{
    public Material black;
    public Material green;
    public Material shinyBlack;
    public Material shinyGreen;


    public void UndoBoardColor(int num)
    {
      GameObject.FindGameObjectWithTag("tagB" + num.ToString()).GetComponent<Renderer>().material.color = black.color;
    }

    public void UndoGreenBoardColor(int num)
    {
      GameObject.FindGameObjectWithTag("tagG" + num.ToString()).GetComponent<Renderer>().material.color = green.color;
    }

    public void ChangeBoardColorShiny(int num)
    {
      GameObject.FindGameObjectWithTag("tagB" + num.ToString()).GetComponent<Renderer>().material.color = shinyBlack.color;
    }

    public void ChangeGreenBoardColorShiny(int num)
    {
      GameObject.FindGameObjectWithTag("tagG" + num.ToString()).GetComponent<Renderer>().material.color = shinyGreen.color;
    }
}
