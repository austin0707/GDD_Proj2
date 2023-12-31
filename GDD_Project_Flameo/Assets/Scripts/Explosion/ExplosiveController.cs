using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much force is generated by the explosion")]
    private float m_ExplosiveForce;

    [SerializeField]
    [Tooltip("How far away the explosion affects objects")]
    private float m_ExplosiveRadius;

    [SerializeField]
    [Tooltip("Modifier that launches player more upwards")]
    private float m_UpwardsModifier = 0.0f;

    [SerializeField]
    [Tooltip("Is the explosive destroyed afterwards")]
    private bool m_Destroyed;

    [SerializeField]
    [Tooltip("Particles when the exploside explodes")]
    private GameObject m_ExplosionParticles;
    #endregion

    #region Private Variables
    private bool p_ShouldExplode;
    private bool p_IsDestroyed;
    private AudioSource p_Audio;
    #endregion

    #region Other Variables
    public GameObject spawner = null;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_ShouldExplode = false;
        p_IsDestroyed = false;
    }

    private void Start()
    {
        p_Audio = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        ExplosiveCheck();
        if (p_ShouldExplode && !p_IsDestroyed)
        {
            Explode();
        }
    }
    #endregion

    #region Explosion Methods
    private void Explode()
    {
        var surroundingObjects = Physics.OverlapSphere(transform.position, m_ExplosiveRadius);

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(m_ExplosiveForce, transform.position, m_ExplosiveRadius, m_UpwardsModifier);
        }
        Instantiate(m_ExplosionParticles, transform.position, Quaternion.identity);

        p_Audio.Play();

        if (m_Destroyed)
        {
            p_IsDestroyed = true;
            Destroy(gameObject);
            
            if (spawner != null)
            {
                spawner.GetComponent<ExplosiveSpawnerController>().ReduceCounter();
            }
        } else
        {
            p_ShouldExplode = false;
        }
    }

    public virtual void ExplosiveCheck()
    {
        return;
    }

    public void MakeExplode()
    {
        p_ShouldExplode = true;
    }
    #endregion

    #region Misc Methods
    public void SetSpawner(GameObject spawn)
    {
        spawner = spawn;
    }
    #endregion
}
