using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text timerText;
    public TMP_Text ammoText;
    public TMP_Text dummiesKilledText;

    [Header("Windows")]
    public GameObject statsWindow;
    public GameObject loseWindow;

    [Header("Week 11 Statistics")]
    public TMP_Text statsAmmoPickedUpText;
    public TMP_Text statsShotsFiredText;
    public TMP_Text statsDummiesKilledText;

    void Start()
    {
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

    public void UpdateDummiesKilled(int killed, int required)
    {
        if (dummiesKilledText != null)
        {
            dummiesKilledText.text =
                "DUMMIES: " + killed + " / " + required;
        }
    }

    public void ShowWeek11Victory(
        int ammoPickedUp,
        int shotsFired,
        int dummiesKilled)
    {
        if (statsAmmoPickedUpText != null)
        {
            statsAmmoPickedUpText.text =
                "Ammo Picked Up: " + ammoPickedUp;
        }

        if (statsShotsFiredText != null)
        {
            statsShotsFiredText.text =
                "Shots Fired: " + shotsFired;
        }

        if (statsDummiesKilledText != null)
        {
            statsDummiesKilledText.text =
                "Dummies Killed: " + dummiesKilled;
        }

        if (statsWindow != null)
        {
            statsWindow.SetActive(true);
        }
    }

    public void ShowWeek11Defeat(
        int ammoPickedUp,
        int shotsFired,
        int dummiesKilled)
    {
        if (statsAmmoPickedUpText != null)
        {
            statsAmmoPickedUpText.text =
                "Ammo Picked Up: " + ammoPickedUp;
        }

        if (statsShotsFiredText != null)
        {
            statsShotsFiredText.text =
                "Shots Fired: " + shotsFired;
        }

        if (statsDummiesKilledText != null)
        {
            statsDummiesKilledText.text =
                "Dummies Killed: " + dummiesKilled;
        }

        if (loseWindow != null)
        {
            loseWindow.SetActive(true);
        }

        Invoke(nameof(ReturnToMainMenu), 3f);
    }

    void ReturnToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMenu();
        }
    }
}