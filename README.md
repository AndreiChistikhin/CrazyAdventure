![bandicam 2023-03-18 11-48-26-092 (online-video-cutter com)](https://user-images.githubusercontent.com/78159702/226095593-da7ffaa6-faef-4cf3-ab75-191edfec0c31.gif)

This project was created to demonstrate code and architecture examples. It includes the following features:

1) Service architecture with service registration in Project Context (Zenject).
2) Bootstrapper to initialize the game.
3) Game State Machine to control global game state
4) GameFactory and UIFactory for initializing the game world and UI.
5) Save and load system using SaveLoadService, storing progress in a single JSON file.
6) Asset loading via Addressables and AssetProviderService.
7) Static data in configurations (ScriptableObjects), provided to factories via ConfigService during scene object initialization.
8) Gameplay is implemented using a component-based approach with MonoBehaviours, favoring composition over inheritance.
9) Decoupled UI logic with a passive UI approach using ActorUI.
10) InputService utilizing the New Input System.
___________________________________________________________________________________________________________
11) Server-side functionality for user registration, authentication, and recording completion times in a database:
[CrazyAdventureServer](https://github.com/AndreiChistikhin/CrazyAdventureServer)
