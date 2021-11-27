public sealed class SceneManagerExample : SceneManagerBase
{
    public override void InitScenes()
    {
        _sceneConfigs[SceneConfigExample.SCENE_NAME] = new SceneConfigExample();
    }
}