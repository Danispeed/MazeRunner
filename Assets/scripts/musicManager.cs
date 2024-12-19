using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource.clip != null)
        {
            audioSource.time = 28f; 
            audioSource.Play(); 
        }
    }
}
