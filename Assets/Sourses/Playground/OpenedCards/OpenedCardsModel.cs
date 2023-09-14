using System.Collections.Generic;
using UnityEngine;

public class OpenedCardsModel : CardPlaceModel
{
    protected override void GiveCardsMove(CardPlaceModel cardPlaceModel,CardModel cardModel)
    {
        Debug.Log("26)StartFillingAll");
        List<CardModel> giftingCards = new List<CardModel>();
        
        Debug.Log("27)StartFillingAll");
        giftingCards.Add(cardModel);
        Debug.Log("28)StartFillingAll");
        GiveCard(cardPlaceModel,cardModel);
        Debug.Log("29)StartFillingAll");
        InvokeGaveCardsMoveAction(this,cardPlaceModel,giftingCards);
    }
}
