using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSystems : MonoBehaviour
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public RuntimeGameSystemContext RuntimeGameSystemContext { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public bool IsInitialized { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    List<GameSystem_Base> _updateGameSystems = new();

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    List<GameSystem_Base> _lateUpdateGameSystems = new();

    List<GameSystem_Base> _updateSystemsToBeRemoved = new();
    List<GameSystem_Base> _lateUpdateSystemsToBeRemoved = new();

    private void OnEnable()
    {
        RefBook.Add(this);
    }

    private void OnDisable()
    {
        RefBook.Remove(this);
    }

    public void Initialize()
    {
        RuntimeGameSystemContext = new RuntimeGameSystemContext();
        IsInitialized = InitializeSystems();
    }

    private bool InitializeSystems()
    {
        foreach (var system in _updateGameSystems)
        {
            if (!system.TryInitialize(this))
            {
                Logger.LogErrorWithTag(LogCategory.GameSystems, $"Error while trying to initialize system : {system}! Cannot continue initializing game systems!");
                return false;
            }
        }

        foreach (var system in _lateUpdateGameSystems)
        {
            if (!system.TryInitialize(this))
            {
                Logger.LogErrorWithTag(LogCategory.GameSystems, $"Error while trying to initialize late update system : {system}! Cannot continue initializing game systems!");
                return false;
            }
        }

        return true;
    }

    public bool TryAddGameSystem(GameSystem_Base gameSystem, bool autoInitialize = true)
    {
        if (gameSystem.IsMarkedForRemoval)
            return false;

        if (autoInitialize)
            if (!gameSystem.TryInitialize(this))
                return false;

        _updateGameSystems.Add(gameSystem);
        return true;
    }

    public bool TryAddGameSystemByType<T>(bool autoInitialize = true) where T : GameSystem_Base
    {
        GameSystem_Base gameSystem = Activator.CreateInstance(typeof(T)) as GameSystem_Base;
        return TryAddGameSystem(gameSystem, autoInitialize);
    }

    public bool TryGetGameSystemByType<T>(out T gameSystem) where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _updateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
        {
            gameSystem = null;
            return false;
        }

        if (foundGameSystem.IsMarkedForRemoval)
        {
            gameSystem = null;
            return false;
        }

        gameSystem = foundGameSystem as T;
        return true;
    }

    public bool TryRemoveGameSystem(GameSystem_Base gameSystem)
    {
        if (!_updateGameSystems.Contains(gameSystem))
            return false;

        _updateSystemsToBeRemoved.Add(gameSystem);
        gameSystem.IsMarkedForRemoval = true;
        return true;
    }

    public bool TryRemoveGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _updateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
            return false;

        _updateSystemsToBeRemoved.Add(foundGameSystem);
        foundGameSystem.IsMarkedForRemoval = true;
        return true;
    }

    public bool TryRemoveGameSystemByType(Type type)
    {
        GameSystem_Base foundGameSystem = _updateGameSystems.Where(x => x.GetType() == type).First();

        if (foundGameSystem == null)
            return false;

        _updateSystemsToBeRemoved.Add(foundGameSystem);
        foundGameSystem.IsMarkedForRemoval = true;
        return true;
    }

    public bool TryAddLateUpdateGameSystem(GameSystem_Base gameSystem, bool autoInitialize = true)
    {
        if (gameSystem.IsMarkedForRemoval)
            return false;

        if (autoInitialize)
            if (!gameSystem.TryInitialize(this))
                return false;

        _lateUpdateGameSystems.Add(gameSystem);
        return true;
    }

    public bool TryAddLateUpdateGameSystemByType<T>(bool autoInitialize = true) where T : GameSystem_Base
    {
        GameSystem_Base gameSystem = Activator.CreateInstance(typeof(T)) as GameSystem_Base;
        return TryAddLateUpdateGameSystem(gameSystem, autoInitialize);
    }

    public bool TryGetLateUpdateGameSystemByType<T>(out T gameSystem) where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _lateUpdateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
        {
            gameSystem = null;
            return false;
        }

        if (foundGameSystem.IsMarkedForRemoval)
        {
            gameSystem = null;
            return false;
        }

        gameSystem = foundGameSystem as T;
        return true;
    }

    public bool TryRemoveLateUpdateGameSystem(GameSystem_Base gameSystem)
    {
        if (!_lateUpdateGameSystems.Contains(gameSystem))
            return false;

        _lateUpdateSystemsToBeRemoved.Add(gameSystem);
        gameSystem.IsMarkedForRemoval = true;
        return true;
    }

    public bool TryRemoveLateUpdateGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _lateUpdateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
            return false;

        _lateUpdateSystemsToBeRemoved.Add(foundGameSystem);
        foundGameSystem.IsMarkedForRemoval = true;
        return true;
    }

    public bool TryRemoveLateUpdateGameSystemByType(Type type)
    {
        GameSystem_Base foundGameSystem = _lateUpdateGameSystems.Where(x => x.GetType() == type).First();

        if (foundGameSystem == null)
            return false;

        _lateUpdateSystemsToBeRemoved.Add(foundGameSystem);
        foundGameSystem.IsMarkedForRemoval = true;
        return true;
    }

    void RemoveToBeRemovedGameSystems()
    {
        foreach (var system in _updateSystemsToBeRemoved)
            _updateGameSystems.Remove(system);

        foreach (var system in _lateUpdateSystemsToBeRemoved)
            _lateUpdateGameSystems.Remove(system);

        _updateSystemsToBeRemoved.Clear();
        _lateUpdateSystemsToBeRemoved.Clear();
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        foreach (var system in _updateGameSystems)
        {
            if (system.IsMarkedForRemoval)
                continue;

            system.Update(RuntimeGameSystemContext);
        }
    }

    private void LateUpdate()
    {
        if (!IsInitialized)
            return;

        foreach (var system in _lateUpdateGameSystems)
        {
            if (system.IsMarkedForRemoval)
                continue;

            system.Update(RuntimeGameSystemContext);
        }

        RemoveToBeRemovedGameSystems();
    }
}
