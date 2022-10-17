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

    public TrackableImageRoot trackableImageRoot;

    [SerializeField]
    private RuntimeReferenceImageLibrary m_Library = new RuntimeReferenceImageLibrary;
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
        // m_Library = m_TrackedImageManager.CreateRuntimeLibrary();
        // m_subsystem = m_TrackedImageManager.subsystem;
        // try
        // {AddImageToReferenceLibrary(imageNameToAdd, 0.5f);} catch (Exception e) {
        //     debug.text = e.ToString();
        // }
        // try{
        // InstantiateRelatablePrefab(imageNameToAdd, Vector3.zero); } catch (Exception e) {
        //     debug.text += e.ToString();
        // }


        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newInstantiatedPrefab =
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newInstantiatedPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newInstantiatedPrefab);
            spawnedPrefabs[prefab.name].SetActive(false);
        }
    }

    // private void AddImageToReferenceLibrary(string imageName, float imageSize) {
    //     if(m_Library is MutableRuntimeReferenceImageLibrary mutableLibrary) {
    //         mutableLibrary.ScheduleAddImageWithValidationJob(Resources.Load<Texture2D>($"Image Targets/{imageName}.png"), imageName, imageSize);
    //     }
    // }

    // private void InstantiateRelatablePrefab(string trackedImageName, Vector3 position) {
    //     GameObject instantiatedPrefab = Instantiate(Resources.Load<GameObject>($"prefab/{trackedImageName}"), position, Quaternion.identity);
    //     spawnedPrefabs.Add(trackedImageName, instantiatedPrefab);
    // }

    private void Update()
    {
    }

    private void OnEnable() =>
        m_TrackedImageManager.trackedImagesChanged += ImageChanged;

    private void OnDisable() =>
        m_TrackedImageManager.trackedImagesChanged -= ImageChanged;

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
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
}
