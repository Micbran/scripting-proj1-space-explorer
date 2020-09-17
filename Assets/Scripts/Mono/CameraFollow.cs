using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform objectToFollow = null;

    private Vector3 objectOffset;

    private void Awake()
    {
        objectOffset = this.transform.position - objectToFollow.position;
    }

    private void LateUpdate()
    {
        this.transform.position = objectToFollow.position + objectOffset;
    }
}
