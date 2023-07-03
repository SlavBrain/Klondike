using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float _movingSpeed=10f;
    private Card _cardModel;
    private Coroutine _moving;

    public Card Card => _cardModel;
    
    public void Initialize(Card card)
    {
        _cardModel = card;
        _cardModel.SignToView(this);
        Refresh();
        _cardModel.ChangedOpenState += Refresh;
    }

    private void Refresh()
    {
        if (_cardModel.IsOpen)
        {
            _spriteRenderer.sprite = _deckImages.GetCardSprite(_cardModel.Rang, _cardModel.Suit);
        }
        else
        {
            _spriteRenderer.sprite = _downSprite;
        }
    }

    public void MoveToNewPlace(ICardPlaceView cardPlaceView)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToPlace(cardPlaceView));
    }

    private IEnumerator MovingToPlace(ICardPlaceView cardPlaceView)
    {
        Vector3 newPosition = cardPlaceView.NextCardPosition();
        int count = 0;
        
        while (Vector3.Distance(transform.position,newPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }

        transform.SetParent(cardPlaceView as Transform);
    }
}
