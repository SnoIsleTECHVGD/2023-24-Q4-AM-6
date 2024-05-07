using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Volume lighting;
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private VolumeProfile mainProfile;
    [SerializeField]
    private VolumeProfile glitchProfile;

    [SerializeField]
    private Image fade;
    [SerializeField]
    private GameObject fadeObject;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private AudioSource whirring;

    public float minDistance;
    public float triggerDistance;

    [HideInInspector]
    public bool sceneActive = false;

    public float fadeSpeed = 1;
    public float finalZoomSize = 3;

    private bool isFading = false;
    private float defaultSize;

    private void Start()
    {
        defaultSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            fade.color = new Color(0, 0, 0, fade.color.a + (Time.deltaTime * fadeSpeed));

            if (fade.color.a >= 1)
            {
                isFading = false;
                StartCoroutine(Activate3());
            }
        }

        if (sceneActive)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance)
            Activate();
        else if (distance <= minDistance)
        {
            if (lighting.profile != glitchProfile)
            {
                pause.canPause = false;
                player.GetComponent<PlayerController>().canJump = false;

                player.GetComponent<Grapple>().Cancel();
                player.GetComponent<Grapple>().canGrapple = false;

                player.GetComponent<Dash>().Cancel();
                player.GetComponent<Dash>().canDash = false;

                whirring.Play();
                GlobalGame.Instance.gameMusic.Pause();
            }

            lighting.profile = glitchProfile;

            float ratio = distance / minDistance;

            lighting.weight = 1 - ratio;
            whirring.volume = 1 - ratio;
            camera.orthographicSize = finalZoomSize + ((defaultSize - finalZoomSize) * ratio);
        }
        else if (lighting.profile == glitchProfile)
        {
            lighting.profile = mainProfile;

            player.GetComponent<Grapple>().canGrapple = true;
            player.GetComponent<Dash>().canDash = true;
            player.GetComponent<PlayerController>().canJump = true;
            pause.canPause = true;

            camera.orthographicSize = defaultSize;

            whirring.Stop();
            GlobalGame.Instance.gameMusic.UnPause();
        }
    }

    // Activate
    void Activate()
    {
        if (sceneActive)
            return;

        sceneActive = true;

        player.GetComponent<Animator>().speed = 0;
        player.GetComponent<PlayerController>().Enable(false);

        StartCoroutine(Activate2());
    }

    IEnumerator Activate2()
    {
        yield return new WaitForSeconds(1.5f);

        // collapse on the floor
        player.GetComponent<Animator>().SetBool("finaldeath", true);
        player.GetComponent<Animator>().speed = 1;

        whirring.Stop();

        yield return new WaitForSeconds(3.5f);

        fade.color = new Color(0, 0, 0, 0);
        fadeObject.SetActive(true);
        isFading = true;
    }

    IEnumerator Activate3()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("CreditsScene");
    }
}
