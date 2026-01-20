using hardartcore.CasualGUI;
using UnityEngine;

public class QuitGamePanel : MonoBehaviour
{
     

   

    public void QuitGame()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }

    public void DoNotQuitGame()
    {
        gameObject.GetComponent<Dialog>().HideDialog();
        gameObject.SetActive(false);
    }
}
