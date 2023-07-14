using System.Collections;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DragableObject _dragableObject;

    private readonly float _movingSpeed=30f;
    
    private CardModel _cardModel;
    private Vector3 _newPosition;
    
    private Coroutine _moving;

    public CardModel Card => _cardModel;

    public void OnDestroy()
    {
        _cardModel.ChangedOpenState -= Refresh;
        _cardModel.ChangedPermissionDragging -= OnChangedDraggingPermission;
    }

    public void Initialize(CardModel card)
    {
        gameObject.name = "Card"+card.Rang.ToString() + card.Suit.ToString();
        _cardModel = card;
        _cardModel.SignToView(this);
        Refresh();
        _cardModel.ChangedOpenState += Refresh;
        _cardModel.ChangedPermissionDragging += OnChangedDraggingPermission;
    }
    
    public void MoveToNewPlace(CardPlaceView cardPlaceView, Transform newParent)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToPlace(cardPlaceView,newParent));
    }

    private void Refresh(CardModel cardModel=null)
    {
        _spriteRenderer.sprite = _cardModel.IsOpen ? _deckImages.GetCardSprite(_cardModel.Rang, _cardModel.Suit) : _downSprite;
    }
    
    private void OnChangedDraggingPermission()
    {
        _dragableObject.enabled = _cardModel.IsDraggingPermission;
    }

    private IEnumerator MovingToPlace(CardPlaceView cardPlaceView, Transform newParent)
    {
        _newPosition = cardPlaceView.GetNextCardPosition();

        transform.position = new Vector3(transform.position.x, transform.position.y, _newPosition.z);
        
        while (Vector3.Distance(transform.position,_newPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,_newPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }

        transform.SetParent(newParent.gameObject.transform);
    }
}
