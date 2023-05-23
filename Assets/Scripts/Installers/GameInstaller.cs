using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform _systemPath;
    [SerializeField] private ShootController _shootController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private UIController _controllerUI;
    [SerializeField] private Transform _uiContainer;
    
    public override void InstallBindings()
    {
        InputInstance();
        UIControllerInstance();
        PlayerInstance();
        ShootInstance();
        LevelInstance();
    }

    private void InputInstance()
    {
        var inputInstance = 
            Container.InstantiatePrefabForComponent<InputController>(_inputController, Vector3.zero,
                Quaternion.identity, _systemPath);
        
        Container.Bind<InputController>().FromInstance(inputInstance).AsSingle().NonLazy();
    }

    private void ShootInstance()
    {
        var shootInstance =
            Container.InstantiatePrefabForComponent<ShootController>(_shootController, Vector3.zero,
                Quaternion.identity, _systemPath);
        Container.Bind<ShootController>().FromInstance(shootInstance).AsSingle().NonLazy();
    }

    private void PlayerInstance()
    {
        var playerInstance =
            Container.InstantiatePrefabForComponent<PlayerMovementController>(_playerMovementController, _playerSpawnPoint.position, 
                Quaternion.identity, null);
        
        Container.Bind<PlayerMovementController>().FromInstance(playerInstance).AsSingle().NonLazy();
    }

    private void LevelInstance()
    {
        var levelInstance =
            Container.InstantiatePrefabForComponent<LevelController>(_levelController, Vector3.zero,
                Quaternion.identity, _systemPath);
        Container.Bind<LevelController>().FromInstance(levelInstance).AsSingle()
            .NonLazy();
    }

    private void UIControllerInstance()
    {
        var uiControllerInstance =
            Container.InstantiatePrefabForComponent<UIController>(_controllerUI, _uiContainer.position, Quaternion.identity,
                _uiContainer);
        Container.Bind<UIController>().FromInstance(uiControllerInstance).AsSingle().NonLazy();
    }
}
