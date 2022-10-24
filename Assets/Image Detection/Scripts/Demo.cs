using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ABXR_ImageDetectionAR.ParsingFunctions;

public class Demo : MonoBehaviour
{
    [SerializeField]
    private Text debug, appInfo;
    IEnumerator GetTexture(string imageUrl) {
        appInfo.text += "Downloading image from URL";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success) {
            appInfo.text += "Downloading failed";
            debug.text = www.error;
        } else {
            appInfo.text += "Dowloading success";
            Texture uploadedTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    private void Awake() {
        SetupI;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
