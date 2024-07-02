using System.Collections;
using System.Collections.Generic;
using DMT.Characters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPopup : MonoBehaviour
{
    [SerializeField] private CharacterSelectSlotUI characterSlotPrefab;

    [SerializeField] private Transform characterContainer;

    public readonly Subject<Character> CharacterSelected = new();
    
    [SerializeField]
    private Vector2 offset = new(0f, 75f);

    public void Initialize(IEnumerable<Character> characters, Vector2 position)
    {
        characterContainer.transform.position =
            new Vector3(position.x, position.y, characterContainer.transform.position.z) +
            new Vector3(offset.x, offset.y, 0f);
        foreach (var character in characters)
        {
            var characterSlot = Instantiate(characterSlotPrefab, characterContainer);
            characterSlot.SetTo(character);
            var characterButton = characterSlot.GetComponent<Button>();
            characterButton.onClick.AsObservable().Subscribe(_ => OnCharacterSelected(character));
        }
    }

    private void OnCharacterSelected(Character character)
    {
        CharacterSelected.OnNext(character);
        Hide();
    }

    public void Hide()
    {
        Destroy(gameObject);
    }
}