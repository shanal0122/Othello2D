using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGreenBoard : MonoBehaviour
{
    public GameObject boardPrefab;
    public GameObject board;
    public GameObject greenBoardPrefab;
    public GameObject greenBoard;

    
    void Awake()
    {
      for(int x = 0; x < 8; x++){
        for(int z = 0; z < 8; z++){
          int num = x + 8 * z;
          GameObject b = Instantiate(boardPrefab, board.transform);
          b.transform.position = new Vector3(x, -0.51f, z);
          b.tag = "tagB" + num.ToString();
          GameObject g = Instantiate(greenBoardPrefab, greenBoard.transform);
          g.transform.position = new Vector3(x, -0.05f, z);
          g.tag = "tagG" + num.ToString();
        }
      }
    }
}
