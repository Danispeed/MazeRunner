using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class uiManagerPlayer2 : MonoBehaviour
{
    public static uiManagerPlayer2 Instance;
    public Image targetImage; 

    void Start()
    {
        targetImage.enabled = false;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateImage(bool onOff)
    {
        targetImage.enabled = onOff;
    }
}
