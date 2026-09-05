using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text timerText;
    public TMP_Text ammoText;
    public TMP_Text correctText;
    public TMP_Text incorrectText;

    [Header("Windows")]
    public GameObject statsWindow;
    public GameObject loseWindow;

    [Header("Stats")]
    public TMP_Text statsCorrectText;
    public TMP_Text statsIncorrectText;
    public TMP_Text statsAmmoText;
    public TMP_Text statsTimeText;
    public TMP_Text scoreText;

    void Start()
    {
        // Hide result windows at the beginning
        if (statsWindow != null)
            statsWindow.SetActive(false);

        if (loseWindow != null)
            loseWindow.SetActive(false);
    }

    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        if (timerText != null)
        {
            timerText.text =
                "TIME: " +
                minutes.ToString("00") +
                ":" +
                seconds.ToString("00");
        }
    }

    public void UpdateAmmo(int ammo)
    {
        if (ammoText != null)
        {
            ammoText.text = "AMMO: " + ammo;
        }
    }

    public void UpdateStats(int correct, int incorrect)
    {
        if (correctText != null)
        {
            correctText.text = "Correct: " + correct;
        }

        if (incorrectText != null)
        {
            incorrectText.text = "Incorrect: " + incorrect;
        }
    }

    public void ShowStats(
        int correct,
        int incorrect,
        int ammo,
        float time)
    {
        Debug.Log("SHOWING STATS WINDOW");

        if (statsCorrectText != null)
            statsCorrectText.text =
                "Correct Objects: " + correct;

        if (statsIncorrectText != null)
            statsIncorrectText.text =
                "Incorrect Shots: " + incorrect;

        if (statsAmmoText != null)
            statsAmmoText.text =
                "Remaining Ammo: " + ammo;

        if (statsTimeText != null)
            statsTimeText.text =
                "Remaining Time: " +
                Mathf.CeilToInt(time);

        int score =
            (correct * 100) -
            (incorrect * 25) +
            (ammo * 5) +
            Mathf.CeilToInt(time);

        if (score < 0)
            score = 0;

        if (scoreText != null)
            scoreText.text =
                "Score: " + score;

        if (statsWindow != null)
        {
            statsWindow.SetActive(true);
            Debug.Log("StatsWindow was activated!");
        }
        else
        {
            Debug.LogError(
                "Stats Window is NOT assigned in LevelUI!"
            );
        }
    }

    public void ShowLose()
    {
        if (loseWindow != null)
        {
            loseWindow.SetActive(true);
        }
        else
        {
            Debug.LogError(
                "Lose Window is NOT assigned in LevelUI!"
            );
        }
    }
}