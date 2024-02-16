using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] List<GameObject> towers = new List<GameObject>();

      void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
                Debug.Log(gameObject.name);
                GameObject clone = Instantiate(towers[0], transform.position, Quaternion.identity);
                Debug.Log("Tower Placed");
                isPlaceable = false;
            }
            
        }
    }
}
