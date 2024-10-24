using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitParenting : MonoBehaviour
{
    public GameObject parent;

    private Vector3 relatPos;
    private Quaternion relatRot;

    private void Awake()
    {
        relatPos = parent.transform.InverseTransformPoint(transform.position);
        relatRot = Quaternion.Inverse(parent.transform.rotation) * transform.rotation;
    }

    private void Update()
    {
        transform.position = parent.transform.TransformPoint(relatPos);
        transform.rotation = parent.transform.rotation * relatRot;
    }
}
