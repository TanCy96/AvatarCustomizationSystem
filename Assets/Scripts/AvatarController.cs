using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    private Dictionary<ApparelSlotType, PartAttacher> mAttacherDict = new();
    private ApparelSlotType mCurrentApparelSlotType = ApparelSlotType.Shirt;
    
    private void Awake()
    {
        var attachers = GetComponentsInChildren<PartAttacher>();
        foreach (var attacher in attachers)
        {
            mAttacherDict[attacher.SlotType] = attacher;
        }
    }
    
    public void SetSlotType(int slotTypeIndex)
    {
        if (!System.Enum.IsDefined(typeof(ApparelSlotType), slotTypeIndex))
        {
            Debug.LogError($"Invalid ApparelSlotType index: {slotTypeIndex}");
            return;
        }
        mCurrentApparelSlotType = (ApparelSlotType)slotTypeIndex;
        Debug.Log($"Current Slot {mCurrentApparelSlotType}");
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
