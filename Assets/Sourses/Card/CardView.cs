using System.Collections;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private DeckImages _deckImages;
    [SerializeField] private Sprite _downSprite;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DragableObject _dragableObject;

    private readonly float _movingSpeed=50f;
    private readonly float _movingCoordinateZ = -10;
    
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

    public void MoveToDot(Vector3 newPosition)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingToDot(newPosition));
    }

    private void Refresh(CardModel cardModel=null)
    {
        _spriteRenderer.sprite = _modelModel.IsOpen ? _deckImages.GetCardSprite(_modelModel.Rang, _modelModel.Suit) : _downSprite;
    }
    
    private void OnChangedDraggingPermission()
    {
        _dragableObject.enabled = _modelModel.IsDraggingPermission;
    }

    private IEnumerator MovingToDot(Vector3 newPosition)
    {
        while (Vector3.Distance(transform.position,newPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MovingToPlace(CardPlaceView cardPlaceView, Transform newParent)
    {
        _newPosition = cardPlaceView.GetNextCardPosition();
        Vector3 tempNewPositionForMoving = new Vector3(_newPosition.x, _newPosition.y, _movingCoordinateZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, _movingCoordinateZ);
        
        while (Vector3.Distance(transform.position,tempNewPositionForMoving)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,tempNewPositionForMoving,_movingSpeed*Time.deltaTime);
            yield return null;
        }
        
        SoundController.Instance.PlayCardMovement();
        transform.SetParent(newParent.gameObject.transform);
        transform.position = _newPosition;
        cardPlaceView.OnTakedCard(this.Model);
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
        
        SoundController.Instance.PlayCardMovement();
        yield return new WaitForSeconds(0.3f);
        
        while (Vector3.Distance(transform.position,oldPosition)!>0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,oldPosition,_movingSpeed*Time.deltaTime);
            yield return null;
        }
        
        SoundController.Instance.PlayCardMovement();
    }
}
