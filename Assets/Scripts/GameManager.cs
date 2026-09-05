using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Settings")]
    public int startingAmmo = 10;
    public float levelTime = 60f;
    public int requiredDummies = 5;

    [Header("UI")]
    public LevelUI levelUI;

    private int ammoRemaining;
    private float timeRemaining;

    private int ammoPickedUp = 0;
    private int shotsFired = 0;
    private int dummiesKilled = 0;

    private bool levelFinished = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        ammoRemaining = startingAmmo;
        timeRemaining = levelTime;

        UpdateUI();
    }

    void Update()
    {
        if (levelFinished)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            UpdateUI();

            LoseLevel();

            return;
        }

        UpdateUI();
    }

    // =========================
    // SHOOTING / AMMO
    // =========================

    public bool TryUseAmmo()
    {
        if (levelFinished)
            return false;

        if (ammoRemaining <= 0)
        {
            Debug.Log("NO AMMO!");

            // IMPORTANT:
            // Running out of ammo does NOT cause defeat.
            return false;
        }

        ammoRemaining--;

        shotsFired++;

        Debug.Log(
            "SHOT FIRED! Ammo remaining: " +
            ammoRemaining
        );

        UpdateUI();

        return true;
    }

    public void RefillAmmo()
    {
        if (levelFinished)
            return;

        ammoRemaining = startingAmmo;

        ammoPickedUp++;

        Debug.Log(
            "AMMO PICKED UP! Total pickups: " +
            ammoPickedUp
        );

        UpdateUI();
    }

    // =========================
    // TRAINING DUMMIES
    // =========================

    public void DummyKilled()
    {
        if (levelFinished)
            return;

        dummiesKilled++;

        Debug.Log(
            "DUMMY KILLED! " +
            dummiesKilled +
            "/" +
            requiredDummies
        );

        UpdateUI();

        if (dummiesKilled >= requiredDummies)
        {
            WinLevel();
        }
    }

    // =========================
    // WIN
    // =========================

    void WinLevel()
    {
        if (levelFinished)
            return;

        levelFinished = true;

        Debug.Log("========== VICTORY ==========");

        if (levelUI != null)
        {
            levelUI.ShowWeek11Victory(
                ammoPickedUp,
                shotsFired,
                dummiesKilled
            );
        }
    }

    // =========================
    // LOSE
    // =========================

    void LoseLevel()
    {
        if (levelFinished)
            return;

        levelFinished = true;

        Debug.Log("========== DEFEAT ==========");

        if (levelUI != null)
        {
            levelUI.ShowWeek11Defeat(
                ammoPickedUp,
                shotsFired,
                dummiesKilled
            );
        }
        else
        {
            ReturnToMenu();
        }
    }

    // =========================
    // UI
    // =========================

    void UpdateUI()
{
    if (levelUI == null)
        return;

    levelUI.UpdateTimer(timeRemaining);
    levelUI.UpdateAmmo(ammoRemaining);

    levelUI.UpdateDummiesKilled(
        dummiesKilled,
        requiredDummies
    );
}

    // =========================
    // BUTTONS
    // =========================

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // =========================
    // SHOOTING CHECK
    // =========================

    public bool CanShoot()
    {
        return !levelFinished &&
               ammoRemaining > 0;
    }
}