using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField]
    private Glitch respawnGlitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (respawnGlitch.isActive == false)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<SpriteRenderer>().enabled = false;

                respawnGlitch.Activate();
            }
        }
    }
}

