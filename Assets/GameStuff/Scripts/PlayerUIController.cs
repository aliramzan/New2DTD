using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateManaBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)playerController.currentHealth / playerController.maxHealth;
    }

    void UpdateManaBar()
    {
        manaSlider.value = (float)playerController.currentMana / playerController.maxMana;
    }
}

