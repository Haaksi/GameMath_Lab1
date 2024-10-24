using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrolleyScript : MonoBehaviour
{
    [SerializeField] GameObject crane;      // Reference to crane
    [SerializeField] GameObject nearLimit;  // Reference to near limit
    [SerializeField] GameObject farLimit;   // Reference to far limit
    [SerializeField] float tMoveSpeed;      // Move speed of trolley

    [SerializeField] Slider trolleySlider;  // Reference to Trolley Slider

    public Vector3 tRelatPos;               // Relative position of the trolley to the crane
    public Vector3 tRelatRot;               // Relative rotation of the trolley to the crane
    private Vector3 tMovement;               // Current movement of the trolley

    private float minLimit;                 // Minimum limit from the crane
    private float maxLimit;                 // Maximum limit from the crane

    private void Awake()
    {
        if (crane != null)
        {
            tRelatPos = transform.position - crane.transform.position;
            tRelatRot = transform.eulerAngles - crane.transform.eulerAngles;
            tMovement = Vector3.zero;

            SetLimits();
        }

        if (trolleySlider != null)
        {
            trolleySlider.value = GetCurrentSliderValue();
        }
    }

    private void Update()
    {
        TrolleyMovement();
    }

    private void LateUpdate()
    {
        if (crane != null)
        {
            // Calculate new position by applying the offset in world space
            Vector3 newPosition = crane.transform.position + crane.transform.TransformDirection(tRelatPos + tMovement);
            newPosition.y = transform.position.y; // Keep the Y position the same

            // Calculate current distance from crane to new position
            float currentDistance = Vector3.Distance(crane.transform.position, newPosition);

            // Check if the new position exceeds the limits
            if (currentDistance < minLimit || currentDistance > maxLimit)
            {
                // Calculate how much movement to apply to keep it within limits
                float distanceToLimit = currentDistance < minLimit ? minLimit - currentDistance : currentDistance - maxLimit;

                // Adjust tMovement based on the distance to limit
                tMovement += new Vector3(-distanceToLimit * Mathf.Sign(tMovement.x), 0, 0);
            }

            // Update the position of the trolley
            transform.position = newPosition;

            // Apply rotation offset to maintain relative rotation
            Quaternion craneRotation = Quaternion.Euler(crane.transform.eulerAngles);
            Quaternion trolleyRotation = Quaternion.Euler(tRelatRot);
            transform.rotation = craneRotation * trolleyRotation;
        }
    }

    void TrolleyMovement()
    {
        float tMoveX = Input.GetAxis("Vertical") * tMoveSpeed * Time.deltaTime;
        float sliderValue = trolleySlider.value;

        // Calculate the proposed new position with movement
        Vector3 proposedNewPosition = transform.position + crane.transform.TransformDirection(new Vector3(tMoveX, 0, 0));
        float currentDistance = Vector3.Distance(crane.transform.position, proposedNewPosition);

        // Only allow movement if within limits
        if (currentDistance >= minLimit && currentDistance <= maxLimit)
        {
            tMovement += new Vector3(tMoveX, 0, 0); // Update movement if within limits
        }
    }

    float GetCurrentSliderValue()
    {
        // Get the current distance and map it to the slider value
        float currentDistance = Vector3.Distance(crane.transform.position, transform.position);
        return Mathf.InverseLerp(minLimit, maxLimit, currentDistance);
    }

    void SetLimits()
    {
        if (nearLimit != null && farLimit != null)
        {
            minLimit = Vector3.Distance(crane.transform.position, nearLimit.transform.position);
            maxLimit = Vector3.Distance(crane.transform.position, farLimit.transform.position);
        }
    }
}
