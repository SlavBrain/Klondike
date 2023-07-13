public class CardOpenChange : GameChanges
{
    public CardModel CardModel { get; private set; }
    public bool IsOpen { get; private set; }

    public CardOpenChange(CardModel cardModel, bool isOpen)
    {
        CardModel= cardModel;
        IsOpen = isOpen;
    }
}
