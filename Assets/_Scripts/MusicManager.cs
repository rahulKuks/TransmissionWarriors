using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour 
{
    static MusicManager instance;

    [SerializeField] AudioClip[] AudioClipArray;

    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () 
    {
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        if(audioSource == null)
        {
            Debug.LogError("MusicManager failed to access AudioSource");
        }
	}

    void OnEnable()
    {
        SceneManager.activeSceneChanged += PlayMusic;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= PlayMusic;
    }

    private void PlayMusic(Scene lastScene, Scene activeSceneChanged)
    {
        int arrayIndex = activeSceneChanged.buildIndex;

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if(AudioClipArray[arrayIndex] != null)
        {
            if(audioSource.clip != null && AudioClipArray[arrayIndex] == audioSource.clip)
            {
                return;
            }

            audioSource.clip = AudioClipArray[arrayIndex];

            //TODO: Fade between audio clips

            audioSource.Play();
        }
    }



}
