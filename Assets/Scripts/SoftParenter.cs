using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************
 * Source: https://discussions.unity.com/t/moving-objects-together-without-parenting/221704/6
 ********************/
public class SoftParenter : MonoBehaviour
{
    public GameObject ParentObject;

    private Vector3 posOffset;
    private Quaternion rotOffset;

    private void Start()
    {
        if (ParentObject != null)
        {
            SetFakeParent(ParentObject);
        }
    }

    private void Update()
    {
        if (ParentObject == null)
            return;

        Vector3 targetPos = ParentObject.transform.position - posOffset;
        Quaternion targetRot = ParentObject.transform.localRotation * rotOffset;

        transform.position = RotatePointAroundPivot(targetPos, ParentObject.transform.position, targetRot);
        transform.localRotation = targetRot;
    }

    public void SetFakeParent(GameObject parent)
    {
        //Offset vector
        posOffset = parent.transform.position - transform.position;
        //Offset rotation
        rotOffset = Quaternion.Inverse(parent.transform.localRotation * transform.localRotation);
        //Our fake parent
        ParentObject = parent;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        //Get a direction from the pivot to the point
        Vector3 dir = point - pivot;
        //Rotate vector around pivot
        dir = rotation * dir;
        //Calc the rotated vector
        point = dir + pivot;
        //Return calculated vector
        return point;
    }
}