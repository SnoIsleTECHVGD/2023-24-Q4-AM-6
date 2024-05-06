using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private Sprite coolSprite;
    [SerializeField]
    private Glitch glitch;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoInteract()
    {
        controller.Enable(false);
        StartCoroutine(Interact2());
    }

    IEnumerator Interact2()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = coolSprite;

        yield return new WaitForSeconds(1f);
        glitch.Activate();
    }
}
