using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // reference to the slider within the health bar
    public Gradient gradient; // reference to the gradient used to change the colour of the health bar
    public Image fill; // reference to the image that displays the colour
    
    public void SetMaxHealth(int health) // Set the slider to the max it can be
    {
        slider.maxValue = health; // Set max value to the value passed through the function
        slider.value = health; // set the current value to the value pass through the function

        fill.color = gradient.Evaluate(1f); // Set the colour of the fill to 1
    }

    public void setHealth(int health) // 
    {
        slider.value = health; // Set the value to the value passed through the function

        fill.color = gradient.Evaluate(slider.normalizedValue); // Set the gradient position to the normalised value (between 0 and 1)
    }
}
