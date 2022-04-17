using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOneShotClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
