using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Health playerHealth;
    public Image fillImage;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)  // If less than or equal to 0
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !(fillImage.enabled)) {
            fillImage.enabled = true;
        }

        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth; // currentHealth and maxHealth must be type float in Health.cs
        /*if (fillValue <= slider.maxValue / 3) // Does not return to red on heal
        {
            fillImage.color = Color.blue; // critical condition
        }*/
        slider.value = fillValue;
    }
}
