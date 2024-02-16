using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{

    TextMeshPro tileText;
    Vector2Int coordinate = new Vector2Int();
     void Awake()
    {
        tileText = GetComponent<TextMeshPro>();
        DisplayCoordniates();
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
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.y / UnityEditor.EditorSnapSettings.move.y);
        tileText.text = coordinate.x.ToString()+","+coordinate.y.ToString();
    }

    void UpdateTilesName()
    {
        transform.parent.name = coordinate.ToString();
    }

     
}
