using System.Collections.Generic;

public class OpenedCardsModel : CardPlaceModel
{
    protected override void GiveCardsMove(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        List<CardModel> giftingCards = new List<CardModel>();
        
            giftingCards.Add(cardModel);
            GiveCard(cardPlaceModel,cardModel);

            InvokeGaveCardsMoveAction(this,cardPlaceModel,giftingCards);
    }
}
