using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    [Header("Position")]
    public Transform Target;
    public Vector3 Offset;

    [Header("Speed")]
    public float SmoothSpeed = 1f;
    public float MaxSpeed = 5f;

    private Vector3 velocity;
    
    void LateUpdate() {
        Vector3 targetPos = Target.position + Offset;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref velocity, SmoothSpeed, MaxSpeed, Time.deltaTime);
    }

    private void FixedUpdate() {
        //Vector3 targetPos = Target.position + Offset;
        //this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref velocity, SmoothSpeed, MaxSpeed, Time.deltaTime);
    }

}
