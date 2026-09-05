using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Shooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;

    [Header("Bullet Colors")]
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    public Color greenColor = Color.green;
    public Color yellowColor = Color.yellow;
    public Color purpleColor = new Color(0.6f, 0f, 1f);
    public Color orangeColor = new Color(1f, 0.5f, 0f);

    [Header("UI")]
    public TMP_Text bulletText;

    private Color currentBulletColor;

    void Start()
    {
        currentBulletColor = redColor;
        UpdateBulletUI();
    }

    void Update()
    {
        SelectBulletColor();

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void SelectBulletColor()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentBulletColor = redColor;
            UpdateBulletUI();
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentBulletColor = blueColor;
            UpdateBulletUI();
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentBulletColor = greenColor;
            UpdateBulletUI();
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentBulletColor = yellowColor;
            UpdateBulletUI();
        }

        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            currentBulletColor = purpleColor;
            UpdateBulletUI();
        }

        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            currentBulletColor = orangeColor;
            UpdateBulletUI();
        }
    }

    void UpdateBulletUI()
    {
        if (bulletText == null)
        {
            return;
        }

        if (currentBulletColor == redColor)
        {
            bulletText.text = "LOADED: RED";
        }
        else if (currentBulletColor == blueColor)
        {
            bulletText.text = "LOADED: BLUE";
        }
        else if (currentBulletColor == greenColor)
        {
            bulletText.text = "LOADED: GREEN";
        }
        else if (currentBulletColor == yellowColor)
        {
            bulletText.text = "LOADED: YELLOW";
        }
        else if (currentBulletColor == purpleColor)
        {
            bulletText.text = "LOADED: PURPLE";
        }
        else if (currentBulletColor == orangeColor)
        {
            bulletText.text = "LOADED: ORANGE";
        }
    }

    public void Shoot()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.TryUseAmmo())
                return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation
        );

        Renderer bulletRenderer =
            bullet.GetComponent<Renderer>();

        if (bulletRenderer != null)
        {
            bulletRenderer.material.color =
                currentBulletColor;
        }

        Rigidbody rb =
            bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                bulletSpawnPoint.forward * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }

    public void SelectRed()
    {
        currentBulletColor = redColor;
        UpdateBulletUI();
    }

    public void SelectBlue()
    {
        currentBulletColor = blueColor;
        UpdateBulletUI();
    }

    public void SelectGreen()
    {
        currentBulletColor = greenColor;
        UpdateBulletUI();
    }

    public void SelectYellow()
    {
        currentBulletColor = yellowColor;
        UpdateBulletUI();
    }

    public void SelectPurple()
    {
        currentBulletColor = purpleColor;
        UpdateBulletUI();
    }

    public void SelectOrange()
    {
        currentBulletColor = orangeColor;
        UpdateBulletUI();
    }
}