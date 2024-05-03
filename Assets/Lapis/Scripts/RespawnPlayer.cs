using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Glitch respawnGlitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (respawnGlitch.isActive == false)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<PlayerController>().Enable(false);
               
                GetComponent<Animator>().SetBool("Dead", true);
                respawnGlitch.Activate();
            }
        }
    }
}

