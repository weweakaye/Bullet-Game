using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 25;

    private bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Make sure one bullet can only process one collision.
        if (hasHit)
            return;

        hasHit = true;

        DummyHealth dummy =
            collision.gameObject.GetComponentInParent<DummyHealth>();

        if (dummy != null)
        {
            dummy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}