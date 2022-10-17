using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackableImageJsonReader : MonoBehaviour
{
    public static void FillTrackableMap(
        Dictionary<int, Dictionary<string, dynamic>> toTrackImages,
        Dictionary<string, Dictionary<string, dynamic>> toInstantiatePrefabs,
        TrackableImageRoot parsedTrackableImageRoot
    )
    {
        foreach (TrackableImage
            trackableImage
            in
            parsedTrackableImageRoot.trackableImages
        )
        {
            if (toTrackImages[trackableImage.id] == null)
            {   List<dynamic> prefabList = new List<dynamic>();
                foreach (RenderedPrefab
                    renderedPrefab
                    in
                    trackableImage.renderedPrefabs
                )
                {
                    if (toInstantiatePrefabs[renderedPrefab.id] == null)
                    {
                        var prefabDetails =
                            new Dictionary<string, dynamic>()
                            {
                                { "name", renderedPrefab.name },
                                {"id", renderedPrefab.id},
                                {
                                    "scaleFactor",
                                    renderedPrefab.scaleFactor
                                },
                                { "origin", renderedPrefab.origin }
                            };
                        toInstantiatePrefabs.Add(renderedPrefab.id, prefabDetails);
                        prefabList.Add(toInstantiatePrefabs[renderedPrefab.id]);
                    }
                }
                var trackableImageDetails = new Dictionary<string, dynamic>(){
                    {
                        "name", trackableImage.name
                    },
                    {
                        "id", trackableImage.id
                    },
                    {
                        "size", trackableImage.size
                    },
                    {
                        "referenceImageName", trackableImage.referenceImageName
                    },
                    {
                        "renderedPrefabs", prefabList
                    }
                };
            }
        }
    }
}
