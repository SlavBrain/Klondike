using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceView : MonoBehaviour
{
    [SerializeField] protected List<CardView> _cards;
    
    protected Vector3 _defaultOffset = new Vector3(0, 0, -0.5f);
    protected CardPlaceModel CardPlaceModel;
    protected virtual Vector3 Offset { get; }= Vector3.zero;
    private bool _isSignedToModel;
    private CardTransferController _cardTransferController;
    
    public CardPlaceModel Model => CardPlaceModel;

    private void OnEnable()
    {
        if (!_isSignedToModel)
        {
            SignToModelAction();
        }
    }

    private void OnDisable()
    {
        if(_isSignedToModel)
            UnsignToModelAction();
    }

    public void Initialize(CardPlaceModel cardPlaceModel,CardTransferController cardTransferController)
    {
        CardPlaceModel = cardPlaceModel;
        _cardTransferController = cardTransferController;
        CardPlaceModel.SignToView(this);
        SignToModelAction();
    }

    public virtual Vector3 GetNextCardPosition()
    {
        return transform.position+_defaultOffset;
    }

    public virtual Transform GetNextCardParent()
    {
        return this.transform;
    }

    protected virtual void SignToModelAction()
    {
        if (CardPlaceModel != null)
        {
            CardPlaceModel.GaveCard += OnGiveCardMove;
            CardPlaceModel.Reseted += OnModelReset;
            _isSignedToModel = true;
        }
    }

    protected virtual void UnsignToModelAction()
    {
        if (CardPlaceModel != null)
        {
            CardPlaceModel.GaveCard -= OnGiveCardMove;
            CardPlaceModel.Reseted -= OnModelReset;
            _isSignedToModel = false;
        }
    }

    public virtual void OnTakedCard(CardModel card)
    {
        _cards.Add(card.View);
        Refresh();
    }

    private void OnGiveCardMove(CardPlaceModel newCardPlaceModel, CardModel cardModel)
    {
        _cardTransferController.AddTransfer(cardModel,newCardPlaceModel);
        _cards.Remove(cardModel.View);
    }

    private void OnModelReset()
    {
        foreach(CardView card in _cards)
        {
            Destroy(card.gameObject);
        }

        _cards.Clear();
    }

    protected virtual void Refresh()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.position = transform.position + Offset * i;
        }
    }
}
