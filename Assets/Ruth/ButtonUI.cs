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

    void Start()
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
       

        //while (audio.isPlaying)
        //{
        //    yield return null;
        //}


        if (Scene == "Quit")
            Application.Quit();
        else 
            SceneManager.LoadScene(Scene);

        yield return null;

    }

    // Buttons

    public void Play()
    {
        StartCoroutine(GoToScene("Level1"));
    }


    public void Quit()
    {
        StartCoroutine(GoToScene("mainmenu"));
    }

    public void Credits()
    {
        StartCoroutine(GoToScene("CreditsScene"));
    }
}




//      skibidi!!!!!!!!!!!!!

