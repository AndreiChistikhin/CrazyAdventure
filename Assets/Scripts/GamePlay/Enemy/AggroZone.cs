using System.Collections;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private Follow _follow;
    [SerializeField] private float _coolDown;
        
    private Coroutine _aggroCoroutine;
    private bool _hasAggroTarget;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
            
        TurnFollowOff();
    }

    private void TriggerEnter()
    {
        if (_hasAggroTarget)
            return;
            
        if (_aggroCoroutine != null)
        {
            _hasAggroTarget = true;
            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }
            
        TurnFollowOn();
    }

    private void TriggerExit()
    {
        _hasAggroTarget = false;
        _aggroCoroutine = StartCoroutine(SwitchFollowAfterCooldown());
    }

    private IEnumerator SwitchFollowAfterCooldown()
    {
        yield return new WaitForSeconds(_coolDown);
            
        TurnFollowOff();
    }

    private void TurnFollowOn()
    {
        _follow.enabled = true;
    }

    private void TurnFollowOff()
    {
        _follow.enabled = false;
    }
}