using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFileReader
{
    public static string LoadJsonAsResource(string path)
    {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonFile.text;
    }

    public static void MapFiller(
        Dictionary<string, Dictionary<string, dynamic>> toFillDict,
        Root jsonObject
    )
    {
        foreach (Node node in jsonObject.main.nodes)
        {
            if (node.nodeType == "SNode")
            {   
                List<string> elementList = new List<string>();
                foreach(NodeElement nodeElements in node.nodeElements) {
                    switch (nodeElements.elementType) {
                        case "Sound":
                        {
                            elementList.Add(nodeElements.sound.objectId);
                            break;
                        }
                        case "Character":
                        {
                            elementList.Add(nodeElements.character.id);
                            break;
                        }
                    }
                }
                var contentDict =
                    new Dictionary<string, dynamic>()
                    {
                        { "name", node.name },
                        { "nodeType", node.nodeType },
                        { "nextNodeId", node.nextNodeID },
                        { "startTime", node.startTime },
                        { "endTime", node.endTime },
                        { "origin", node.origin },
                        {"nodeElements", elementList},
                    };
                foreach(NodeElement nodeElem in node.nodeElements){
                    
                }
                toFillDict.Add(node.nodeId.ToString(), contentDict);
            }
        }
    }

    // public static void MapFiller()
}
