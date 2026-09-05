using UnityEngine;
using UnityEngine.UI;

public class DummyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;

    private float currentHealth;
    private bool isDead = false;

    [Header("Health Bar")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;

        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        // Do nothing if this dummy is already dead.
        if (isDead)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        UpdateHealthBar();

        Debug.Log(
            "Training Dummy HP: " + currentHealth
        );

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        // Prevent the death logic from running more than once.
        if (isDead)
            return;

        isDead = true;

        Debug.Log("TRAINING DUMMY DIED!");

        // Count this dummy exactly once.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DummyKilled();
        }

        Destroy(gameObject);
    }
}