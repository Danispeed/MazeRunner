using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }
    
    public GameObject player1SelectedPrefab;
    public GameObject player2SelectedPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }
    }

    public void SetPlayerSelection(int playerNumber, GameObject selectedPrefab)
    {
        if (playerNumber == 1)
        {
            player1SelectedPrefab = selectedPrefab;
            player1SelectedPrefab.tag = "Player1";
            SceneManager.LoadScene("player2");
        }
        else if (playerNumber == 2)
        {
            player2SelectedPrefab = selectedPrefab;
            player2SelectedPrefab.tag = "Player2";
            
            if (player1SelectedPrefab == player2SelectedPrefab){
                return;
            }
            SceneManager.LoadScene("Game");
        }
    }
}
