using System.Collections;
using System.Collections.Generic;
using DMT.Characters;
using NUnit.Framework;
using UnityEngine;

public class CharacterManagementPopup : MonoBehaviour
{
    private Player player;
    private Character targetCharacter;
    
    public void ShowForCharacter(Character character, Vector3 position)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Assert.IsNotNull(player, "Player is null");
        if (player.characterParty.Count == 1)
        {
            Destroy(gameObject);
        }
        
        Assert.IsNotNull(character, "Character managed cannot be null");
        targetCharacter = character;
        transform.position = position;
    }
    
    public void AbandonCharacter()
    {
        player.RemoveFromParty(targetCharacter);
    }
}
