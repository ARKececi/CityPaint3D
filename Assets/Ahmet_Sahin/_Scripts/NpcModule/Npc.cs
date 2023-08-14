using System.Collections;
using System.Collections.Generic;
using SpawnerSystem.PoolManager.Controller.Interface;
using UnityEngine;

namespace Ahmet
{
    public class Npc : MonoBehaviour
    {

        public NpcSO npcSO;
        private Material _material;
        public Color Color;

        private void OnEnable()
        {
            npcSO.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0,1f));
            _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
            _material.color = npcSO.color;
            Color = npcSO.color;
        }
    }
}