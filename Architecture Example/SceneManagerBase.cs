using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManagerBase // Обёртка над SceneManager от юнити
{
    public event Action<Scene> OnSceneLoaded;
    
    public Scene Scene { get; private set; }
    public bool IsLoading { get; private set; }

    protected Dictionary<string, SceneConfig> _sceneConfigs;

    public SceneManagerBase()
    {
        _sceneConfigs = new Dictionary<string, SceneConfig>();
    }

    public abstract void InitScenes();

    public Coroutine LoadCurrentSceneAsync()
    {
        if (IsLoading)
        {
            throw new Exception("Scene is loading now");
        }

        var sceneName = SceneManager.GetActiveScene().name;
        var config = _sceneConfigs[sceneName];
        return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
    }

    private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig) // например, для инициализации первой сцены,
    {                                                                     // т.к. при запуске игры она создается автоматически
        IsLoading = true;

        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));
        
        IsLoading = false;
        OnSceneLoaded?.Invoke(Scene);
    }

    public Coroutine LoadNewSceneAsync(string sceneName)
    {
        if (IsLoading)
        {
            throw new Exception("Scene is loading now");
        }

        var config = _sceneConfigs[sceneName];
        return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
    }

    private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
    {
        IsLoading = true;

        yield return Coroutines.StartRoutine(LoadSceneRoutine(sceneConfig));
        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));
        
        IsLoading = false;
        OnSceneLoaded?.Invoke(Scene);
    }
    
    private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
    {
        var async = SceneManager.LoadSceneAsync(sceneConfig.SceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) // ждем пока загружается следующая сцена
            yield return null;

        async.allowSceneActivation = true; // выгружаем старую сцену из памяти и меняем
    }

    private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
    {
        Scene = new Scene(sceneConfig);
        yield return Scene.InitializeAsync();
    }

    public T GetRepository<T>() where T : Repository
    {
        return Scene.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return Scene.GetInteractor<T>();
    }
}