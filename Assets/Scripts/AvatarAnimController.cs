using UnityEngine;

public class AvatarAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private bool mIsMoving = false;
    
    public void ToggleMoving()
    {
        mIsMoving = !mIsMoving;
        _animator.SetBool("Move", mIsMoving);
    }
}
