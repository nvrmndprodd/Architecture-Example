using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }

    public override void Initialize()
    {
        var playerGo = new GameObject("Player");
        Player = playerGo.AddComponent<Player>(); // обычно достается через ресурсы
    }
}