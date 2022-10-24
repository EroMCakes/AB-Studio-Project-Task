using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageTrackingSceneSetup {
    public string name;
    public List<TrackedImage> trackableImages;
}

[System.Serializable]
public class TrackedImage {
    public string name;
    public int id;
    public float size;
    public string referenceImageName;
    public RenderedPrefab[] renderedPrefabs;
}

[System.Serializable]
public class RenderedPrefab {
    public string name;
    public string id;
    public List<float> scaleFactor;
    public Origin origin;
}

[System.Serializable]
public class Origin {
    public List<int> position;
    public List<int> rotaion;
}