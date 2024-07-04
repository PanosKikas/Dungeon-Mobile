using DMT.Characters;

public interface IUsable
{
    bool CanBeUsedOn(Character character);
    void UseOn(Character character);
}
