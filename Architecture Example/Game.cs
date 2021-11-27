using System;
using System.Collections;

// ARCHITECTURE ASSET : https://github.com/vavilichev/UnityGameArchitecturePreset
public static class Game
{
    public static Action OnGameInitialized;
    
    public static SceneManagerBase SceneManager { get; private set; }
    
    public static void Run()
    {
        SceneManager = new SceneManagerExample();
        Coroutines.StartRoutine(InitializeGameRoutine());
    }

    private static IEnumerator InitializeGameRoutine()
    {
        SceneManager.InitScenes();
        yield return SceneManager.LoadCurrentSceneAsync();
        OnGameInitialized?.Invoke();
    }

    public static T GetInteractor<T>() where T : Interactor
    {
        return SceneManager.GetInteractor<T>();
    }
    
    public static T GetRepository<T>() where T : Repository
    {
        return SceneManager.GetRepository<T>();
    }
}