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
    private const int _dumpCount = 4;
    private DeckModel _deck;
    private List<ColumnModel> _columns;
    private OpenedCardModel _openedCardModel;
    private List<DumpModel> _dumps;

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
        _openedCardModel = new OpenedCardModel();
        _openedCardsView.Initialize(_openedCardModel);
        _dumps = new List<DumpModel>();
        
        _columns = new List<ColumnModel>();
        
        for (int i = 0; i < _columnCount; i++)
        {
            _columns.Add(new ColumnModel());
            _columnsView[i].Initialize(_columns[i]);
        }

        for (int i = 0; i < _dumpCount; i++)
        {
            _dumps.Add(new DumpModel());
            _dumpsView[i].Initialize(_dumps[i]);
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
