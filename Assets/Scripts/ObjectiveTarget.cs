using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    [Header("Objective")]
    public BulletColor requiredColor;

    [Header("Visual")]
    public Renderer targetRenderer;

    private bool completed = false;

    void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterObjective(this);
        }
    }

    public void ReceiveBullet(BulletColor bulletColor)
    {
        if (completed)
            return;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ObjectHit(
                this,
                requiredColor,
                bulletColor
            );
        }

        if (bulletColor == requiredColor)
        {
            completed = true;

            if (targetRenderer != null)
            {
                targetRenderer.material.color =
                    GetUnityColor(bulletColor);
            }
        }
    }

    Color GetUnityColor(BulletColor color)
    {
        switch (color)
        {
            case BulletColor.Red:
                return Color.red;

            case BulletColor.Blue:
                return Color.blue;

            case BulletColor.Green:
                return Color.green;

            case BulletColor.Yellow:
                return Color.yellow;

            case BulletColor.Purple:
                return new Color(0.6f, 0f, 1f);

            case BulletColor.Orange:
                return new Color(1f, 0.5f, 0f);
        }

        return Color.white;
    }
}