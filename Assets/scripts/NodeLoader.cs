using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLoader : MonoBehaviour
{
    public Root myWhip;
    void Awake() {
        string loadMyWhip = JsonFileReader.LoadJsonAsResource("data/main.json");
        myWhip = JsonUtility.FromJson<Root>(loadMyWhip);
        JsonFileReader.FillMap(jsonMap,myWhip);
    }

    public Dictionary<string, Dictionary<string, dynamic>> jsonMap = new Dictionary<string, Dictionary<string, dynamic>>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
