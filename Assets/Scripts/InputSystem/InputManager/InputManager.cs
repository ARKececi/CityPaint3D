using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsSystem.Weapons.WeaponRoot.Signals;

namespace Ahmet
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject _ballPrefab;
        private P3dPaintDecal _p3dPaintDecal;

        private void Awake()
        {
            _p3dPaintDecal = _ballPrefab.GetComponentInChildren<P3dPaintDecal>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    WeaponSignals.Instance.onAimPosition?.Invoke(hit.point);
                    // if (hit.collider.TryGetComponent(out Npc npc))
                    // {
                    //     _p3dPaintDecal.Scale = new Vector3(3f, 3f, 3f);
                    //     _p3dPaintDecal.Color = npc.Color;
                    //     Destroy(npc.gameObject);
                    //     Instantiate(_ballPrefab, npc.transform.position, Quaternion.identity);
                    // }
                    // else
                    // {
                    //     _p3dPaintDecal.Scale = new Vector3(1f, 1f, 1f);
                    //     Instantiate(_ballPrefab, hit.point, Quaternion.identity);
                    // }
                }
            }
        }
    }
}