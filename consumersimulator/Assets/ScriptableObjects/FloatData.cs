using System;
using UnityEngine;

[CreateAssetMenu( fileName = "Data" , menuName = "ScriptableObjects/FloatData" , order = 3 )]
public class FloatData : ScriptableObject, ISerializationCallbackReceiver
{
  
    private float dataTransfer;
    [NonSerialized]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = dataTransfer;
    }

    public void OnBeforeSerialize() {
        dataTransfer = RuntimeValue;
    }
}
