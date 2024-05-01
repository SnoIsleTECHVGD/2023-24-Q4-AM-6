using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private Sprite coolSprite;
    [SerializeField]
    private Glitch glitch;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Light2D sun;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoInteract()
    {
        controller.enabled = false;
        StartCoroutine(Interact2());
    }

    IEnumerator Interact2()
    {
        spriteRenderer.sprite = coolSprite;

        yield return new WaitForSeconds(1f);
        glitch.Activate();
        yield return new WaitForSeconds(1.3f);
        cam.GetComponent<Volume>().weight = 0.785f;
        sun.GetComponent<Light2D>().intensity = 0.65f;
        //click sound effect for lights turning on
    }
}
