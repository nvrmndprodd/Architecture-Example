using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour
{
    private Player _player;
    
    private void Start()
    {
        Game.Run();
        Game.OnGameInitialized += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitialized -= OnGameInitialized;
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.Player;
    }

    private void Update()
    {
        if (!Bank.IsInitialized) return;
        if (_player == null) return; // иначе ошибка, потому что банк может быть уже инициализирован,
                                     // а плеер проинициализируется в следующем кадре
        
        Debug.Log("Player position: " + _player.transform.position);
        
        if (Input.GetKeyDown(KeyCode.A))
            Bank.AddCoins(this, 5);
        if (Input.GetKeyDown(KeyCode.D))
            Bank.Spend(this, 10);
    }
}