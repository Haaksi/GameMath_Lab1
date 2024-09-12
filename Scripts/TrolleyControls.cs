using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyControls : MonoBehaviour
{
    [SerializeField] Transform parent;

    private Vector3 relativePos;
    private Vector3 relativeAxisY;
    private Vector3 relativeAxisZ;

    private void Awake()
    {
        relativePos = parent.transform.InverseTransformVector(transform.position - parent.position);
        relativeAxisY = parent.transform.InverseTransformVector(transform.up);
        relativeAxisZ = parent.transform.InverseTransformVector(transform.forward);


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position + parent.TransformVector(relativePos);
        transform.rotation = Quaternion.LookRotation(parent.TransformVector(relativeAxisZ), 
                                                     parent.TransformVector(relativeAxisY));
    }
}
