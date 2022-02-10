using UnityEngine;

/// <summary>
/// Its a timer, runs until timerEnd value. 
/// </summary>
[System.Serializable]
public class Timer
{
    public float currentTime = 0f;
    public float timerEnd;

    bool timerStarted = false;
    bool timerEnded = false;
    public bool IsRunning { get { return timerStarted && !timerEnded; } }
    public bool HasEnded { get { return timerEnded && !timerStarted; } }
    public bool NotStarted { get { return !timerEnded && !timerStarted; } }

    public Timer(float timerEnd)
    {
        this.timerEnd = timerEnd;
    }

    public void Update()//NOTE!!! make sure to update the timer using this method.
    {
        if (!timerEnded && timerStarted)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timerEnd)
            {
                timerEnded = true;
                timerStarted = false;
            }
        }
    }

    public void StartIfNotRunning()
    {
        if(NotStarted)
        {
            timerStarted = true;
            timerEnded = false;
            currentTime = 0f;
        }
    }
    public void Stop()
    {
        timerStarted = false;
        timerEnded = false;
        currentTime = 0f;
    }

    public void Restart()
    {
        if(HasEnded)
        {
            timerStarted = true;
            timerEnded = false;
            currentTime = 0f;
        }
    }

    public void StopIfEnded()
    {
        if (HasEnded)
        {
            timerStarted = false;
            timerEnded = false;
            currentTime = 0f;
        }
    }
}