using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _cancelMoveButton;
    
    [SerializeField]private Playground _playground;
    [SerializeField]private DeckView _deckView;
    [SerializeField]private OpenedCardsView _openedCardsView;
    [SerializeField]private List<ColumnView> _columnViews;
    [SerializeField]private List<DumpView> _dumpViews;

    private DeckModel _deckModel;
    private OpenedCardsModel _openedCardsModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;

    private void OnEnable()
    {
        InitializeDeck();
        InitializeOpenedCards();
        InitializeColumns();
        InitializeDumps();
        
        InitializePlayground();
        
        BindButtons();
    }

    private void InitializeDeck()
    {
        _deckModel = new DeckModel();
        _deckView.Initialize(_deckModel);
    }
    
    private void InitializeOpenedCards()
    {
        _openedCardsModel = new OpenedCardsModel();
        _openedCardsView.Initialize(_openedCardsModel);
    }

    private void InitializeColumns()
    {
        _columnModels = new List<ColumnModel>();
        
        for (int i = 0; i < _columnViews.Count; i++)
        {
            _columnModels.Add(new ColumnModel());
            _columnViews[i].Initialize(_columnModels[i]);
        }
    }

    private void InitializeDumps()
    {
        _dumpModels = new List<DumpModel>();

        for (int i = 0; i < _dumpViews.Count; i++)
        {
            _dumpModels.Add(new DumpModel());
            _dumpViews[i].Initialize(_dumpModels[i]);
        }
    }

    private void InitializePlayground()
    {
        _playground.Initialize(_deckModel,_columnModels,_startButton);
    }

    private void BindButtons()
    {
        _startButton.onClick.AddListener(_playground.StartGame);
    }
}
