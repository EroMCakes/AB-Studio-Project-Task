using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Whip 
{
    public string mainName;
    public MainContent main;
}

[System.Serializable]
public class MainContent 
{
    public Node[] nodes;
}

[System.Serializable]
public class Node {
    public string name;
    public int nodeId;
    public string nodeType;
    public int? nextNodeId;
    public int? startTime;
    public int? endTime;
    public Coords?  nodeOrigin;
    public NodeElement?[] nodeElements;
    public Condition?[] conditions;
}

[System.Serializable]
public class Condition {
    public string conditionType;
    public Event conditionEvent;
}


[System.Serializable]     
public class Event {
    public int desiredEventValue;
    public string eventType;
}

[System.Serializable]
public class Coords {
    public int[] position;
    public int[] rotation;
}

[System.Serializable]  
public class NodeElement {
    public string elementType;
    public Sound? sound;
    public Character? character;
    public Animation? animation;
    public string? id;
}

[System.Serializable]    
public class Sound {
    public string objectName;
    public string objectId;
    public string audioType;
    public string audioURL;
    public bool loop;
    public int volume;
    public int pitch;
    public string spatialMode;
    public int minDistance;
    public int maxDistance;
    public int startTime;
    public int endTime;
}

[System.Serializable] 
public class Character {
    public string id;
    public string characterName;
    public int startTime;
    public int endTime;
    public string model;
    public Coords characterOrigin;
    public NodeElement[] elements;
}

[System.Serializable]
public class Animation {
    public string animationId;
    public Coords destination;
    public int endTime;
    public int loopCount;
    public string name;
    public int startTime;
}