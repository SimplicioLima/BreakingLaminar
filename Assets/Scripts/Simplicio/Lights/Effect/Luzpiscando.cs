using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luzpiscando : MonoBehaviour
{
    public new Light light;
    public GameObject emissiveObject;
    public AnimationCurve animationCurve;
    public Color color;
    public float multiplyIntensity = 1.0f;

    public WrapMode wrapmode = WrapMode.PingPong;

    private Material emissiveMaterial;

    public bool emergencyLight = true;

    private void Start()
    {
        this.animationCurve.postWrapMode = this.wrapmode;
        this.emissiveMaterial = emissiveObject.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (GameManager.current.karenAlerted && emergencyLight)
        {
            this.light.color = this.color;
            float value = animationCurve.Evaluate(Time.time);

            this.light.intensity = value * multiplyIntensity;
            this.emissiveMaterial.SetColor("EmissionColor", this.color * value);
        }
        else
        {
            this.light.color = Color.white;

            this.light.intensity = multiplyIntensity;
            this.emissiveMaterial.SetColor("EmissionColor", Color.white);
        }
    }
}
