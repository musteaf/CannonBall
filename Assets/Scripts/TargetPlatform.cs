using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlatform: MonoBehaviour
{
   private Vector3 position;
   [SerializeField] 
   private GameObject cubePrefab;
   private List<GameObject> pool;
   private List<Vector3> points;

   public Vector3 Position
   {
      get => position;
      set => position = value;
   }

   private void Start()
   {
      position = transform.position;
      InitializePlatform();
      //GameObject gameObject = Instantiate(cubePrefab, new Vector3(position.x, position.y+j, offset + i), Quaternion.identity);

   }

   public void InitializePlatform()
   {
      CalculatePoints();
      InitializeCubes();
   }

   private void InitializeCubes()
   {
      if (pool == null)
      {
         pool = new List<GameObject>();
      }

      int neededBalls = points.Count - pool.Count;
      for (int i = 0; i < neededBalls; i++)
      {
         GameObject go = Instantiate(cubePrefab, new Vector3(-5000, -5000, -5000), Quaternion.identity);
         go.AddComponent<EruptiveCube>();
         pool.Add(go);
      }

      for (int i = 0; i < points.Count; i++)
      {
         GameObject go = pool[i];
         go.SetActive(true);
         Rigidbody rigidbody = go.GetComponent<Rigidbody>();
         rigidbody.velocity= Vector3.zero;
         rigidbody.angularVelocity = Vector3.zero;
         rigidbody.transform.rotation = Quaternion.identity;
         pool[i].transform.position = points[i];
      }
   }


   public void OpenCubes()
   {
      
   }

   private void CalculatePoints()
   {
      if (points == null)
      {
         points = new List<Vector3>();
      }else
         points.Clear();
      
      int length = 20;
      int height = 10;
      float offset = -length / 2;
      offset += offset % 2 == 0 ? 0.5f : 0f;
      for (int j = 0; j < height; j++)
      {
         for (int i = 0; i < length; i++)
         {
            points.Add(new Vector3(position.x, position.y + j, offset + i));
         }
      }
   }

}
