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
    public TMP_Text consumableCounter;

    private int healthConsumableCounter = 0;

    public void Start()
    {
        hpSlider.value = playerStatsController.initialHealth;
        playerStatsController.deathDelegate += SetActiveDeathScreen;
        playerStatsController.healthUpdateDelegate += UpdateHealthBar;
        playerStatsController.healthConsumableDelegate += ConsumableCounter_AddOne;
    }

    private void UpdateHealthBar()
    {
        hpSlider.value = playerStatsController.health/ playerStatsController.initialHealth;
        consumableCounter.text = consumableCounter.ToString();
    }

    private void SetActiveDeathScreen()
    {
        inGameScreen.SetActive(false);
        deathScreen.SetActive(true);
    }

    private void ConsumableCounter_AddOne()
    {
        consumableCounter.text = consumableCounter.ToString();
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