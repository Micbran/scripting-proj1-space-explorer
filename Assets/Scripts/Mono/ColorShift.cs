using UnityEngine;

public class ColorShift : MonoBehaviour
{
    public float FadeDuration = 1f;
    public Color Color1 = Color.gray;
    public Color Color2 = Color.white;

    private Color startColor;
    private Color endColor;
    private float lastColorChangeTime;

    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<Renderer>().material;
        startColor = Color1;
        endColor = Color2;
    }

    void Update()
    {
        float ratio = (Time.time - lastColorChangeTime) / FadeDuration;
        ratio = Mathf.Clamp01(ratio);
        material.color = Color.Lerp(startColor, endColor, ratio);

        if (ratio == 1f)
        {
            lastColorChangeTime = Time.time;

            Color temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}
