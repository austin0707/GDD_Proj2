using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpawnerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The explosives to be spawned")]
    private GameObject m_SpawnedExplosive;

    [SerializeField]
    [Tooltip("Number of explosives to be spawned at a time")]
    private int m_NumExplosives;

    [SerializeField]
    [Tooltip("Spawn delay between spawning explosives")]
    private float m_SpawnDelay;
    #endregion

    #region Private Variables
    private int p_CurrNumExplosives;
    private float p_TimeToNextSpawn;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_CurrNumExplosives = 0;
        p_TimeToNextSpawn = 0;
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        if (p_TimeToNextSpawn <= 0 && p_CurrNumExplosives < m_NumExplosives)
        {
            GameObject go = Instantiate(m_SpawnedExplosive, transform.position, Quaternion.identity);
            go.GetComponent<ExplosiveController>().SetSpawner(gameObject);
            p_CurrNumExplosives += 1;
            p_TimeToNextSpawn = m_SpawnDelay;
        } else
        {
            p_TimeToNextSpawn -= Time.deltaTime;
        }
    }
    #endregion

    #region Misc Methods
    public void ReduceCounter()
    {
        p_CurrNumExplosives -= 1;
    }
    #endregion
}
