using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class characterSelector : MonoBehaviour
{
    public GameObject[] prefabs;
    public int player_character;

    public TextMeshProUGUI infoText;
    public string same_character; 

    public void OnClickCharacterSelector(int ball_index)
    {
        gameManager.Instance.SetPlayerSelection(player_character, prefabs[ball_index]);

        infoText.text = same_character;
        infoText.gameObject.SetActive(true); 
        StartCoroutine(PauseGameRoutine());
    }

    IEnumerator PauseGameRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        infoText.gameObject.SetActive(false); 
    }
}
