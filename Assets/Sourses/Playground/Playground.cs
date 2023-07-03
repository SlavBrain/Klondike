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
    private Deck _deck;
    private List<Column> _columns;
    private List<Card> _openedCard;
    private List<Card> _dump;

    private Coroutine _waitingTime;

    private void OnEnable()
    {
        Initialize();
        StartGame();
    }

    private void Initialize()
    {
        _deck = new Deck(_deckView);
        _deckView.Initialize(_deck);
        _openedCard = new List<Card>();
        _dump = new List<Card>();
        _columns = new List<Column>();
        
        for (int i = 0; i < _columnCount; i++)
        {
            _columns.Add(new Column(_columnsView[i]));
            _columnsView[i].Initialize(_columns[i],_cardTemplate);
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
        yield return new WaitForSeconds(5f);
        SpreadOutCard();
    }
}
