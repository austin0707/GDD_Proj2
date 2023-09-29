using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTController : ExplosiveController
{
    #region Explosion Methods
    public override void ExplosiveCheck()
    {
        return;
    }
    #endregion

    #region Collision Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            base.MakeExplode();
        }
    }
    #endregion
}
