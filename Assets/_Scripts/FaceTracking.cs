using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFaceManager))]
public class FaceTracking : MonoBehaviour
{
    [SerializeField] private Button toggleTrackingBtn;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject nose;
    [SerializeField] private GameObject mouth;

    private ARFaceManager arFaceManager;
    private ARFace face;


    private void Awake()
    {
        arFaceManager = GetComponent<ARFaceManager>();
    }


    private void OnEnable()
    {
        toggleTrackingBtn.onClick.AddListener(ToggleFaceTracking);

        leftEye.SetActive(false);
        rightEye.SetActive(false);
        nose.SetActive(false);
        mouth.SetActive(false);
    }


    private void OnDisable()
    {
        toggleTrackingBtn.onClick.RemoveListener(ToggleFaceTracking);
        arFaceManager.facesChanged -= ArFaceManager_facesChanged;
    }

    void Start()
    {
        arFaceManager.facesChanged += ArFaceManager_facesChanged;
    }

    private void ArFaceManager_facesChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach(ARFace face in eventArgs.added)
        {
            if(face != null)
            {
                leftEye.SetActive(true);
                rightEye.SetActive(true);
                nose.SetActive(true);
                mouth.SetActive(true);

                leftEye.transform.position = face.leftEye.position;
                rightEye.transform.position = face.rightEye.position;
                mouth.transform.position = face.fixationPoint.position;
                nose.transform.position = face.vertices[face.vertices.Length / 2];
                break;
            }
            
        }

        foreach(ARFace face in eventArgs.updated)
        {
            if(face != null)
            {
                if(face.trackingState is TrackingState.Limited or TrackingState.None)
                {
                    leftEye.SetActive(false);
                    rightEye.SetActive(false);
                    nose.SetActive(false);
                    mouth.SetActive(false);
                    break;
                }

                else
                {
                    leftEye.transform.position = face.leftEye.position;
                    rightEye.transform.position = face.rightEye.position;
                    mouth.transform.position = face.fixationPoint.position;
                    nose.transform.position = face.vertices[face.vertices.Length / 2];
                    break;
                }
            }
        }

        foreach(ARFace face in eventArgs.removed)
        {
            leftEye.SetActive(false);
            rightEye.SetActive(false);
            nose.SetActive(false);
            mouth.SetActive(false);
            break;
        }
    }

    private void ToggleFaceTracking()
    {
        foreach(var face in arFaceManager.trackables)
        {
            face.enabled = !face.enabled;
        }
    }
}
