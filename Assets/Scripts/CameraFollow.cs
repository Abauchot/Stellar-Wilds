using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float smoothSpeed = 0.125f;
    [SerializeField] public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
