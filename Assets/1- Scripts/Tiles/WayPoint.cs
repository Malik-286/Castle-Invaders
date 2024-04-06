using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool isPlaceable;
    [SerializeField] List<GameObject> towers = new List<GameObject>();

    public bool CheckIfPlaceable()
    {
        return isPlaceable;
    }
    public void SetPlaceable(bool boolean)
    {
        isPlaceable = boolean;
    }

    

}
