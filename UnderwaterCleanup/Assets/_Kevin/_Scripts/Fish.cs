using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable() => FishSchoolManager.allFish.Add(this);
    private void OnDisable() => FishSchoolManager.allFish.Remove(this);

    private void Awake()
    {
        transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
    }
}
