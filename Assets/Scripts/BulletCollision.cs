using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        DummyHealth dummy =
            collision.gameObject.GetComponentInParent<DummyHealth>();

        if (dummy != null)
        {
            dummy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}