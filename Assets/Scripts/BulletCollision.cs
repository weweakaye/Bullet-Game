using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public Color bulletColor = Color.red;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we hit is an objective
        ObjectiveTarget objective =
            collision.gameObject.GetComponent<ObjectiveTarget>();

        if (objective != null)
        {
            // Convert the bullet's Color into our BulletColor enum
            BulletColor selectedColor = GetBulletColor();

            // Tell the objective what color was shot
            objective.ReceiveBullet(selectedColor);
        }

        // Keep your existing color-changing system
        ColorChange colorChange =
            collision.gameObject.GetComponent<ColorChange>();

        if (colorChange != null)
        {
            colorChange.ChangeColor(bulletColor);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }

    BulletColor GetBulletColor()
    {
        if (bulletColor == Color.red)
            return BulletColor.Red;

        if (bulletColor == Color.blue)
            return BulletColor.Blue;

        if (bulletColor == Color.green)
            return BulletColor.Green;

        if (bulletColor == Color.yellow)
            return BulletColor.Yellow;

        if (bulletColor == new Color(0.6f, 0f, 1f))
            return BulletColor.Purple;

        if (bulletColor == new Color(1f, 0.5f, 0f))
            return BulletColor.Orange;

        return BulletColor.Red;
    }
}