using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    private AudioSource buttonPress;

    [SerializeField]
    private AudioSource exitButton;

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();
    }

    // Global
    IEnumerator GoToScene(string Scene)
    {
        
    }
}
