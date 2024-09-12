using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneControls : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    private float rotationInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        RotateCrane();
    }

    void RotateCrane()
    {
        float craneRotation = rotationInput * rotationSpeed;
        transform.Rotate(Vector3.up, craneRotation * Time.deltaTime);
    }
}
