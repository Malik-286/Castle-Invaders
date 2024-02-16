using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float waitTime = 1.0f;
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
     void Start()
    {

        StartCoroutine(FollowPath());
       
    }

    IEnumerator FollowPath()
    {
        foreach(WayPoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
            Debug.Log(waypoint);
        }
    }

  
}
