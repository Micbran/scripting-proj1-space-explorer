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
        if(objectToFollow != null)
            this.transform.position = objectToFollow.position + objectOffset;
    }

    public void ChangeFollow(Transform newFollow)
    {
        objectToFollow = newFollow;
        this.transform.position = objectToFollow.position + objectOffset;
    }
}
