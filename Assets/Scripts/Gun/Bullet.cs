using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IRecovered
{
    [SerializeField] private int damage;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(Tools.Invoke(5, delegate { ObjectPooler.Instance.AddToPool(this); }));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();

        if (enemy != null)
            enemy.TakeDamage(damage);

        Destroy();
    }

    private void Destroy()
    {
        ObjectPooler.Instance.AddToPool(this);
        StopAllCoroutines();
    }

    public void RecoverState()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.ResetCenterOfMass();
        rigidbody.ResetInertiaTensor();
    }
}
