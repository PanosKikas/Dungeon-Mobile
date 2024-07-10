using System;
using System.Linq;
using DMT.Characters;
using DMT.Controllers;
using NUnit.Framework;
using UniRx;
using UnityEngine;

public class GameOverListener : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private IDisposable gameOverSubscription;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Assert.IsNotNull(player, "Cannot find player in game over listener");
        gameOverSubscription = player.CharacterParty.CharacterRemoved.Subscribe(CharacterRemoved);
    }

    private void CharacterRemoved(CollectionRemoveEvent<Character> character)
    {
        if (player.CharacterParty.Any())
        {
            return;
        }
        
        gameOverSubscription?.Dispose();
        Debug.Log("GAME OVER.");
        // TODO: Show Game Over UI.
    }
}
