using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet prefab;

    [SerializeField] Transform spawningPoint;
    [SerializeField] float shootForce;

    public void Shot(Vector3 target)
    {
        Vector3 direction = (target - spawningPoint.position).normalized;

        GameObject spawnedBullet = ObjectPooler.Instance.CreateObject(prefab, spawningPoint.position, prefab.transform.rotation);

        spawnedBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    }
}
