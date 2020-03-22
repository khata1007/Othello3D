using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PvP444
{
  public class Stone444 : MonoBehaviour
  {
      private int[,,] square = new int[4,4,4]; //noStone : 0, blackStone : 1, whiteStone : -1
      private readonly int[,] vector = new int[,]{{0,1,0},{1,1,0},{0,1,1},{-1,1,0},{0,1,-1},{1,0,0},{1,0,1},{0,0,1},{-1,0,1},{-1,0,0},{-1,0,-1},{0,0,-1},{1,0,-1},{1,-1,0},{0,-1,1},{-1,-1,0},{0,-1,-1},{0,-1,0}};
      public GameObject blackStone;
      public GameObject whiteStone;
      public GameObject master; //GameからTurnを受け取る
      public GameObject colorManager; //CanPutAndInformで置ける場所を光らせる


      private int FlipNum(int stone, int x, int y, int z, int vec) //stone{1,-1}を座標(x,y,z)に置いた時vec方向のコマを返せる個数を返す
      {
        int flipNum = 0;
        int myStone = stone;
        int yourStone = -1 * stone;
        while(true)
        {
          x += vector[vec,0];
          y += vector[vec,1];
          z += vector[vec,2];
          try
          {
            if(square[x,y,z] == yourStone)
            {
              flipNum++;
            }else if(square[x,y,z] == myStone)
            {
              break;
            }else
            {
              flipNum = 0;break;
            }
          }catch(IndexOutOfRangeException)
          {
            flipNum = 0;break;
          }
        }
        return flipNum;
      }

      public void PutStone(int stone, int x, int y, int z) //座標(x,y,z)に石をおく
      {
        if(stone == 1)
        {
          GameObject s = Instantiate(blackStone, this.transform);
          s.transform.position = new Vector3(x, y, z);
          s.tag = "tagS" + x + y + z;
          square[x,y,z] = 1;
        }
        if(stone == -1)
        {
          GameObject s = Instantiate(whiteStone, this.transform);
          s.transform.position = new Vector3(x, y, z);
          s.tag = "tagS" + x + y + z;
          square[x,y,z] = -1;
        }
        if(stone != 1 && stone != -1)
        {
          Debug.Log("Error : Stone/PutStone");//////////////////////////////////////////////////////////////////////////////////////
        }
      }

      private void RemoveStone(int x, int y, int z) //座標(x,y,z)の石を取り除く
      {
        Destroy(GameObject.FindGameObjectWithTag("tagS" + x + y + z));
        square[x,y,z] = 0;
      }

      private int VecFlipStone(int stone, int x, int y, int z, int vec) //stone{-1,1}を座標(x,z)に置いた時のvec方向のstoneを裏返す。戻り値は裏返す石の数
      {
        int flipNum = FlipNum(stone, x, y, z, vec);
        for(int n=1; n<=flipNum; n++)
        {
          RemoveStone(x+n*vector[vec,0], y+n*vector[vec,1], z+n*vector[vec,2]);
          PutStone(stone, x+n*vector[vec,0], y+n*vector[vec,1], z+n*vector[vec,2]);
        }
        return flipNum;
      }

      public void FlipStone(int stone, int x, int y, int z) //座標(x,y,z)に石をおき裏返しturnを変更する。置けない時は何もしない
      {
        if(square[x,y,z] == 0)
        {
          int sumOfFlipNum = 0;
          for(int n=0; n<vector.GetLength(0); n++)
          {
            sumOfFlipNum += VecFlipStone(stone, x, y, z, n);
          }
          if(sumOfFlipNum != 0)
          {
            PutStone(stone,x,y,z);
            master.GetComponent<Game444>().Turn *= -1;
          }
        }
      }


      public int CountStone(int stone) //盤上にあるstoneの数を数える
      {
        int stoneNum = 0;
        foreach(int sq in square) {if(sq == stone) {stoneNum++;}}
        if(stone != 1 && stone != -1)
        {
          Debug.Log("Error : Stone/CountStone");//////////////////////////////////////////////////////////////////////////////////////
        }
        return stoneNum;
      }

      public bool CanPut(int stone) //stoneを置ける場所が一つでもあればtrueを返す
      {
        if(stone != 1 && stone != -1)
        {
          Debug.Log("Error : Stone/CanPut");//////////////////////////////////////////////////////////////////////////////////////
        }
        for(int y=0; y<4; y++)
        {
          for(int z=0; z<4; z++)
          {
            for(int x=0; x<4; x++)
            {
              for(int n=0; n<vector.GetLength(0); n++)
              {
                if(square[x,y,z] == 0 && FlipNum(stone,x,y,z,n) != 0) {return true;}
              }
            }
          }
        }
        return false;
      }

      public bool CanPutAndInform(int stone) //stoneを置ける場所が一つでもあればtrueを返す。置ける場所を光らせる（Menu画面で変更可能）
      {
        if(stone != 1 && stone != -1)
        {
          Debug.Log("Error : Stone/CanPutAndInform");//////////////////////////////////////////////////////////////////////////////////////
        }
        bool canPut = false;
        bool cp = false; //各マスの少なくとも1つの方向で石が返せるならtrue。これにより各マスの置ける場所を光らせる
        for(int y=0; y<4; y++)
        {
          for(int z=0; z<4; z++)
          {
            for(int x=0; x<4; x++)
            {
                cp = false;
                for(int n=0; n<vector.GetLength(0); n++)
                {
                  if(square[x,y,z] == 0 && FlipNum(stone,x,y,z,n) != 0)
                  {
                    cp = true;
                    canPut = true;
                  }
                }
                if(cp) {colorManager.GetComponent<ChangeColor444>().InformShineBoardColor(x,y,z);} //光らせる
            }
          }
        }
        return canPut;
      }

      public void Inform(int stone, int x, int y, int z) //(x,y,z)に石が置けるなら光らせる（Menu画面で変更可能）
      {
        if(stone != 1 && stone != -1)
        {
          Debug.Log("Error : Stone/CanPutAndInform");//////////////////////////////////////////////////////////////////////////////////////
        }
        if(square[x,y,z] == 0)
        {
          bool cp = false; //各マスの少なくとも1つの方向で石が返せるならtrue。これにより各マスの置ける場所を光らせる
          for(int n=0; n<vector.GetLength(0); n++)
          {
            if(FlipNum(stone,x,y,z,n) != 0) { cp = true; break; }
          }
          if(cp) {colorManager.GetComponent<ChangeColor444>().InformShineBoardColor(x,y,z);}
        }
      }
  }

}
