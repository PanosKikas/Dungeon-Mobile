using DMT.Characters.Stats;
using UnityEngine;
using DMT.Characters;
using UnityEngine.Assertions;

public class JoinPartyBehaviour : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField] 
    private InitialCharacterData characterData;
    
    public void TryJoinPlayerParty()
    {
        Assert.IsNotNull(player, "Player is null");
        player.AddToParty(characterData);
        Destroy(gameObject);
    }
}
