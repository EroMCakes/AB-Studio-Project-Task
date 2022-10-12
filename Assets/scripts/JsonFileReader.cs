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

    public static void FillMap(
        Dictionary<string, Dictionary<string, dynamic>> toFillDict,
        Root jsonObject
    )
    {
        foreach (Node node in jsonObject.main.nodes)
        {
            if (node.nodeType == "SNode")
            {
                List<dynamic> elementList = new List<dynamic>();
                foreach (NodeElement nodeElements in node.nodeElements)
                {
                    switch (nodeElements.elementType)
                    {
                        case "Sound":
                            {
                                var elementDictContent =
                                    new Dictionary<string, dynamic>()
                                    {
                                        {
                                            "objectName",
                                            nodeElements.sound.objectName
                                        },
                                        {
                                            "audioType",
                                            nodeElements.sound.audioType
                                        },
                                        {
                                            "audioUrl",
                                            nodeElements.sound.audioUrl
                                        },
                                        { "loop", nodeElements.sound.loop },
                                        { "volume", nodeElements.sound.volume },
                                        { "pitch", nodeElements.sound.pitch },
                                        {
                                            "spatialMode",
                                            nodeElements.sound.spatialMode
                                        },
                                        {
                                            "minDistance",
                                            nodeElements.sound.minDistance
                                        },
                                        {
                                            "maxDistance",
                                            nodeElements.sound.maxDistance
                                        },
                                        {
                                            "startTime",
                                            nodeElements.sound.startTime
                                        },
                                        {
                                            "endTime",
                                            nodeElements.sound.endTime
                                        }
                                    };
                                toFillDict
                                    .Add(nodeElements.sound.objectId,
                                    elementDictContent);
                                elementList
                                    .Add(toFillDict[nodeElements
                                        .sound
                                        .objectId]);
                                break;
                            }
                        case "Character":
                            {
                                var characterElementList = new List<dynamic>();
                                foreach (Element
                                    element
                                    in
                                    nodeElements.character.elements
                                )
                                {
                                    var characterElementDictContent =
                                        new Dictionary<string, dynamic>()
                                        {
                                            { "name", element.animation.name },
                                            {
                                                "destination",
                                                element.animation.destination
                                            },
                                            {
                                                "startTime",
                                                element.animation.startTime
                                            },
                                            {
                                                "endTime",
                                                element.animation.endTime
                                            },
                                            {
                                                "loopCount",
                                                element.animation.loopCount
                                            }
                                        };
                                    toFillDict
                                        .Add(element.animation.animationId,
                                        characterElementDictContent);
                                    
                                    var characterElementDict =
                                        new Dictionary<string, dynamic>()
                                        {
                                            {
                                                "elementType",
                                                element.elementType
                                            },
                                            {
                                                "elementInfo",
                                                toFillDict[element
                                                    .animation
                                                    .animationId]
                                            }
                                        };
                                    toFillDict
                                        .Add(element.id, characterElementDict);
                                    characterElementList
                                        .Add(toFillDict[element.id]);
                                }
                                var elementDictContent =
                                    new Dictionary<string, dynamic>()
                                    {
                                        { "name", nodeElements.character.name },
                                        {
                                            "startTime",
                                            nodeElements.character.startTime
                                        },
                                        {
                                            "endTime",
                                            nodeElements.character.endTime
                                        },
                                        {
                                            "model",
                                            nodeElements.character.model
                                        },
                                        {
                                            "origin",
                                            nodeElements.character.origin
                                        },
                                        { "elements", characterElementList }
                                    };
                                toFillDict
                                    .Add(nodeElements.character.id,
                                    elementDictContent);
                                elementList
                                    .Add(toFillDict[nodeElements.character.id]);
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
                        { "nodeElements", elementList }
                    };
                toFillDict.Add(node.nodeId.ToString(), contentDict);
            }
            else
            {
                var conditionList = new List<dynamic>();
                foreach (Condition condition in node.conditions)
                {
                    var conditionContent =
                        new Dictionary<string, dynamic>()
                        {
                            { "conditionType", condition.conditionType },
                            { "event", condition.@event }
                        };
                    conditionList.Add (conditionContent);
                }
                var contentDict =
                    new Dictionary<string, dynamic>()
                    {
                        { "name", node.name },
                        { "nodeType", node.nodeType },
                        { "conditions", conditionList }
                    };
            }
        }
    }

    // public void AddNode(dynamic node) {
    //     switch (node.GetType().ToString()) {
    //         case typeof(Node):
    //         {

    //             break;
    //         }
    //     }
    // }
}
