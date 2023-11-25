using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : BaseEnemy
{
    private void FixedUpdate()
    {
        if (_agent.IsStoppedByDistance)
        {
            _visual.transform.up = (_player.position - transform.position).normalized;
        }
    }

}
