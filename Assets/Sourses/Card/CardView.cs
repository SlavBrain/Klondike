using System.Collections;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DragableObject _dragableObject;

    private readonly float _movingSpeed=30f;
    
    private CardModel _modelModel;
    private Vector3 _newPosition;
    
    private Coroutine _moving;

    public CardModel Model => _modelModel;

    public void OnDestroy()
    {
        _modelModel.ChangedOpenState -= Refresh;
        _modelModel.ChangedPermissionDragging -= OnChangedDraggingPermission;
    }

    public void Initialize(CardModel card)
    {
        gameObject.name = "Card"+card.Rang.ToString() + card.Suit.ToString();
        _modelModel = card;
        _modelModel.SignToView(this);
        Refresh();
        _modelModel.ChangedOpenState += Refresh;
        _modelModel.ChangedPermissionDragging += OnChangedDraggingPermission;
    }
    
    public void MoveToNewPlace(CardPlaceView cardPlaceView, Transform newParent)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToPlace(cardPlaceView,newParent));
    }

    public void MoveBoomerang(CardPlaceView cardPlaceView)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingBoomerang(cardPlaceView));
    }

    private void Refresh(CardModel cardModel=null)
    {
        _spriteRenderer.sprite = _modelModel.IsOpen ? _deckImages.GetCardSprite(_modelModel.Rang, _modelModel.Suit) : _downSprite;
    }
    
    private void OnChangedDraggingPermission()
    {
        _dragableObject.enabled = _modelModel.IsDraggingPermission;
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

    private IEnumerator MovingBoomerang(CardPlaceView cardPlaceView)
    {
        Vector3 oldPosition = transform.position;
        Vector3 tempPosition = cardPlaceView.GetNextCardPosition();
        
        transform.position = new Vector3(transform.position.x, transform.position.y, tempPosition.z);
        
        while (Vector3.Distance(transform.position,tempPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,tempPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        
        while (Vector3.Distance(transform.position,oldPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,oldPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }
    }
}
