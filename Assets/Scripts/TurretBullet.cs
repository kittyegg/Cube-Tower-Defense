using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 3f;

    private Vector3 _spawnPos;

    public float Damage => _damage;
    public float Distance => _distance;
    
    private void Start()
    {
        _spawnPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        if (Vector3.Distance(transform.position, _spawnPos) > _distance)
            Destroy(gameObject);
    }
}
