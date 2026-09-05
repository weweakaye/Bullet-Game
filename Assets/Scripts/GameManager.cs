using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Settings")]
public int startingAmmo = 10;
public float levelTime = 60f;
public int requiredObjectives = 4;

    [Header("UI")]
    public LevelUI levelUI;

    private int ammoRemaining;
    private float timeRemaining;

    private int correctObjects = 0;
    private int incorrectShots = 0;

    private List<ObjectiveTarget> objectives =
        new List<ObjectiveTarget>();

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

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateUI();
            LoseLevel();
            return;
        }

        UpdateUI();
    }

    // =========================
    // OBJECTIVES
    // =========================

    public void RegisterObjective(ObjectiveTarget target)
    {
        if (!objectives.Contains(target))
        {
            objectives.Add(target);
        }
    }

    public void ObjectHit(
        ObjectiveTarget target,
        BulletColor required,
        BulletColor actual)
    {
        if (levelFinished)
            return;

        if (actual == required)
        {
            correctObjects++;

            Debug.Log(
                "CORRECT! " +
                target.name +
                " | Correct: " +
                correctObjects
            );
        }
        else
        {
            incorrectShots++;

            Debug.Log(
                "INCORRECT! " +
                target.name +
                " | Incorrect: " +
                incorrectShots
            );
        }

        UpdateUI();

        CheckWin();
    }

    // =========================
    // AMMO
    // =========================

   public bool TryUseAmmo()
{
    if (levelFinished)
        return false;

    if (ammoRemaining <= 0)
    {
        Debug.Log("NO AMMO!");
        LoseLevel();
        return false;
    }

    ammoRemaining--;

    Debug.Log("SHOT FIRED! Ammo remaining: " + ammoRemaining);

    UpdateUI();

    return true;
}

    // =========================
    // WIN / LOSE
    // =========================

    void CheckWin()
{
    Debug.Log(
        "CHECK WIN → Correct: " +
        correctObjects +
        " | Objectives registered: " +
        objectives.Count
    );

    if (correctObjects >= requiredObjectives)
{
    WinLevel();
}
}

    void WinLevel()
    {
        levelFinished = true;

        Debug.Log("LEVEL COMPLETE!");

        if (levelUI != null)
        {
            levelUI.ShowStats(
                correctObjects,
                incorrectShots,
                ammoRemaining,
                timeRemaining
            );
        }
    }

    void LoseLevel()
    {
        if (levelFinished)
            return;

        levelFinished = true;

        Debug.Log("YOU LOSE!");

        if (levelUI != null)
        {
            levelUI.ShowLose();
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
        levelUI.UpdateStats(
            correctObjects,
            incorrectShots
        );
    }

    // =========================
    // BUTTONS
    // =========================

    public void GoToNextLevel()
{
    SceneManager.LoadScene("GameLevel2");
}

    public void RetryLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMenu()
    {
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