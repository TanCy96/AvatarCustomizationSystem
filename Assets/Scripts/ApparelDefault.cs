using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AvatarController))]
public class ApparelDefault : MonoBehaviour
{
    [SerializeField] private List<ApparelPartsSO> _allPartData;
    
    private AvatarController mAvatarController;
    
    private void Awake()
    {
        mAvatarController = GetComponent<AvatarController>();
    }
    
    private void OnEnable()
    {
        if (!mAvatarController.IsInit)
        {
            mAvatarController.OnInitialized += SetAvatarDefaultApparel;
            return;
        }
        SetAvatarDefaultApparel();
    }
    
    private void SetAvatarDefaultApparel()
    {
        mAvatarController.OnInitialized -= SetAvatarDefaultApparel;
        foreach (ApparelSlotType slotType in Enum.GetValues(typeof(ApparelSlotType)))
        {
            mAvatarController.SetSlotType(slotType);
            var currentSet = _allPartData.FirstOrDefault(p => p.SlotType == slotType);
            if (!currentSet)
                continue;
            
            int index = Random.Range(0, currentSet.ApparelParts.Count);
            var part = currentSet.ApparelParts[index];
            mAvatarController.ChangeApparel(part.Prefab);
        }
        mAvatarController.SetSlotType(ApparelSlotType.Shirt);
    }
}
