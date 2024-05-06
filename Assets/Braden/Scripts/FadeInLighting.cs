using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeInLighting : MonoBehaviour
{
    private Volume volume;

    [SerializeField]
    private VolumeProfile titleProfile;
    private VolumeProfile mainProfile;

    [SerializeField]
    private float weightPoint = 0.15f;

    public float fadeSpeed = 0.6f;
    private bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        mainProfile = volume.profile;

        volume.profile = titleProfile;
        volume.weight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isComplete)
            return;

        volume.weight -= (Time.deltaTime * fadeSpeed);

        if (volume.weight <= weightPoint)
        {
            volume.profile = mainProfile;
            volume.weight = 1;
            isComplete = true;
        }
    }
}
