using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class AvatarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _avatarPrefab;
    
    private ObjectPool<GameObject> mAvatarPool;
    private List<GameObject> mAvatarList = new();
    
    private void Awake()
    {
        mAvatarPool = new ObjectPool<GameObject>(OnAvatarCreated, OnAvatarSpawned, OnAvatarReleased);
    }
    
    public void OnSliderUpdated(float value)
    {
        int intValue = (int)value;
        var difference = intValue - mAvatarList.Count;
        if (difference == 0)
            return;
        
        if (difference < 0)
        {
            // Despawn
            for (int i = difference; i < 0; i++)
            {
                if (mAvatarList.Count <= 0) break;
                var index = Random.Range(0, mAvatarList.Count);
                mAvatarPool.Release(mAvatarList[index]);
                mAvatarList.RemoveAt(index);
            }
            return;
        }
        
        // Spawn
        for (var i = 0; i < difference; i++)
        {
            var newGO = mAvatarPool.Get();
            newGO.transform.position = new Vector3(Random.Range(-7f, 7f), 0, Random.Range(-7f, 7f));
            newGO.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
            AvatarAnimController animCtrl = newGO.GetComponent<AvatarAnimController>();
            if (animCtrl && Random.Range(0f, 1f) > 0.5f)
                animCtrl.ToggleMoving();
            
            mAvatarList.Add(newGO);
        }
    }
    
    private GameObject OnAvatarCreated()
    {
        var go = Instantiate(_avatarPrefab, transform);
        return go;
    }
    
    private void OnAvatarSpawned(GameObject avatar)
    {
        avatar.SetActive(true);
    }
    
    private void OnAvatarReleased(GameObject avatar)
    {
        avatar.SetActive(false);
    }
}
