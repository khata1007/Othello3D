﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvP444
{
  public class KeyDetector : MonoBehaviour
  {
      private readonly string[] keys = {"1", "2", "3", "4"};
      private int x = 0; //GameObject.csのXCoordiに連動
      private int y = 0;
      private int z = 0;
      public GameObject master;


      public void NumKeyDetect() //x,z,y,Enterの順でキーが押されると順にGameクラスの変数に代入
      {
          if(x == 0)
          {
              foreach(string key in keys)
              {
                  if(Input.GetKeyDown(key))
                  {
                      x = int.Parse(key);
                      master.GetComponent<Game>().XCoordi = x;
                  }
              }
          }else if(z == 0)
          {
              foreach(string key in keys)
              {
                  if(Input.GetKeyDown(key))
                  {
                      z = int.Parse(key);
                      master.GetComponent<Game>().ZCoordi = z;
                  }
              }
          }else if(y == 0)
          {
              foreach(string key in keys)
              {
                  if(Input.GetKeyDown(key))
                  {
                      y = int.Parse(key);
                      master.GetComponent<Game>().YCoordi = y;
                  }
              }
          }else
          {
              if(Input.GetKeyDown("return"))
              {
                  master.GetComponent<Game>().SetEnterPressed = true;
                  x = y = z = 0;
              }
          }
      }

      public void BackSpaceDetect() //backspaceが押されたらx,z,yに0を代入
      {
          if(y != 0)
          {
              if(Input.GetKeyDown("backspace"))
              {
                  y = 0;
                  master.GetComponent<Game>().YCoordi = 0;
              }
          }else if(z != 0)
          {
              if(Input.GetKeyDown("backspace"))
              {
                  z = 0;
                  master.GetComponent<Game>().ZCoordi = 0;
              }
          }else if(x != 0)
          {
              if(Input.GetKeyDown("backspace"))
              {
                  x = 0;
                  master.GetComponent<Game>().XCoordi = 0;
              }
          }
      }
  }

}
