using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }
        private event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {

            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        [SerializeField] private int m_gold = 0;        

        public void ChangeGold(int change)
        {
            if(m_gold + change < 0)
            {
                return;
            }
            m_gold += change;
            OnGoldUpdate(m_gold);        
        }
        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private Tower m_towerPrefab;

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
          var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
           Destroy(buildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset healthUpgrade;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }
    }
}

