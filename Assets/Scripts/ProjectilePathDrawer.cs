using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ProjectilePathDrawer : MonoBehaviour
{
    [SerializeField] 
    private GameObject ballPrefab;

    private List<GameObject> pool;

    private List<Vector3> points;

    public void DrawPath(BallMovement ballMovement, Vector3 initialVelocity)
    {
        CalculatePositionsOnPath(ballMovement.StartPosition, ballMovement.Gravity, ballMovement.FlightDuration,
            initialVelocity);
        CreateProjectilePath();
    }

    public void RemovePath()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject go = pool[i];
            go.SetActive(false);
        }
    }
    
    public void CalculatePositionsOnPath (Vector3 startPoint, float gravity, float flightDuration, Vector3 initialVelocity)
    {
        if (points == null)
        {
            points = new List<Vector3>();
        }else
            points.Clear();
        
        int totalPoints = 10;
        for (int i = 0; i < totalPoints; i++)
        {
            float simulationTime = (i+1) / (float)totalPoints * flightDuration;
            float displacementY = (0.5f * gravity * Mathf.Pow(simulationTime, 2)) + (initialVelocity.y * simulationTime);
            float displacementX = simulationTime * (initialVelocity.x);
            float displacementZ = simulationTime * (initialVelocity.z);
            Vector3 newPoint = new Vector3(displacementX + startPoint.x , startPoint.y + displacementY, 
                startPoint.z + displacementZ);
            points.Add(newPoint); 
        }
    }

    public void CreateProjectilePath()
    {
        if (pool == null)
        {
            pool = new List<GameObject>();
        }

        int neededBalls = points.Count - pool.Count;
        for (int i = 0; i < neededBalls; i++)
        {
            pool.Add(Instantiate(ballPrefab));
        }

        for (int i = 0; i < points.Count; i++)
        {
            GameObject go = pool[i];
            go.SetActive(true);
            go.transform.position = points[i];
        }
    }
}
