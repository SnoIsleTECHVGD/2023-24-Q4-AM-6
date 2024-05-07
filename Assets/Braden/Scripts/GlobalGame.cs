using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGame : MonoBehaviour
{
    public static GlobalGame Instance;

    public AudioSource menuMusic;
    public AudioSource gameMusic;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "mainmenu" || scene.name == "CreditsScene")
        {
            gameMusic.Stop();

            if (!menuMusic.isPlaying)
                menuMusic.Play();
        }
        else
        {
            menuMusic.Stop();

            if (!gameMusic.isPlaying)
                gameMusic.Play();
        }
    }
}
