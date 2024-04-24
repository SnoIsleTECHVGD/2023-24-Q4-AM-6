using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Glitch : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportPoint;

    [SerializeField]
    private Volume lighting;
    [SerializeField]
    private VolumeProfile mainProfile;
    [SerializeField]
    private VolumeProfile glitchProfile;

    [SerializeField]
    private string glitchType;

    public bool isActive = false;

    public float initialGlitchWeight = 0.25f;
    public float teleportTime = 1f;

    void Update()
    {
        if (isActive == true)
        {
            lighting.weight += Time.deltaTime / teleportTime;

            if (lighting.weight >= 1)
                FinishActivate();
        }
    }

    public void Activate()
    {
        if (isActive)
            return;

        isActive = true;

        lighting.profile = glitchProfile;
        lighting.weight = initialGlitchWeight;
    }

    public void FinishActivate()
    {
        if (!isActive)
            return;

        isActive = false;

        if (glitchType == "Teleport")
            player.transform.position = teleportPoint.transform.position;
        else if (glitchType == "Level2Transition")
        {
            SceneManager.LoadScene("Level2");
            return;
        }
        else if (glitchType == "Respawn")
        {
            StartCoroutine(Respawn());
            return;
        }     

        lighting.profile = mainProfile;
        lighting.weight = 1;
    }

    // Custom Effects

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Trigger

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (glitchType == "Teleport")
            Activate();
    }
}
