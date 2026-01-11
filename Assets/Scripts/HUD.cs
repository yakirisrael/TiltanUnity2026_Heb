using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider healthBar;
    public void UpdateHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }



    public void UpdateHealthBar(int amount, int maxValue)
    {
        float percentage = amount / (float)maxValue;
        healthBar.value += percentage;
        healthBar.value = Mathf.Clamp(healthBar.value, 0, 1);
    } 
}
