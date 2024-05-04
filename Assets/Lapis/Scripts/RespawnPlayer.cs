using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField]
    private HealthE healthScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
            healthScript.Kill();
    }
}

