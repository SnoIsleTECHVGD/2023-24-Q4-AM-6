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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Activate()
    {
        active = true;

        player.GetComponent<Rigidbody2D>().simulated = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level4");
    }

    // Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !active)
            StartCoroutine(Activate());
    }
}
