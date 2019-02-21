using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("J/3D/JFadeLight")]
public class JFadeLight : MonoBehaviour
{
    public bool Fade;
    public Light FadeLight;
    public AnimationCurve Intensity;
    public float Duration;

    // Start is called before the first frame update
    void Start()
    {
        FadeLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Fade)
        {
            FadeLight.intensity = Intensity.Evaluate(Duration * Time.deltaTime);
        }
    }
}
