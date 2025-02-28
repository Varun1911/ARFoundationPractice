using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private Vector2 touchPos;

    private static List<ARRaycastHit> raycastHits = new();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private bool TryGetTouchPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    private void Update()
    {
        if(!TryGetTouchPos(out touchPos))
        {
            return;
        }

        if(raycastManager.Raycast(touchPos, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = raycastHits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
            }

            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }

}
