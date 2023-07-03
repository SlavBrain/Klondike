using UnityEngine;

public interface ICardPlaceView
{
    public void GiveCard(CardView cardView,ICardPlaceView cardPlaceView);
    public void TakeCard(CardView newCardView);
    public Vector3 NextCardPosition();
}
