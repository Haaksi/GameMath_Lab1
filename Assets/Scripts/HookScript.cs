using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    [SerializeField] GameObject cable;

    private Vector3 hRelatPos;
    private Vector3 hRelatAxisY;
    private Vector3 hRelatAxisZ;

    private void Awake()
    {
        hRelatPos = cable.transform.InverseTransformVector(transform.position - cable.transform.position);
        hRelatAxisY = cable.transform.InverseTransformVector(transform.up);
        hRelatAxisZ = cable.transform.InverseTransformVector(transform.forward);
    }

    private void Update()
    {
        transform.position = cable.transform.position + cable.transform.TransformVector(hRelatPos);
        //transform.position = trueRelativePos();
        transform.rotation = Quaternion.LookRotation(cable.transform.TransformVector(hRelatAxisZ), cable.transform.TransformVector(hRelatAxisY));
    }

    Vector3 trueRelativePos()
    {
        var posY = cable.transform.position.y + cable.transform.TransformVector(hRelatPos).y;
        var posZ = cable.transform.position.z + cable.transform.TransformVector(hRelatPos).z;

        return new Vector3(transform.position.x, posY, posZ);
    }
}