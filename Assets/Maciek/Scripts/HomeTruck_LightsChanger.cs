using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomeTruck_LightsChanger : MonoBehaviour
{
    public Color color;

    void Start()
    {
        float inten = 2f;
        if (GetComponent<SpriteRenderer>() != null) {
            Material material = GetComponent<SpriteRenderer>().material;
            material.SetColor("Color_F71EC87A", new Color(color.r * inten, color.g * inten, color.b * inten));
        }
        if (GetComponent<UnityEngine.Rendering.Universal.Light2D>() != null) {
            UnityEngine.Rendering.Universal.Light2D light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            light.color = color;
        }
        
    }


}
