﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateDisplay444 : MonoBehaviour //座標を表すテキストを配置
{
    public GameObject coordinateCanvas;
    public GameObject coordinateTextPrefab;


    private void RemoveCoordinateDisplay()
    {
      GameObject[] displays = GameObject.FindGameObjectsWithTag("CoordinateDisplay");
      foreach(GameObject display in displays)
      {
        Destroy(display);
      }
    }

    public void BeforePressedDisplay()
    {
      RemoveCoordinateDisplay();
      for(int n=0; n<2; n++)
      {
        for(int x=0; x<4; x++)
        {
          GameObject d = Instantiate(coordinateTextPrefab,coordinateCanvas.transform);
          d.GetComponent<Text>().text = (x+1).ToString();
          d.transform.position = new Vector3 (1.1f*x-0.15f, 4.5f*n-0.75f, 1.5f);
          d.tag = "CoordinateDisplay";
        }
      }
    }

    public void AfterXPressedDisplay()
    {
      RemoveCoordinateDisplay();
      for(int n=0; n<2; n++)
      {
        for(int x=0; x<4; x++)
        {
          GameObject d = Instantiate(coordinateTextPrefab,coordinateCanvas.transform);
          d.GetComponent<Text>().text = (x+1).ToString();
          d.transform.position = new Vector3 (1.5f, 4.5f*n-0.75f, 1.1f*x-0.15f);
          d.tag = "CoordinateDisplay";
        }
      }
    }

    public void AfterZPressedDisplay()
    {
      RemoveCoordinateDisplay();
      for(int n=0; n<2; n++)
      {
        for(int x=0; x<4; x++)
        {
          GameObject d = Instantiate(coordinateTextPrefab,coordinateCanvas.transform);
          d.GetComponent<Text>().text = (x+1).ToString();
          d.transform.position = new Vector3 (4.5f*n-0.75f, 1.1f*x-0.15f, 1.5f);
          d.tag = "CoordinateDisplay";
        }
      }
    }

    public void AfterYPressedDisplay()
    {
      RemoveCoordinateDisplay();
    }
}
