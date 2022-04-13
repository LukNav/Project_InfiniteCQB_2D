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
    public GameObject pauseScreen;
    public Slider hpSlider;
    public TMP_Text consumableCounterText;
    public Slider fireRateSlider;

    public delegate void UpdateFireRateSlider(float value);
    public static UpdateFireRateSlider OnUpdateFireRateSlider;

    public delegate void ToggleParticles(bool value);
    public static ToggleParticles OnParticlesToggle;
    private bool _areParticlesActive = true;

    private bool _isPaused = false;

    private int healthConsumableCounter = 0;

    public void Start()
    {
        hpSlider.value = playerStatsController.initialHealth;
        playerStatsController.deathDelegate += SetActiveDeathScreen;
        playerStatsController.healthUpdateDelegate += UpdateHealthBar;
        Consumable.healthConsumableDelegate += ConsumableCounter_AddOne;
        OnUpdateFireRateSlider += UpdateFireRateComponent;
        PlayerInputController.input_Pause += OnPause;
    }

    public void OnToggleParticles()
    {
        _areParticlesActive = !_areParticlesActive;
        OnParticlesToggle(_areParticlesActive);
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

    private void OnPause()
    {
        if (playerStatsController.health <= 0)
            return;

        _isPaused = !_isPaused;
        if (_isPaused)
        {
            pauseScreen.SetActive(true);
            inGameScreen.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            inGameScreen.SetActive(true);
            Time.timeScale = 1f;
        }
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