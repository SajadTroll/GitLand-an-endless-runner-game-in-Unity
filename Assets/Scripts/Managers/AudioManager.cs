using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.pitch = Random.Range(0.75f, 1f);
        audioSource.PlayOneShot(clip);
    }
}
