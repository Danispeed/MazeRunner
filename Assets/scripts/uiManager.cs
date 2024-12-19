using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public static uiManager Instance { get; private set; }
    public GameObject menuPanel; 
    public TextMeshProUGUI loserText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMenu(string loser)
    {
        // GameObject menuPanel = GameObject.Find("MenuPanel");
        Debug.LogError("hello1");
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
            Debug.LogError("hello2");
            
            if (loserText != null)
            {
                Debug.LogError("hello3");
                loserText.text = loser;
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }
}
