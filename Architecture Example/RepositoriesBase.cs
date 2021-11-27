using System;
using System.Collections.Generic;
using System.Linq;

public class RepositoriesBase
{
    private Dictionary<Type, Repository> _repositoriesMap;
    private SceneConfig _sceneConfig;

    public RepositoriesBase(SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
    }

    public void CreateAllRepositories()
    {
        _repositoriesMap = _sceneConfig.CreateAllRepositories();
    }

    public void SendOnCreateToAllRepositories()
    {
        _repositoriesMap.Values.ToList().ForEach(i => i.OnCreate());
    }
    
    public void InitializeAllRepositories()
    {
        _repositoriesMap.Values.ToList().ForEach(i => i.Initialize());
    }
    
    public void SendOnStartToAllRepositories()
    {
        _repositoriesMap.Values.ToList().ForEach(i => i.OnStart());
    }

    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);
        return (T) this._repositoriesMap[type];
    }
}