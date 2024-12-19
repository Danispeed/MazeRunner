using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ballInfoDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string advantage; 
    public TextMeshProUGUI infoText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = advantage;
        infoText.gameObject.SetActive(true); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.gameObject.SetActive(false); 
    }
}
