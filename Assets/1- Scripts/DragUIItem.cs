using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Net.Security;


public class DragUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
     [SerializeField] private GameObject PrefabToInstantiate;

     [SerializeField] private RectTransform UIDragElement;

     [SerializeField] private RectTransform Canvas;

    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;

 


    WayPoint wayPoint;
    CurrencyManager currencyManager;
    AudioManager audioManager;
    GamePlayUI gamePlayUI;
    bool FillerisWorking = false;
   
    void Start()
    {
        mOriginalPosition = UIDragElement.localPosition;
        wayPoint = FindObjectOfType<WayPoint>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        audioManager = FindObjectOfType<AudioManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
    }

   
 
    public void OnBeginDrag(PointerEventData data)
    {
         mOriginalPanelLocalPosition = UIDragElement.localPosition;

         RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Canvas,
            data.position,
            data.pressEventCamera,
            out mOriginalLocalPointerPosition);
    }

 
    public void OnDrag(PointerEventData data)
    {
        if (!FillerisWorking)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Canvas,
                data.position,
                data.pressEventCamera,
                out localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - mOriginalLocalPointerPosition;

                UIDragElement.localPosition = mOriginalPanelLocalPosition + offsetToOriginal;
            }
        }
  
    }


    public IEnumerator Coroutine_MoveUIElement(RectTransform r, Vector2 targetPosition, float duration = 0.1f)
    {
        if (!FillerisWorking)
        {
            float elapsedTime = 0;
            Vector2 startingPos = r.localPosition;

            while (elapsedTime < duration)
            {
                r.localPosition = Vector2.Lerp(startingPos, targetPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            r.localPosition = targetPosition;
        }
    }

     
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!FillerisWorking)
        {
            StartCoroutine(Coroutine_MoveUIElement(UIDragElement, mOriginalPosition, 0.5f));

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Vector3 worldPoint = hit.point;


                CheckSpaceForTower(worldPoint, 1.0f);
            }
        }
    }

 
    public void CreateObject(Vector3 position)
    {
        if (currencyManager.GetCurrentGold() <= 5)
        {
             Debug.Log("Not Enough Coins");
             gamePlayUI.EnableShopPanel();
                       
            return;
        }

        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }
        else
        {
            if(PrefabToInstantiate.tag == "Tower4" && currencyManager.GetCurrentDiamond() <= 0)
            {
                Debug.Log("Not Enough Coins");
                gamePlayUI.EnableShopPanel();

                return;
            }
        }

     

        if (PositionWithinCell(position))
        {
            GameObject obj = Instantiate(PrefabToInstantiate, position, Quaternion.identity);
            Destroy(obj, 15f);
            PlaceTowersInTheWorld(obj);            
                    
            if (audioManager != null)
            {
                audioManager.PlayTowerPlacingSoundEffect();
            }
                      
        }
    }

   
      private bool PositionWithinCell(Vector3 pos)
    {
         return true;
    }

    void PlaceTowersInTheWorld(GameObject obj)
    {
        if(obj.CompareTag("Tower1"))
        {
            currencyManager.DecreaseGold(10);

            gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            FillerisWorking = true;
            Invoke(nameof(removeFiller), 5f);
        }else if(obj.CompareTag("Tower2"))
        {
            currencyManager.DecreaseGold(20);
            gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            FillerisWorking = true;
            Invoke(nameof(removeFiller), 5f);
        }
        else if(obj.CompareTag("Tower3"))
        {
            currencyManager.DecreaseGold(50);
            gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            FillerisWorking = true;
            Invoke(nameof(removeFiller), 5f);
        }
        else if (obj.CompareTag("Tower4"))
        {
            // Check if the player has at least 1 diamond before placing Tower4
            if (currencyManager.GetCurrentDiamond() > 0)
            {
                currencyManager.DecreaseDiamond(1);
                gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                FillerisWorking = true;
                Invoke(nameof(removeFiller), 5f);
            }
            else if (currencyManager.GetCurrentDiamond() <= 0)
            {
                Debug.Log("Not Enough Diamonds to place this tower.");
                //gamePlayUI.EnableShopPanel();
                return;
            }


            
        }

    }

    public void removeFiller()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        FillerisWorking = false;
    }


    void CheckSpaceForTower(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        string Path = "path";

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Tower1") || colliders[i].CompareTag("Tower2") ||  colliders[i].CompareTag("Tower3") || colliders[i].CompareTag("Tower4") || colliders[i].CompareTag("Path"))
            {
                Debug.Log("Cannot place tower here, space is occupied by another tower.");
                return;
            }

            Transform parent = colliders[i].transform.parent;
            if (parent != null && parent.CompareTag("Path"))
            {
                Debug.Log("Cannot place tower here, space is part of the path.");
                return;
            }
        }

        
        CreateObject(position);
        
    }

}
