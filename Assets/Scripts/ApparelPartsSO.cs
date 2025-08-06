using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApparelPart
{
    [SerializeField] private string _apparelName;
    [SerializeField] private GameObject _prefab;
    
    public string ApparelName => _apparelName;
    public GameObject Prefab => _prefab;
}

[CreateAssetMenu(fileName = "ApparelPartsSO", menuName = "Scriptable Objects/ApparelPartsSO")]
public class ApparelPartsSO : ScriptableObject
{
    [SerializeField] private ApparelSlotType _slotType;
    [SerializeField] private List<ApparelPart> _apparelParts;
    
    public ApparelSlotType SlotType => _slotType;
    public List<ApparelPart> ApparelParts => _apparelParts;
}
