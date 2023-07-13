using System.Collections.Generic;

public class CardTransferChange : GameChanges
{
    public CardPlaceModel OldCardPlaceModel { get; private set; }
    public CardPlaceModel NewCardPlaceModel { get; private set; }
    public List<CardModel> CardModel { get; private set; }
    
    public CardTransferChange(CardPlaceModel oldCardPlaceModel, CardPlaceModel newCardPlaceModel, List<CardModel> cardModel)
    {
        OldCardPlaceModel = oldCardPlaceModel;
        NewCardPlaceModel = newCardPlaceModel;
        CardModel = cardModel;
    }
}
