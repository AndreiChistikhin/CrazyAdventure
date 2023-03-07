using System;
using GamePlay.Hero;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
    public event Action TriggerEnter;
    public event Action TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HeroHealth hero))
            TriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out HeroHealth hero))
            TriggerExit?.Invoke();
    }
}