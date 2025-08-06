using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApparelListUI : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private List<ApparelPartsSO> _allPartData;
    [SerializeField] private AvatarController _avatarController; // ugly way but fast
    [SerializeField] private CameraController _camController; // ugly way but fast
    
    private ApparelSlotType mCurrentApparelSlotType = ApparelSlotType.Shirt;
    
    private void Start()
    {
        RefreshUI();
    }
    
    private void RefreshUI()
    {
        foreach(Transform child in _content)
            Destroy(child.gameObject);
        
        var currentSet = _allPartData.FirstOrDefault(p => p.SlotType == mCurrentApparelSlotType);
        if (!currentSet)
        {
            Debug.LogError($"ApparelListUI: No Apparel Slot {mCurrentApparelSlotType}");
            return;
        }
        
        foreach (var part in currentSet.ApparelParts)
        {
            var btnGO = Instantiate(_buttonPrefab, _content);
            var label = btnGO.GetComponentInChildren<TextMeshProUGUI>();
            if (label)
                label.SetText(part.ApparelName);
            
            var btn = btnGO.GetComponent<Button>();
            if (!btn) continue;
            GameObject partPrefab = part.Prefab;
            btn.onClick.AddListener(() =>
            {
                _avatarController.ChangeApparel(partPrefab);
            });
        }
    }
    
    public void OnSlotTypeChanged(int slotTypeIndex)
    {
        if (!Enum.IsDefined(typeof(ApparelSlotType), slotTypeIndex))
        {
            Debug.LogError($"Invalid ApparelSlotType index: {slotTypeIndex}");
            return;
        }
        mCurrentApparelSlotType = (ApparelSlotType)slotTypeIndex;
        _avatarController.SetSlotType(mCurrentApparelSlotType);
        RefreshUI();
        
        if (mCurrentApparelSlotType == ApparelSlotType.Accessory ||
            mCurrentApparelSlotType == ApparelSlotType.Hair)
            _camController.SetFaceCamera();
        else
            _camController.SetFollowCamera();
        
        Debug.Log($"Current Slot {mCurrentApparelSlotType}");
    }
}
