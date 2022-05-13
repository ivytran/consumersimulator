using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Events" , menuName = "ScriptableObjects/GameEvents" , order = 1 )]
public class GameEvents : ScriptableObject
{
	private List<GameEventListener> listeners = new List<GameEventListener>();
	private bool isEventCalled = false;
	[NonSerialized]
	public List<GameEventListener> runtimeListenrs = new List<GameEventListener>();
	[NonSerialized]
	public bool isRuntimeEventCalled = false;
	public void OnAfterDeserialize()
	{
		runtimeListenrs = listeners;
		isRuntimeEventCalled = isEventCalled;
	}

	public void OnBeforeSerialize() { }
	public void Raise()
	{
        for (int i = runtimeListenrs.Count - 1; i >= 0; i--)
        {
			Debug.Log( "listeners " + runtimeListenrs[i] );
			if (runtimeListenrs[i]  != null)
			{
				runtimeListenrs[i].OnEventRaised();
			}
        }
	}

	public void RegisterListener(GameEventListener listener)
	{ runtimeListenrs.Add(listener ); }

	public void UnregisterListener(GameEventListener listener)
	{ runtimeListenrs.Remove( listener ); }


}
