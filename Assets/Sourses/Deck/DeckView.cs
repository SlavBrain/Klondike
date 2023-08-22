using Unity.Mathematics;
using UnityEngine;

public class DeckView : CardPlaceView
{
    [SerializeField] private CardView _cardTemplate;
    [SerializeField] private OpenedCardsView _openedCardsView;
    protected override Vector3 Offset { get; } = new Vector3(0, 0, -0.1f);

    public void OnOpenCardButtonClick()
    {
        if (_cards.Count == CardPlaceModel.Cards.Count)
        {
            if (_cards.Count > 0)
            {
                CardPlaceModel.TryGiveTopCard(_openedCardsView.Model);
            }
            else
            {
                DeckModel deckModel = CardPlaceModel as DeckModel;
                deckModel.TakeAllCard(_openedCardsView.Model as OpenedCardsModel);
            }
        }
    }
    
    protected override void SignToModelAction()
    {
        if (CardPlaceModel is DeckModel)
        {
            DeckModel deckModel = CardPlaceModel as DeckModel;
            deckModel.CreatedNewCard += AddNewCard;
        }

        base.SignToModelAction();        
    }

    protected override void UnsignToModelAction()
    {
        if (CardPlaceModel is DeckModel)
        {
            DeckModel deckModel = CardPlaceModel as DeckModel;
            deckModel.CreatedNewCard -= AddNewCard;
        }

        base.UnsignToModelAction();
    }

    private void AddNewCard(CardModel card)
    {
        CardView newCard = Instantiate(_cardTemplate, transform.position + Offset*_cards.Count, quaternion.identity, transform);
        newCard.Initialize(card);
        _cards.Add(newCard);
    }

    private void OnValidate()
    {
        if (_openedCardsView == null)
        {
            Debug.LogError(name+" :OpenedCardView field is empty");
        }
    }
}
