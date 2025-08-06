using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _followCam;
    [SerializeField] private GameObject _faceCam;
    [SerializeField] private Transform _pivotTarget;
    [SerializeField] private float _rotationSpeed = 100f;
    
    private GameObject mCurrentCam;
    private float mCurrentAngle = 0f;
    
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        mCurrentAngle += inputX * _rotationSpeed * Time.deltaTime;
        
        _pivotTarget.rotation = Quaternion.Euler(0f, mCurrentAngle, 0f);
    }
    
    public void SetFollowCamera()
    {
        if (mCurrentCam == _followCam) return;
        
        _followCam.SetActive(true);
        if (mCurrentCam)
            mCurrentCam.SetActive(false);
        mCurrentCam = _followCam;
    }
    
    public void SetFaceCamera()
    {
        if (mCurrentCam == _faceCam) return;
        
        _faceCam.SetActive(true);
        if (mCurrentCam)
            mCurrentCam.SetActive(false);
        mCurrentCam = _faceCam;
    }
}
