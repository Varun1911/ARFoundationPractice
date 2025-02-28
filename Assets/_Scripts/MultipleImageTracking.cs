using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleImageTracking : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private RuntimeReferenceImageLibrary refImageLib;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> arObjects = new();

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
    {
        trackedImageManager.trackedImagesChanged += TrackedImageManager_trackedImagesChanged;

        foreach(var prefab in prefabsToSpawn)
        {
            var newGO = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newGO.name = prefab.name;
            newGO.SetActive(false);
            arObjects.Add(newGO.name, newGO);
        }
    }


    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= TrackedImageManager_trackedImagesChanged;
    }


    private void TrackedImageManager_trackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }


        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.referenceImage.name].SetActive(false);
        }

    }


    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
        {
            arObjects[trackedImage.referenceImage.name].SetActive(false);
            return;
        }

        if (prefabsToSpawn != null) 
        {
            arObjects[trackedImage.referenceImage.name].SetActive(true);
            arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        }
    }
}
