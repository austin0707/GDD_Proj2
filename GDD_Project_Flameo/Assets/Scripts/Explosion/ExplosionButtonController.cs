using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionButtonController : ExplosiveController
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
        GameObject hit = collision.collider.gameObject;
        if (hit.CompareTag("Player"))
        {
            if (hit.GetComponent<PlayerController>().CheckBelowTag(m_IsExplosive))
            {
                base.MakeExplode();
            }
        }
    }
    #endregion
}
