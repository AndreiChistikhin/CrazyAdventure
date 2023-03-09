using UnityEngine;

public class CheckAttackRange : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

        _attack.DisableAttack();
    }

    private void TriggerEnter()
    {
        _attack.EnableAttack();
    }

    private void TriggerExit()
    {
        _attack.DisableAttack();
    }
}