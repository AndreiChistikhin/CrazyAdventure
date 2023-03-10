using System.Linq;
using GamePlay.Enemy;
using GamePlay.HUD;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private EnemyAnimation Animator;
    [SerializeField] private float AttackCoolDown = 3f;
    [SerializeField] private float Cleavage = 0.5f;
    [SerializeField] private float EffectiveDistance = 0.5f;
    [SerializeField] private float Damage;

    private Transform _heroTransform;
    private float _currentAttackCoolDown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private bool _attackIsActive;

    public void Construct(Transform heroTransform)
    {
        _heroTransform = heroTransform;
    }

    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        UpdateCoolDown();

        if (!_isAttacking && CoolDownIsUp() && _attackIsActive)
        {
            Debug.Log("StartAttack");
            StartAttack();
        }
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
        {
            hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
        }
    }

    private void OnAttackEnded()
    {
        _currentAttackCoolDown = AttackCoolDown;
        _isAttacking = false;
    }

    public void EnableAttack()
    {
        _attackIsActive = true;
    }

    public void DisableAttack()
    {
        _attackIsActive = false;
    }

    private bool Hit(out Collider hit)
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

        hit = _hits.FirstOrDefault();

        return hitsCount > 0;
    }

    private Vector3 StartPoint()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
               transform.forward * EffectiveDistance;
    }

    private void UpdateCoolDown()
    {
        if (_currentAttackCoolDown > 0)
            _currentAttackCoolDown -= Time.deltaTime;
    }

    private bool CoolDownIsUp()
    {
        return _currentAttackCoolDown <= 0;
    }

    private void StartAttack()
    {
        transform.LookAt(_heroTransform);
        Animator.PlayAttack();

        _isAttacking = true;
    }
}