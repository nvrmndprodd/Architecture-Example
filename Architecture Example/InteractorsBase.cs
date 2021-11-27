using System;
using System.Collections.Generic;
using System.Linq;

public class InteractorsBase
{
    private Dictionary<Type, Interactor> _interactorsMap;
    private SceneConfig _sceneConfig;

    public InteractorsBase(SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
    }

    public void CreateAllInteractors()
    {
        _interactorsMap = _sceneConfig.CreateAllInteractors();
    }

    public void SendOnCreateToAllInteractors()
    {
        _interactorsMap.Values.ToList().ForEach(i => i.OnCreate());
    }
    
    public void InitializeAllInteractors()
    {
        _interactorsMap.Values.ToList().ForEach(i => i.Initialize());
    }
    
    public void SendOnStartToAllInteractors()
    {
        _interactorsMap.Values.ToList().ForEach(i => i.OnStart());
    }

    public T GetInteractor<T>() where T : Interactor
    {
        var type = typeof(T);
        return (T) this._interactorsMap[type];
    }
}