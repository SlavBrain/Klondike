using Unity.Mathematics;
using UnityEngine;

public class DeckView : CardPlaceView
{
    [SerializeField] private CardView _cardTemplate;
    [SerializeField] private OpenedCardsView _openedCardsView;
    private Vector3 _offset = new(0, 0, 0.5f);

    public void OnOpenCardButtonClick()
    {
        _cardPlaceModel.TryGiveTopCard(_openedCardsView.Model);
    }
    
    protected override void SignToModelAction()
    {
        if (_cardPlaceModel is DeckModel)
        {
            DeckModel deckModel = _cardPlaceModel as DeckModel;
            deckModel.CreatedNewCard += AddNewCard;
        }

        base.SignToModelAction();        
    }

    protected override void UnsignToModelAction()
    {
        if (_cardPlaceModel is DeckModel)
        {
            DeckModel deckModel = _cardPlaceModel as DeckModel;
            deckModel.CreatedNewCard -= AddNewCard;
        }

        base.UnsignToModelAction();
    }

    private void AddNewCard(CardModel card)
    {
        CardView newCard = Instantiate(_cardTemplate, transform.position + _offset*_cards.Count, quaternion.identity, transform);
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
