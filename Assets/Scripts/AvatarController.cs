using System;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    private Dictionary<ApparelSlotType, PartAttacher> mAttacherDict = new();
    private ApparelSlotType mCurrentApparelSlotType = ApparelSlotType.Shirt;
    
    public bool IsInit { get; private set; }
    
    public event Action OnInitialized;
    
    private void Awake()
    {
        var attachers = GetComponentsInChildren<PartAttacher>();
        foreach (var attacher in attachers)
        {
            mAttacherDict[attacher.SlotType] = attacher;
        }
        IsInit = true;
        OnInitialized?.Invoke();
    }
    
    public void SetSlotType(ApparelSlotType slotType)
    {
        mCurrentApparelSlotType = slotType;
    }
    
    public void ChangeApparel(GameObject newApparel)
    {
        if (!mAttacherDict.ContainsKey(mCurrentApparelSlotType))
        {
            Debug.LogError($"Missing {mCurrentApparelSlotType} Apparel Slot");
            return;
        }
        
        if (!mAttacherDict[mCurrentApparelSlotType])
        {
            Debug.LogError($"Null {mCurrentApparelSlotType} Apparel Slot");
            return;
        }
        
        mAttacherDict[mCurrentApparelSlotType].AttachPart(newApparel);
    }
}
