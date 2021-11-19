using Oculus.Voice;
using UnityEngine;

public class WitActivation : MonoBehaviour
{
    private AppVoiceExperience _voiceExperience;
    [SerializeField] private VoidEventChannelSO _voiceEventChannel;
    
    private void OnValidate()
    {
        if (!_voiceExperience)
        {
            _voiceExperience = GetComponent<AppVoiceExperience>();
        }
    }

    void Awake()
    {
        _voiceEventChannel.OnEventRaised += ActivateWit;
    }
    
    private void Start()
    {
        _voiceExperience = GetComponent<AppVoiceExperience>();
    }
    
    /// <summary>
    /// Activates Wit i.e. start listening to the user.
    /// </summary>
    private void ActivateWit()
    {
        if(!_voiceExperience.Active)
            _voiceExperience.Activate();
        
        Debug.Log($"Wit is activated: {_voiceExperience.ToString()}");
    }
}
