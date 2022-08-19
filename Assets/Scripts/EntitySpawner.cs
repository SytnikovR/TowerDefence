using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class EntitySpawner : Spawner
    {
       /// <summary>
       /// Ссылки на то что спавнить.
       /// </summary>
       [SerializeField] private GameObject[] m_EntityPrefabs;      
      
        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[UnityEngine.Random.Range(0, m_EntityPrefabs.Length)]);
        }
     }
}