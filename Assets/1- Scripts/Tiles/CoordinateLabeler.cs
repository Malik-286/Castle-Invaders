using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{

    TextMeshPro tileText;
    Vector2Int coordinate = new Vector2Int();
     void Awake()
    {
        tileText = GetComponent<TextMeshPro>();
       
    }

     void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordniates();
            UpdateTilesName();

        }
    }

    
        void DisplayCoordniates()
        {
            coordinate.x = Mathf.RoundToInt(transform.parent.position.x / 10);
            coordinate.y = Mathf.RoundToInt(transform.parent.position.y / 10);

             if (!Application.isPlaying)
            {
                tileText.text = coordinate.x.ToString() + "," + coordinate.y.ToString();
            }
            else
            {
                tileText.text = "";  
            }
        }


    void UpdateTilesName()
    {
        transform.parent.name = coordinate.ToString();
    }

     
}
