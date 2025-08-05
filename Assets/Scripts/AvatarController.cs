using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _baseRenderer;
    [SerializeField] private PartAttacher _attacher;
    
    public void EquipShirt(GameObject prefab)
    {
        _attacher.AttachPart(_baseRenderer, prefab, PartSlotType.Shirt);
    }
}
