using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonBall _projectilePrefab;

    public void Fire(Collider2D ownerCollider)
    {
        CannonBall cannonBall = Instantiate(_projectilePrefab);
        cannonBall.transform.position = transform.position;
        cannonBall.SetOwner(ownerCollider);
        cannonBall.Fire(transform.up);


    }

    public void Fire(Collider2D ownerCollider, float delay)
    {
        StartCoroutine(DelayFire(ownerCollider, delay));
    }

    private IEnumerator DelayFire(Collider2D ownerCollider, float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire(ownerCollider);
    }
}