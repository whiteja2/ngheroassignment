using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCooldown : MonoBehaviour
{

    public Slider slider;
    public float duration = 0.2f;
    private bool isDecreasing = false;
    private float currentValue = 1f;
    private float targetValue = 0f;
    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDecreasing)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            slider.value = Mathf.Lerp(currentValue, targetValue, t);

            if (t >= 1f)
            {
                isDecreasing = false;
            }
        }
    }

    public void EggFired()
    {
        isDecreasing = true;
        slider.value = 1f;
        currentValue = slider.value;
        elapsedTime = 0f;
    }
}
