using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    [SerializeField] private CardView _cardTemplate;
    [SerializeField] private List<ColumnView> _columnsView;
    [SerializeField] private DeckView _deckView;
    [SerializeField] private List<DumpView> _dumpsView;
    [SerializeField] private OpenedCardsView _openedCardsView;

    private const int _columnCount = 7;
    private DeckModel _deck;
    private List<ColumnModel> _columns;
    private List<CardModel> _openedCard;
    private List<CardModel> _dump;

    private Coroutine _waitingTime;

    private void OnEnable()
    {
        Initialize();
        StartGame();
    }

    private void Initialize()
    {
        _deck = new DeckModel();
        _deckView.Initialize(_deck);
        _openedCard = new List<CardModel>();
        _dump = new List<CardModel>();
        _columns = new List<ColumnModel>();
        
        for (int i = 0; i < _columnCount; i++)
        {
            _columns.Add(new ColumnModel());
            _columnsView[i].Initialize(_columns[i]);
        }
    }

    private void StartGame()
    {
        _deck.CreateNew();
        StartWaitInitialization();
    }

    private void SpreadOutCard()
    {
        for (int i = 0; i < _columnCount; i++)
        {
            _columns[i].Fill(_deck,i+1);
            _columns[i].OpenLastCard();
        }
    }

    private void StartWaitInitialization()
    {
        if (_waitingTime != null)
        {
            StopCoroutine(_waitingTime);
        }

        _waitingTime = StartCoroutine(WaitInitialization());
    }

    private IEnumerator WaitInitialization()
    {
        yield return new WaitForSeconds(2f);
        SpreadOutCard();
    }
}
