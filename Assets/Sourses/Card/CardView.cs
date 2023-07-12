using System.Collections;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DragableObject _dragableObject;

    readonly private float _movingSpeed=30f;
    private CardModel _cardModel;
    private Coroutine _moving;

    public CardModel Card => _cardModel;

    public void OnDestroy()
    {
        _cardModel.ChangedOpenState -= Refresh;
        _cardModel.ChangedPermissionDragging -= OnChangedDraggingPermission;
    }

    public void Initialize(CardModel card)
    {
        _cardModel = card;
        _cardModel.SignToView(this);
        Refresh();
        _cardModel.ChangedOpenState += Refresh;
        _cardModel.ChangedPermissionDragging += OnChangedDraggingPermission;
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

    private void OnChangedDraggingPermission()
    {
        _dragableObject.enabled = _cardModel.IsDraggingPermission;
    }
}
