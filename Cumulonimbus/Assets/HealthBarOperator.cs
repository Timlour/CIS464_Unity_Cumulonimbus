using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarOperator : MonoBehaviour
{
    public Slider Slider1;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    public bool hideUndamaged;
   
    public void SetHealth(float health, float maxHealth)
    {
        if(hideUndamaged){
            Slider1.gameObject.SetActive(health < maxHealth);
        }
        Slider1.value = health;
        Debug.Log("Slider1.value " + health);
        Slider1.maxValue = maxHealth;
        Debug.Log("Slider1.maxValue " + maxHealth);

        Slider1.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider1.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        Slider1.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
