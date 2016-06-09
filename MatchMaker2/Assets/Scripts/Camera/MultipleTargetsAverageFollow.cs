using UnityEngine;
using System.Collections;

public class MultipleTargetsAverageFollow : MonoBehaviour
{
    //the z distance the camera should be away from its target(s).
    [SerializeField]
    private float zDistance = -10;
    [SerializeField]
    private float lerpSpeed = 5f;

    [SerializeField]
    private float minZoomSize = 4;
    [SerializeField]
    private float maxZoomSize = 8;
    //the targets themselves.
    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private Camera mainCamera;
    private float tempZoomSize = 5;
    private float zoomSize = 5;
    //tempPostiion is the vector3 variable that will be calculated.
    private Vector3 tempPosition;

    [SerializeField]
    private Transform bgImage;
    private float ratioX, ratioY;
    //is used to calculate the distance of multpile targets positions.
    private Vector3 distance;

    void Awake()
    {
        tempZoomSize =zoomSize = mainCamera.GetComponent<Camera>().orthographicSize;
        ratioX = bgImage.localScale.x / zoomSize;
        ratioY = bgImage.localScale.y / zoomSize;

    }
    void Update()
    {
        //sets the position to the first target.
        if(targets[0].GetComponent<Movement>().IsAlive == true)
            tempPosition = targets[0].position;

        //if there are more than one target to follow. 
        //It calculates the avarage distances between the targets by calculating the distances 
        //one by one and aply the half of the distance to the tempPosition 
        //and so it goes on until it has every objects transform.
        if (targets.Length > 1)
        {
            for (int i = 1; i < targets.Length; i++)
            {
                if(targets[i].GetComponent<Movement>().IsAlive == true)
                {
                    distance = tempPosition - targets[i].position;
                    tempPosition -= distance / 2;
                }
            }
        }
        //checks if it must zoom out or in to see the targets clearly.
        CheckIfToZoom();
        //declares the z distance from the target.
        tempPosition.z = zDistance;
        //aplies the tempPosition to the position of the object that must have the average position.

        //transform.position = tempPosition;
        transform.position = Vector3.Lerp(transform.position, tempPosition, lerpSpeed * Time.deltaTime);

        }
    
    void CheckIfToZoom()
    {
        zoomSize = mainCamera.GetComponent<Camera>().orthographicSize;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<Movement>().IsAlive)
            {
                Vector3 screenPoint = Camera.main.WorldToViewportPoint(targets[i].position);
                if (screenPoint.x > 0.3 && screenPoint.x < 0.7 && screenPoint.y > 0.3 && screenPoint.y < 0.7 && tempZoomSize > 4)
                {
                    tempZoomSize = zoomSize - 1f;
                }
                else if ((screenPoint.x < 0.1 || screenPoint.x > 0.9 || screenPoint.y < 0.1 || screenPoint.y > 0.9) && tempZoomSize < 8)
                {
                    tempZoomSize = zoomSize + 1f * targets.Length;
                }
            }
        }
        zoomSize = Mathf.Lerp(zoomSize, tempZoomSize, Time.deltaTime);
        zoomSize = Mathf.Clamp(zoomSize, minZoomSize, maxZoomSize);
        bgImage.localScale = new Vector2(zoomSize * ratioX, zoomSize * ratioY );
        mainCamera.GetComponent<Camera>().orthographicSize = zoomSize;
    }


}

