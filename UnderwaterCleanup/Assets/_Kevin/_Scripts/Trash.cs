using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Trash : MonoBehaviour
{
    // [SerializeField] private SphereCollider _collider;
    [FormerlySerializedAs("OnVoiceRequestedChannel")] [SerializeField] private VoidEventChannelSO _onVoiceRequestedChannel;
    [SerializeField] private Vector3EventChannelSO _trashAttackChannel;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Key is pressed");
            RequestVoice();
            RequestClean();
        }
    }

    private void RequestClean()
    {
        _trashAttackChannel.RaiseEvent(transform.position);
    }

    private void RequestVoice()
    {
        _onVoiceRequestedChannel.RaiseEvent();
    }
    
}
