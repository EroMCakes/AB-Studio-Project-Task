using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLoader : MonoBehaviour
{
    public Whip myWhip;
    void Awake() {
        string loadMyWhip = JsonFileReader.LoadJsonAsResource("data/main.json");
        myWhip = JsonUtility.FromJson<Whip>(loadMyWhip);
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
