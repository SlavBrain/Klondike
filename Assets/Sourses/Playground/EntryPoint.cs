using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _cancelMoveButton;
    [SerializeField] private Button _findMovesButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private MessagePanel _notEnoughCoinsPanel;
    [SerializeField] private EndingGameController _endingGameController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CardTransferController _cardTransferController;
    
    [SerializeField]private Playground _playground;
    [SerializeField]private DeckView _deckView;
    [SerializeField]private OpenedCardsView _openedCardsView;
    [SerializeField]private List<ColumnView> _columnViews;
    [SerializeField]private List<DumpView> _dumpViews;

    private DeckModel _deckModel;
    private OpenedCardsModel _openedCardsModel;
    private List<ColumnModel> _columnModels;
    private List<DumpModel> _dumpModels;

    private GameMovesLogger _gameMovesLogger;
    private MoveCanceler _moveCanceler;
    private EndGameInspector _endGameInspector;

    private MoveFinder _moveFinder;
    private AutoFiller _autoFiller;
    
    private void OnEnable()
    {
        InitializeDeck();
        InitializeOpenedCards();
        InitializeColumns();
        InitializeDumps();
        
        InitializePlayground();
        
        InitializeGameMovesLogger();
        InitializeMoveCanceler();
        InitializeEndGameInspector();
        InitializeEndingGameController();
        InitializeMoveFinder();
        InitializeAutoFiller();
        
        BindButtons();
        BindEventActions();
    }

    private void InitializeDeck()
    {
        _deckModel = new DeckModel();
        _deckView.Initialize(_deckModel,_cardTransferController);
    }
    
    private void InitializeOpenedCards()
    {
        _openedCardsModel = new OpenedCardsModel();
        _openedCardsView.Initialize(_openedCardsModel,_cardTransferController);
    }

    private void InitializeColumns()
    {
        _columnModels = new List<ColumnModel>();
        
        for (int i = 0; i < _columnViews.Count; i++)
        {
            _columnModels.Add(new ColumnModel());
            _columnViews[i].Initialize(_columnModels[i],_cardTransferController);
        }
    }

    private void InitializeDumps()
    {
        _dumpModels = new List<DumpModel>();

        for (int i = 0; i < _dumpViews.Count; i++)
        {
            _dumpModels.Add(new DumpModel());
            _dumpViews[i].Initialize(_dumpModels[i],_cardTransferController);
        }
    }

    private void InitializePlayground()
    {
        _playground.Initialize(_deckModel,_columnModels,_startButton,_restartButton,_notEnoughCoinsPanel);
    }

    private void InitializeGameMovesLogger()
    {
        _gameMovesLogger = new GameMovesLogger(_playground, _deckModel, _openedCardsModel, _columnModels, _dumpModels);
    }

    private void InitializeMoveCanceler()
    {
        _moveCanceler = new MoveCanceler(_gameMovesLogger, _cancelMoveButton);
    }

    private void InitializeEndGameInspector()
    {
        _endGameInspector = new EndGameInspector(_dumpModels);
    }

    private void InitializeEndingGameController()
    {
        _endingGameController.Initialize(_endGameInspector);
    }

    private void InitializeMoveFinder()
    {
        _moveFinder = new MoveFinder(_openedCardsModel, _columnModels, _dumpModels);
    }

    private void InitializeAutoFiller()
    {
        _autoFiller = new AutoFiller(_inputController, _moveFinder, _deckModel, _columnModels, _openedCardsModel,_dumpModels);
    }

    private void BindButtons()
    {
        _findMovesButton.onClick.AddListener(_moveFinder.OnHelpInvoke);
        _backToMenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void BindEventActions()
    {
        _playground.GameStarting += _deckModel.Reset;
        _playground.GameStarting += _openedCardsModel.Reset;

        foreach(ColumnModel column in _columnModels)
        {
            _playground.GameStarting += column.Reset;
        }

        foreach(DumpModel dump in _dumpModels)
        {
            _playground.GameStarting += dump.Reset;
        }
    }

    private void BackToMainMenu()
    {
        MainMenu.Load();
    }
}
