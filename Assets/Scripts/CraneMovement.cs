using GameMath.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneMovement : MonoBehaviour
{
    [SerializeField] float cRotSpeed;

    public GameObject rButton;
    public GameObject lButton;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || rButton.GetComponent<HoldableButton>().IsHeldDown == true)
        {
            RotateRight();
        }

        if (Input.GetKey(KeyCode.LeftArrow) || lButton.GetComponent<HoldableButton>().IsHeldDown == true) 
        { 
            RotateLeft(); 
        }
    }

    public void RotateRight()
    {
        transform.Rotate(transform.position, cRotSpeed * Time.deltaTime);
    }
    public void RotateLeft()
    {
        transform.Rotate(transform.position, -cRotSpeed * Time.deltaTime);
    }
}
