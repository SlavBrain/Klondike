using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardTransferController : MonoBehaviour
{
    private Queue<CardTransfer> _cardTransfers=new Queue<CardTransfer>();
    private bool _isLaunching = false;
    private readonly float _launchingDelay = 0.1f;
    private Playground _playground;
    private Coroutine _launchingTransfers;

    public void Initialize(Playground playground)
    {
        _playground = playground;
        _playground.GameStarting += ClearQueue;
    }
    
    private void ClearQueue()
    {
        if (_launchingTransfers != null)
        {
            StopCoroutine(_launchingTransfers);
        }
        _cardTransfers.Clear();
    }

    public void AddTransfer(CardModel cardModel, CardPlaceModel cardPlaceModel)
    {
        _cardTransfers.Enqueue(new CardTransfer(cardModel,cardPlaceModel));

        if (!_isLaunching)
        {
            StartLaunchingTransferCards();
        }
    }

    private void LaunchTransfer()
    {
        if (_cardTransfers.Count > 0)
        {
            CardTransfer nextCardTransfer = _cardTransfers.Dequeue();
            nextCardTransfer.CardModel.View.MoveToNewPlace(nextCardTransfer.CardPlaceModel.View,nextCardTransfer.CardPlaceModel.View.GetNextCardParent());
        }
    }

    private void StartLaunchingTransferCards()
    {
        if (_launchingTransfers != null)
        {
            _isLaunching = false;
            StopCoroutine(_launchingTransfers);
        }

        _launchingTransfers = StartCoroutine(LaunchingTransferCards());
    }

    private IEnumerator LaunchingTransferCards()
    {
        _isLaunching = true;
        WaitForSeconds delay = new WaitForSeconds(_launchingDelay);

        while (_cardTransfers.Count > 0)
        {
            LaunchTransfer();
            yield return delay;
        }

        _isLaunching = false;
    }
}

public struct CardTransfer
{
    private CardModel _cardModel;
    private CardPlaceModel _cardPlaceModel;
    
    public CardTransfer(CardModel cardModel, CardPlaceModel cardPlaceModel)
    {
        _cardModel = cardModel;
        _cardPlaceModel = cardPlaceModel;
    }

    public CardModel CardModel => _cardModel;
    public CardPlaceModel CardPlaceModel => _cardPlaceModel;

}
