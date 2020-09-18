using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinYDirection : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1f;
    [SerializeField] private Transform transformToSpin;

    private void Update()
    {
        transformToSpin.Rotate(0, spinSpeed * Time.deltaTime, 0);    
    }
}
