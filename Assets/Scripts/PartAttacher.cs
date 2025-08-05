using UnityEngine;

public enum PartSlotType
{
    Shirt,
    Pants,
    Shoes,
}

public class PartAttacher : MonoBehaviour
{
    public GameObject AttachPart(SkinnedMeshRenderer avatarRenderer, GameObject partPrefab, PartSlotType slotType)
    {
        GameObject part = Instantiate(partPrefab, transform);
        SkinnedMeshRenderer partRenderer = partPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
        
        if (!partRenderer)
        {
            Debug.LogError(partPrefab.name + " has no SkinnedMeshRenderer");
            Destroy(part);
            return null;
        }
        
        GameObject partObject = new GameObject(slotType.ToString());
        partObject.transform.SetParent(transform);
        
        var newRenderer = partObject.AddComponent<SkinnedMeshRenderer>();
        newRenderer.sharedMesh = partRenderer.sharedMesh;
        newRenderer.materials = partRenderer.sharedMaterials;
        newRenderer.bones = avatarRenderer.bones;
        newRenderer.rootBone = avatarRenderer.rootBone;
        
        Destroy(part);
        return partObject;
    }
}
