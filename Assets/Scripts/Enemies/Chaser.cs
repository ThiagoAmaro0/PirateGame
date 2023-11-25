using System;
using UnityEngine;

public class Chaser : BaseEnemy
{
    [SerializeField] private HurtfulObject _hurtfulObject;
    protected override void Awake()
    {
        base.Awake();
        _hurtfulObject.OnHit += Explode;
    }

    private void Explode()
    {
        print("BOOM");
    }
}