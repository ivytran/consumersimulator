using System;
using UnityEngine;

[CreateAssetMenu( fileName = "Data" , menuName = "ScriptableObjects/StringData" , order = 1 )]
public class StringData : ScriptableObject, ISerializationCallbackReceiver
{
    private string dataTransfer;

    [NonSerialized]
    public string RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = dataTransfer;
    }

    public void OnBeforeSerialize() {
        dataTransfer = RuntimeValue;
    }
}
