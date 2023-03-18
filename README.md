![bandicam 2023-03-18 11-48-26-092 (online-video-cutter com)](https://user-images.githubusercontent.com/78159702/226095593-da7ffaa6-faef-4cf3-ab75-191edfec0c31.gif)

Проект был создан для демонстрации примеров кода и архитектуры, в проекте реализовано:
1) Некоторое количество сервисов и их регистрация в Project Context (Zenject)
2) Bootstraper
3) Машина состояний игры : (Bootsstrap state, LoadProgress state, LoadService state, Gameloop state)
4) GameFactory, UIFactory с инициализацией игрового мира и UI
5) Сохранение и загрузка прогресса посредством SaveLoadService одним файлом json
6) Загрузка всех необходимых ассетов через Addressables и AssetProviderService
7) Статик дата в конфигах (Scriptable object), которую Factory получает через ConfigService при инициализации объектов сцены
8) Сам геймплей сделан монобехами компонентным подходом, предпочитая композицию наследованию
9) Пассивный UI с отделением от него логики через ActorUI
10) InputService реализует New Input System
