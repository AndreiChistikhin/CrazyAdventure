using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _cameraDistance;
    [SerializeField] private float _cameraAngle;
    [SerializeField] private float _cameraYOffset;

    private GameObject _objectToFollow;

    private void LateUpdate()
    {
        if (_objectToFollow == null)
            return;

        Quaternion cameraRotation = Quaternion.Euler(_cameraAngle, 0, 0);
        Vector3 cameraPosition = (_objectToFollow.transform.position + Vector3.back * _cameraDistance +
                                 Vector3.up * _cameraYOffset);
        transform.position = cameraPosition;
        transform.rotation = cameraRotation;
    }

    public void Follow(GameObject gameObjectToFollow)
    {
        _objectToFollow = gameObjectToFollow;
    }
}