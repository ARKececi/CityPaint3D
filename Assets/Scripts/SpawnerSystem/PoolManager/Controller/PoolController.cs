﻿using System;
using SpawnerSystem.PoolManager.Controller.Interface;
using SpawnerSystem.PoolManager.Data.UnityObject;
using SpawnerSystem.PoolManager.Data.ValueObject;
using SpawnerSystem.PoolManager.Enum;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpawnerSystem.PoolManager.Controller
{
    public class PoolController : Root.SpawnerSystem
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SerializedDictionary<PoolType, PoolData> PoolData = new SerializedDictionary<PoolType, PoolData>();
        [SerializeField] private SerializedDictionary<PoolType, PoolChange> PoolChanges = new SerializedDictionary<PoolType, PoolChange>();
        
        [SerializeField] private GameObject place;

        #endregion

        #region Private Variables

        private readonly string POOL_DATA = "Data/CD_Pool";

        #endregion

        #endregion
        
        #region LocalEvent Subscription
        private void OnEnable()
        {
            spawner.SpawnerID.SpawnerLocalSignals.onListAdd += Listadd;
            spawner.SpawnerID.SpawnerLocalSignals.onListRemove += ListRemove;
        }
        private void OnDisable()
        {
            spawner.SpawnerID.SpawnerLocalSignals.onListAdd -= Listadd;
            spawner.SpawnerID.SpawnerLocalSignals.onListRemove -= ListRemove;
        }
        #endregion
        
        private void Awake()
        {
            PoolData = GetWeaponData();
            foreach (var VARIABLE in PoolData.Keys)
            {
                PoolChanges.Add(VARIABLE,new PoolChange());
            }

            Pooling();
        }
        
        private SerializedDictionary<PoolType, PoolData> GetWeaponData()
        {
            return Resources.Load<CD_Pool>(POOL_DATA).PoolDatas;
        }
        
        private void Pooling()
        {
            foreach (var PoolType in PoolData.Keys)
            {
                GameObject PoolObj = PoolData[PoolType].PoolObj;
                int PoolCount = PoolData[PoolType].PoolCount;
                for (int i = 0; i < PoolCount; i++)
                {
                    var poolObj = Instantiate(PoolObj);
                    Listadd(poolObj, PoolType);
                }
            }
        }
        
        public void Listadd(GameObject poolObj, PoolType poolType)
        {
            PoolChanges[poolType].Pool.Add(poolObj);
            poolObj.transform.SetParent(place.transform,true);
            poolObj.transform.position = Vector3.zero;
            poolObj.gameObject.SetActive(false);
            if (PoolChanges[poolType].Use.Contains(poolObj))
            {
                PoolChanges[poolType].Use.Remove(poolObj);
            }
        }
        
        public GameObject ListRemove(PoolType poolType)
        {
            if (PoolChanges[poolType].Pool.Count != 0)
            {
                GameObject poolObj = PoolChanges[poolType].Pool[0];
                PoolChanges[poolType].Use.Add(poolObj);
                poolObj.gameObject.SetActive(true);
                if (PoolChanges[poolType].Pool.Contains(poolObj))
                {
                    PoolChanges[poolType].Pool.Remove(poolObj);
                }
                return poolObj;
            }
            else return null;
        }

        public void Reset()
        {
            foreach (var keys in PoolData.Keys)
            {
                int keysCount = PoolChanges[keys].Use.Count;
                for (int i = 0; i < keysCount; i++)
                {
                    Listadd(PoolChanges[keys].Use[0],keys);
                }
            }
        }
    }
}