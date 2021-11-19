using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Random = UnityEngine.Random;

public class FishSchoolManager : MonoBehaviour
{
    public static List<Fish> allFish = new List<Fish>();

    [SerializeField] private Vector3EventChannelSO _trashAttackChannel;
    [SerializeField] private GameObject fish;
    [SerializeField] private int spawnRadius;
    [SerializeField] private int numFishToSpawn = 0;
    [SerializeField] private Transform[] _path;
    [SerializeField] private float lookAtRange;
    [SerializeField] private float duration;

    private Vector3 startPosition;
    private Sequence idleSequence;
    
    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, spawnRadius);
    //     foreach (var fish in allFish)
    //     {
    //         Gizmos.DrawLine( transform.position, fish.transform.position);
    //     }
    //     
    //     for (int i = 0; i < _path.Length; i++)
    //     {
    //         Handles.DrawLine(_path[i].position, _path[i + 1].position);
    //     }
    // }

    void Awake()
    {
        _trashAttackChannel.OnEventRaised += FishPower;
    }
    
    void Start()
    {
        startPosition = transform.position;
        SpawnFish(numFishToSpawn);
        idleSequence = DOTween.Sequence();
        idleSequence.Append(IdleSwimAsync()).SetLoops(-1, LoopType.Restart);
    }
    
    private void SpawnFish(int numFish)
    {
        for (int i = 0; i < numFish; i++)
        {
            var randomPos = (Vector3) Random.insideUnitSphere * spawnRadius;
            randomPos += transform.position;
            var newFish = Instantiate(fish, randomPos, transform.rotation);
            newFish.transform.parent = transform;
        }
    }

    private Vector3[] GetFishPath()
    {
        Vector3[] points = new Vector3[_path.Length];
        
        for (int i = 0; i < _path.Length; i++)
        {
            points[i] = _path[i].position;
        }

        return points;
    }
    
    //TODO: Fish are facing directly in negative of the X axis on spawn, need to change to face the Z axis
    private Tween IdleSwimAsync()
    {
        return transform.DOPath(GetFishPath(), duration, PathType.Linear, PathMode.Full3D, default, Color.cyan)
            .SetLookAt(lookAtRange)
            .SetEase(Ease.Linear);
    }

    private void FishPower(Vector3 target)
    {
        idleSequence.Pause();
        
        transform.DOPath(new[] {transform.position, target}, duration, PathType.Linear, PathMode.Full3D, default, Color.cyan)
            .SetLookAt(lookAtRange)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                //TODO: Implement fish power logic
                Debug.Log($"Fish power executed. target Vector3: {target}");
            }).OnComplete(() =>
            {
                transform.DOPath(new [] {target, startPosition}, duration, PathType.Linear, PathMode.Full3D, default, Color.cyan)
                    .SetLookAt(lookAtRange)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => idleSequence.Restart());
            });
    }
}
