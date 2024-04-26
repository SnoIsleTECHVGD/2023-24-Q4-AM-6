using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private CamFollow camFollow;

    private bool active = false;
    private bool isZooming = false;

    // Update is called once per frame
    void Update()
    {
        if (isZooming)
        {
            mainCamera.orthographicSize -= Time.deltaTime * 5;

            if (mainCamera.orthographicSize <= 0)
                StartCoroutine(Complete());
        }
    }

    IEnumerator Activate()
    {
        active = true;

        player.GetComponent<Rigidbody2D>().simulated = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        //camFollow.speed = 3;
        camFollow.target = transform;

        yield return new WaitForSeconds(0.4f);
        isZooming = true;
    }

    IEnumerator Complete()
    {
        isZooming = false;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level4");
    }

    // Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !active)
            StartCoroutine(Activate());
    }
}
