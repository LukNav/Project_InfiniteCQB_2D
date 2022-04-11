using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public StatsController playerStatsController;
    public GameObject deathScreen;
    public GameObject inGameScreen;
    public Slider hpSlider;
    public TMP_Text consumableCounterText;
    public Slider fireRateSlider;

    public delegate void UpdateFireRateSlider(float value);
    public static UpdateFireRateSlider OnUpdateFireRateSlider;

    private int healthConsumableCounter = 0;

    public void Start()
    {
        hpSlider.value = playerStatsController.initialHealth;
        playerStatsController.deathDelegate += SetActiveDeathScreen;
        playerStatsController.healthUpdateDelegate += UpdateHealthBar;
        Consumable.healthConsumableDelegate += ConsumableCounter_AddOne;
        OnUpdateFireRateSlider += UpdateFireRateComponent;
    }

    private void UpdateHealthBar()
    {
        hpSlider.value = playerStatsController.health/ playerStatsController.initialHealth;
    }

    private void SetActiveDeathScreen()
    {
        inGameScreen.SetActive(false);
        deathScreen.SetActive(true);
    }

    private void ConsumableCounter_AddOne(float healthAdded)
    {
        healthConsumableCounter++;
        consumableCounterText.text = healthConsumableCounter.ToString();
    }


    /// <param name="value">Slider value [0;1]</param>
    private void UpdateFireRateComponent(float value)
    {
        fireRateSlider.value = value;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnStart()
    {
        SceneManager.LoadScene(0);
    }
}