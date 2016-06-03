using UnityEngine;
using System.Collections;

public class MultipleTargetsAverageFollow : MonoBehaviour
{
    //the z distance the camera should be away from its target(s).
    [SerializeField]
    private float zDistance = -10;
    [SerializeField]
    private float lerpSpeed = 5f;
    //the targets themselves.
    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private bool hasBounds = true;
    [SerializeField]
    private float xMax, xMin, yMax, yMin;

    [SerializeField]
    private Camera mainCamera;
    private float zoomSize = 5;
    //tempPostiion is the vector3 variable that will be calculated.
    private Vector3 tempPosition;

    //is used to calculate the distance of multpile targets positions.
    private Vector3 distance;

    void Awake()
    {
        zoomSize = mainCamera.GetComponent<Camera>().orthographicSize;
    }
    void Update()
    {
        //sets the position to the first target.
        if(targets[0] != null)
            tempPosition = targets[0].position;

        //if there are more than one target to follow. 
        //It calculates the avarage distances between the targets by calculating the distances 
        //one by one and aply the half of the distance to the tempPosition 
        //and so it goes on until it has every objects transform.
        if (targets.Length > 1)
        {
            for (int i = 1; i < targets.Length; i++)
            {
                if(targets[i] != null)
                {
                    distance = tempPosition - targets[i].position;
                    tempPosition -= distance / 2;
                }
            }
        }

        //checks if the object is out of boundaries, if so it blocks the object to got there.
        CheckBoundaries();

        //declares the z distance from the target.
        tempPosition.z = zDistance;
        //aplies the tempPosition to the position of the object that must have the average position.

        //transform.position = tempPosition;
        transform.position = Vector3.Lerp(transform.position, tempPosition, lerpSpeed * Time.deltaTime);

        mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(mainCamera.GetComponent<Camera>().orthographicSize, zoomSize, Time.deltaTime * 10f);
    }
    void CheckBoundaries()
    {
        tempPosition.x = Mathf.Clamp(tempPosition.x, xMin, xMax);
        tempPosition.y =Mathf.Clamp(tempPosition.y, yMin, yMax);
    }
    /*
    void CheckIfToZoom()
    {
        int z = 0;
        for (int i = 1; i < targets.Length; i++)
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(targets[i].position);
            if (screenPoint.z > 0 && screenPoint.x > 0.3 && screenPoint.x < 0.7 && screenPoint.y > 0.3 && screenPoint.y < 0.7 && zoomSize > 2)
            {
                zoomSize -= 0.1f;
            }
            else if ((screenPoint.x < 0.1 || screenPoint.x > 0.9 || screenPoint.y < 0.1 || screenPoint.y > 0.9) && zoomSize < 10)
            {
                zoomSize += 0.1f;
            }
        }

    }
    */

}

