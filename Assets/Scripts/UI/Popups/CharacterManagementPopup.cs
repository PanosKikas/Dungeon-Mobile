using DMT.Controllers;
using NUnit.Framework;
using UnityEngine;

namespace DMT.Characters.UI
{
    public class CharacterManagementPopup : MonoBehaviour
    {
        private Player player;
        private Character targetCharacter;

        public void ShowForCharacter(Character character, Vector3 position)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Assert.IsNotNull(player, "Player is null");
            if (player.CharacterParty.Count == 1)
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
}
