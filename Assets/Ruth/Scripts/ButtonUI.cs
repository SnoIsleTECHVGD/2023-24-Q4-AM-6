using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField]
    private AudioSource music;

    void Start()
    {
        Time.timeScale = 1;
    }

    void GoToScene(string Scene)
    {
        music.Stop();

        if (Scene == "Quit")
            Application.Quit();
        else if (Scene == "Level1")
            GetComponent<Glitch>().Activate();
        else
            SceneManager.LoadScene(Scene);
    }

    // Buttons

    public void Play()
    {
        GoToScene("Level1");
    }


    public void Quit()
    {
        GoToScene("Quit");
    }

    public void Credits()
    {
        GoToScene("CreditsScene");
    }
}




//      skibidi my friend!!!!!!!!!!!!!

