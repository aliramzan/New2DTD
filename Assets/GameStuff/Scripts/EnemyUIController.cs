using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider healthSlider;

    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)enemyController.currentHealth / enemyController.maxHealth;
    }
}

