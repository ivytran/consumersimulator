using System;
using UnityEngine;

[CreateAssetMenu( fileName = "Data" , menuName = "ScriptableObjects/IntData" , order = 2 )]
public class IntData : ScriptableObject, ISerializationCallbackReceiver
{
    private int dataTransfer;

    [NonSerialized]
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = dataTransfer;
    }

    public void OnBeforeSerialize() {
        dataTransfer = RuntimeValue;
    }
}
