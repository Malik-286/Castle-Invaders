using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

 
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

   
    public IEnumerator Coroutine_MoveUIElement(RectTransform r, Vector2 targetPosition, float duration = 0.1f)
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

     
    public void OnEndDrag(PointerEventData eventData)
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

 
    public void CreateObject(Vector3 position)
    {
        if (currencyManager.GetCurrentGold() <= 5)
        {
            Debug.Log("Not Enough Coins");
            gamePlayUI.EnableShopPanel();
            
            Debug.Log("Calling this methosd from Class DragUIiTEM");
            return;
        }

        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }

        if (PositionWithinCell(position))
        {         
            GameObject obj = Instantiate(PrefabToInstantiate, position, Quaternion.identity);
            Destroy(obj, 15f);
            SpendGold(obj);
            wayPoint.SetPlaceable(false);
            if(audioManager != null)
            {
                audioManager.PlayTowerPlacingSoundEffect();
            }
                      
        }
    }

   
      private bool PositionWithinCell(Vector3 pos)
    {
         return true;
    }

    void SpendGold(GameObject obj)
    {
        if(obj.CompareTag("Tower1"))
        {
            currencyManager.DecreaseGold(10);
        }else if(obj.CompareTag("Tower2"))
        {
            currencyManager.DecreaseGold(20);
        }
        else if(obj.CompareTag("Tower3"))
        {
            currencyManager.DecreaseGold(30);
        }
        else if (obj.CompareTag("Tower4"))
        {
            currencyManager.DecreaseGold(40);
        }
        else if (obj.CompareTag("Tower5"))
        {
            currencyManager.DecreaseGold(50);
        }
    }



    void CheckSpaceForTower(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Tower1") || colliders[i].CompareTag("Tower2") ||
                colliders[i].CompareTag("Tower3") || colliders[i].CompareTag("Tower4") || colliders[i].CompareTag("Tower5"))
            {
                Debug.Log("Cannot place tower here, space is occupied by another tower.");
                return;
            }
        }

        CreateObject(position);
        
    }

}
