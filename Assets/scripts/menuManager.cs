using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class menuManager : MonoBehaviour
{
    public void SelectPlayer()
    {
        SceneManager.LoadScene("player1"); 
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
