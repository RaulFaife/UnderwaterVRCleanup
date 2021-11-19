using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for events that have Vector3 arguments.
/// </summary>

[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vector3EventChannelSO : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 v3)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(v3);
        }
    }
}
