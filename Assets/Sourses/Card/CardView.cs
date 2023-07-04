using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour,IDragHandler
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    readonly private float _movingSpeed=30f;
    private Vector2 _lastMousePosition;
    private CardModel _cardModel;
    private Coroutine _moving;

    public CardModel Card => _cardModel;
    
    public void Initialize(CardModel card)
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

    public void MoveToNewPlace(CardPlaceView cardPlaceView)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToPlace(cardPlaceView));
    }

    private IEnumerator MovingToPlace(CardPlaceView cardPlaceView)
    {
        Vector3 newPosition = cardPlaceView.GetNextCardPosition();

        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
        
        while (Vector3.Distance(transform.position,newPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }

        transform.SetParent(cardPlaceView.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
