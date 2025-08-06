using Unity.VisualScripting;
using UnityEngine;

public class PartAttacher : MonoBehaviour
{
    [SerializeField] private ApparelSlotType _slotType;
    [SerializeField] private SkinnedMeshRenderer _baseRenderer;
    
    private GameObject mActivePart;
    
    public ApparelSlotType SlotType => _slotType;
    
    public void AttachPart(GameObject partPrefab)
    {
        GameObject part = Instantiate(partPrefab, transform);
        SkinnedMeshRenderer partRenderer = partPrefab.GetComponent<SkinnedMeshRenderer>();
        // Prefab might have renderer in children instead
        if (!partRenderer)
            partRenderer = partPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
        
        if (!partRenderer)
        {
            Debug.LogError(partPrefab.name + " has no SkinnedMeshRenderer");
            Destroy(part);
            return;
        }
        
        if (!mActivePart)
        {
            mActivePart = new GameObject();
            mActivePart.transform.SetParent(transform);
        }
        
        var newRenderer = mActivePart.GetOrAddComponent<SkinnedMeshRenderer>();
        newRenderer.sharedMesh = partRenderer.sharedMesh;
        newRenderer.materials = partRenderer.sharedMaterials;
        newRenderer.bones = _baseRenderer.bones;
        newRenderer.rootBone = _baseRenderer.rootBone;
        
        Destroy(part);
    }
}
