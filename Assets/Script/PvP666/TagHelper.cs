using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PvP666
{
  public class TagHelper : MonoBehaviour
  {
      public GameObject board; //MaxNumを受け取る


      void Awake()
      {
        for(int y=0; y<6; y++)
        {
          for(int z=0; z<6; z++)
          {
            for(int x=0; x<6; x++)
            {
              AddTag("tagS" + x + y + z);
              AddTag("tagB" + x + y + z);
            }
          }
        }
      }

      private static void AddTag(string tagname) //タグの生成
      {
          UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
          if ((asset != null) && (asset.Length > 0))
          {
            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");
            for (int i = 0; i < tags.arraySize; ++i)
            {
              if (tags.GetArrayElementAtIndex(i).stringValue == tagname){return;}
            }
            int index = tags.arraySize;
            tags.InsertArrayElementAtIndex(index);
            tags.GetArrayElementAtIndex(index).stringValue = tagname;
            so.ApplyModifiedProperties();
            so.Update();
          }
      }
  }

}