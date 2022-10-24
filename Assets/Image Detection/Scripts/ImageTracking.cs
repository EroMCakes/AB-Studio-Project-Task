using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof (ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;

    public ImageTrackingSceneSetup imageTrackingSceneSetup;

    [SerializeField]
    private XRReferenceImageLibrary m_Library;

    // private XRImageTrackingSubsystem m_subsystem; 

    [SerializeField]
    private Text debug;


    [SerializeField]
    private Dictionary<string, GameObject>
        spawnedPrefabs = new Dictionary<string, GameObject>();

    private ARTrackedImageManager m_TrackedImageManager;

    public string imageNameToAdd;

    private void Awake()
    {
        m_TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        m_TrackedImageManager.referenceLibrary = m_TrackedImageManager.CreateRuntimeLibrary(m_Library);
        m_TrackedImageManager.requestedMaxNumberOfMovingImages = 4;
        m_TrackedImageManager.enabled = true;

        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        // foreach (GameObject prefab in placeablePrefabs)
        // {
        //     GameObject newInstantiatedPrefab =
        //         Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //     newInstantiatedPrefab.name = prefab.name;
        //     spawnedPrefabs.Add(prefab.name, newInstantiatedPrefab);
        //     spawnedPrefabs[prefab.name].SetActive(false);
        // }
    }

    private void Update()
    {
    }

    private void OnEnable() =>
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

    private void OnDisable() =>
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            UpdateImage (newImage);
        }
        foreach (var updatedImage in eventArgs.updated)
        {
            UpdateImage (updatedImage);
        }
        foreach (var removedImage in eventArgs.removed)
        {
            spawnedPrefabs[removedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        AssignGameObject(trackedImage.referenceImage.name,
        trackedImage.transform.position);
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (spawnedPrefabs != null)
        {
            GameObject goARObject = spawnedPrefabs[name];
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;
            foreach (GameObject go in spawnedPrefabs.Values)
            {
                if (go.name != name)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public IEnumerator AddImage(Texture2D texture2D) {
        yield return null;

        var firtGuid = new SerializableGuid(0,0);
        var secondGuid = new SerializableGuid(0,0);

        XRReferenceImage newImage = new XRReferenceImage(firtGuid, secondGuid, new Vector2(0.1f, 0.1f), Guid.NewGuid().ToString(), texture2D);

        try {
            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = m_TrackedImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageWithValidationJob(texture2D, Guid.NewGuid().ToString(), 0.1f);

            while(!jobHandle.status.IsComplete()) {
                debug.text = "Job Running...";
            }

            debug.text = "Job Completed...";
        } catch (Exception e) {
            debug.text = e.ToString();
        }
    }

}
