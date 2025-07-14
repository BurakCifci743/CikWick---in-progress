using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] public float _force = 10;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
        Destroy(gameObject);

    }
}
