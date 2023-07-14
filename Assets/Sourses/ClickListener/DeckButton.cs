using UnityEngine;

public class DeckButton : ClickListener
{ 
    [SerializeField] private DeckView _deckView;

    private void OnEnable()
    {
        Clicked += _deckView.OnOpenCardButtonClick;
    }

    private void OnDisable()
    {
        Clicked -= _deckView.OnOpenCardButtonClick;
    }
}
