using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScript : MonoBehaviour
{
    [SerializeField] GameObject trolley;
    [SerializeField] float liftMax;
    [SerializeField] float liftMin;

    private Vector3 cRelatPos;
    private Vector3 cRelatAxisY;
    private Vector3 cRelatAxisZ;

    private void Awake()
    {
        cRelatPos = trolley.transform.InverseTransformVector(transform.position - trolley.transform.position);
        cRelatAxisY = trolley.transform.InverseTransformVector(transform.up);
        cRelatAxisZ = trolley.transform.InverseTransformVector(transform.forward);
    }

    private void Update()
    {
        transform.position = trolley.transform.position + trolley.transform.TransformVector(cRelatPos);
        //transform.position = trueRelativePos();
        transform.rotation = Quaternion.LookRotation(trolley.transform.TransformVector(cRelatAxisZ), trolley.transform.TransformVector(cRelatAxisY));
    }

    Vector3 trueRelativePos()
    {
        var posY = trolley.transform.position.y + trolley.transform.TransformVector(cRelatPos).y;
        var posZ = trolley.transform.position.z + trolley.transform.TransformVector(cRelatPos).z;

        return new Vector3(transform.position.x, posY, posZ);
    }

    void posCheck()
    {
        if (transform.position.y >= liftMax)
        {
            transform.position = new Vector3(transform.position.x, liftMax, transform.position.z);
        }
        if (transform.position.y <= liftMin)
        {
            transform.position = new Vector3(transform.position.x, liftMin, transform.position.z);
        }
    }
}
