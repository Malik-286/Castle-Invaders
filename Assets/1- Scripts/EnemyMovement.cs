using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] List<WayPoint> path = new List<WayPoint>();
     void Start()
    {
        PrintWayPointName();
    }

    void PrintWayPointName()
    {
        foreach(WayPoint waypoint in path)
        {
            Debug.Log(waypoint);
        }
    }

  
}
