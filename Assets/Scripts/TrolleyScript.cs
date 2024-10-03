using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyScript : MonoBehaviour
{
    [SerializeField] GameObject crane;
    [SerializeField] GameObject nearLimit;
    [SerializeField] GameObject farLimit;

    private Vector3 tRelatPos;
    private Vector3 tRelatAxisY;
    private Vector3 tRelatAxisZ;

    private void Awake()
    {
        tRelatPos = crane.transform.InverseTransformVector(transform.position - crane.transform.position);
        tRelatAxisY = crane.transform.InverseTransformVector(transform.up);
        tRelatAxisZ = crane.transform.InverseTransformVector(transform.forward);
    }

    private void Update()
    {
        transform.position = crane.transform.position + crane.transform.TransformVector(tRelatPos);
        //transform.position = trueRelativePos();
        transform.rotation = Quaternion.LookRotation(crane.transform.TransformVector(tRelatAxisZ), crane.transform.TransformVector(tRelatAxisY));
    }

    Vector3 trueRelativePos()
    {
        var posY = crane.transform.position.y + crane.transform.TransformVector(tRelatPos).y;
        var posZ = crane.transform.position.z + crane.transform.TransformVector(tRelatPos).z;

        return new Vector3(transform.position.x, posY, posZ);
    }

    void posCheck()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, farLimit.transform.position)) <= 0)
        {
            transform.position = new Vector3(farLimit.transform.position.x, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(Vector3.Distance(transform.position, nearLimit.transform.position)) <= 0)
        {
            transform.position = new Vector3(nearLimit.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
