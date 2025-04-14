using UnityEngine;

public class TrilhaSonora : MonoBehaviour
{
    public AudioClip musica; 
    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musica;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;

        audioSource.Play(); // Inicia a música
    }

}
