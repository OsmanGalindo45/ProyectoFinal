using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class InstantImageTarget : MonoBehaviour
{
    [SerializeField] private GameObject[] aRObjects;
    [SerializeField] private XRReferenceImageLibrary runtimeImageLibrary;

    private ARTrackedImageManager imageManager;

    // Track the currently active model
    private GameObject currentModel;
    private bool isImageAdded = false;

    void Start()
    {
        imageManager = gameObject.AddComponent<ARTrackedImageManager>();
        imageManager.referenceLibrary = imageManager.CreateRuntimeLibrary(runtimeImageLibrary);
        imageManager.requestedMaxNumberOfMovingImages = 5;

        if (aRObjects.Length > 0)
        {
            currentModel = Instantiate(aRObjects[0]); // Instantiate the first model
            currentModel.SetActive(false); // Deactivate it initially
            imageManager.trackedImagePrefab = currentModel;
        }

        imageManager.enabled = true;
    }

    public void TakeScreenShot()
    {
        if (!isImageAdded)
        {
            StartCoroutine(CaptureScreen());
        }
    }

    private IEnumerator CaptureScreen()
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        AddImageTarget(texture);
    }

    private void AddImageTarget(Texture2D texture)
    {
        if (!isImageAdded && imageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            isImageAdded = true;

            XRReferenceImage referenceImage = new XRReferenceImage(SerializableGuid.empty, SerializableGuid.empty, new Vector3(0.1f, 0.1f, 0.1f), "My Image", texture);
            var jobState = mutableLibrary.ScheduleAddImageWithValidationJob(texture, "My Image", 0.1f);
            while (!jobState.jobHandle.IsCompleted)
            {
                Debug.Log("JOB IS RUNNING");
            }
            Debug.Log("JOB IS COMPLETE");

            StartCoroutine(ResetImageAdded());
        }
        else
        {
            Debug.Log("NO MUTABLE LIBRARY or Image Already Added");
        }
    }

    private IEnumerator ResetImageAdded()
    {
        // Reset isImageAdded after a short delay
        yield return new WaitForSeconds(1f);
        isImageAdded = false;
    }

}