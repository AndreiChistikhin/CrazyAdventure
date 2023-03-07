using GamePlay.Hero;
using Services.Interfaces;
using UnityEngine;
using Zenject;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] private HeroAnimation _heroAnimation;
    
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Update()
    {
        if (_inputService == null)
            return;

        if (_inputService.IsAttacking)
            DoAttack();
    }

    private void DoAttack()
    {
        _heroAnimation.PlayAttack();
    }
}
