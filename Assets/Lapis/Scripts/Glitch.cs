using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Glitch : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportPoint;
    public GameObject checkpointPoint;

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

    public AudioSource deathSound;
    public AudioSource glitchWhirr;

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

        glitchWhirr.Play();

        if (glitchType == "Respawn")
            deathSound.Play();
    }

    public void FinishActivate()
    {
        if (!isActive)
            return;

        isActive = false;
        glitchWhirr.Stop();

        if (glitchType == "Teleport" || glitchType == "TeleportNoCollide")
        {
            player.GetComponent<Grapple>().Cancel();
            player.transform.position = teleportPoint.transform.position;
            StartCoroutine(ReEnable());
        }
        else if (glitchType == "MenuPlay")
        {
            SceneManager.LoadScene("Level1");
            return;
        }
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
        lighting.weight = 0.7f;
    }

    // Custom Effects

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.15f);

        if (checkpointPoint && checkpointPoint.GetComponent<CheckpointTrigger>().active == true)
        {
            player.transform.position = checkpointPoint.transform.position;
            player.GetComponent<PlayerController>().Enable(true);
            player.GetComponent<Rigidbody2D>().simulated = true;
            GetComponent<Animator>().SetBool("Dead", false);
            GetComponent<HealthE>().isAlive = true;
            GetComponent<HealthE>().health = 1;
            
            lighting.profile = mainProfile;
            lighting.weight = 1;
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReEnable()
    {
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<PlayerController>().Enable(true);
    }

    // Trigger

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (glitchType == "Teleport")
            Activate();
    }
}
