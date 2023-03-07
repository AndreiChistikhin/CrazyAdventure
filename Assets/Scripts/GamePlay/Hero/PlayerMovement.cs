using Infrasctructure.Extensions;
using Services;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace GamePlay.Hero
{
    public class PlayerMovement : MonoBehaviour, IProgressHandler
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

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

            Vector3 movementDirection = new Vector3();

            if (_inputService.Axis.magnitude > Constants.Epsilon)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("Нет мейн кармеры");
                    return;
                }
            
                movementDirection = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementDirection.y = 0;
                movementDirection.Normalize();
                transform.forward = movementDirection;
            }
            
            movementDirection += Physics.gravity;
            _characterController.Move(movementDirection * Time.deltaTime * _movementSpeed);
        }

        public void LoadProgress(GameProgress gameProgress)
        {
            transform.position = gameProgress.WorldProgress.PositionOnScene.ToVector3();
        }

        public void SaveProgress(GameProgress gameProgress)
        {
            gameProgress.WorldProgress.PositionOnScene = transform.position.ToSerializedVector();
        }
    }

    public static class Constants
    {
        public const float Epsilon = 0.001f;
    }
}