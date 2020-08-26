using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform objectToFollow = null;

    Vector3 objectOffset;

    // Start is called before the first frame update
    private void Awake()
    {
        objectOffset = this.transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        this.transform.position = objectToFollow.position + objectOffset;
    }
}
