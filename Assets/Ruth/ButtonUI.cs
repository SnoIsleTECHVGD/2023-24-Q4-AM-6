using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    private AudioSource buttonPress;

    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private AudioSource exitButton;

    public void Start()
    {
        buttonPress = GetComponent<AudioSource>();
    }

    IEnumerator GoToScene(string Scene)
    {
        AudioSource audio;

        if (Scene == "Quit")
            audio = exitButton;
        else 
            audio = buttonPress;

        audio.Play();
        music.Stop();
       

        while (audio.isPlaying)
        {
            yield return null;
        }

        if (Scene == "Quit")
            Application.Quit();
        else 
            SceneManager.LoadScene(Scene);

    }

    // Buttons

    public void Play()
    {
        StartCoroutine(GoToScene("Play"));
    }


    public void Quit()
    {
        StartCoroutine(GoToScene("MainMenu"));
    }
}




//      skibidi!!!!!!!!!!!!!

