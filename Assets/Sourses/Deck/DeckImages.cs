using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "DeckImages")]
public class DeckImages : ScriptableObject
{
    [Header("Hearts")]
    [SerializeField] public Sprite AceHearts;
    [SerializeField] public Sprite TwoHearts;
    [SerializeField] public Sprite ThreeHearts;
    [SerializeField] public Sprite FourHearts;
    [SerializeField] public Sprite FiveHearts;
    [SerializeField] public Sprite SixHearts;
    [SerializeField] public Sprite SevenHearts;
    [SerializeField] public Sprite EightHearts;
    [SerializeField] public Sprite NineHearts;
    [SerializeField] public Sprite TenHearts;
    [SerializeField] public Sprite JackHearts;
    [SerializeField] public Sprite QueenHearts;
    [SerializeField] public Sprite KingHearts;
    [Header("Diamond")]
    [SerializeField] public Sprite AceDiamonds;
    [SerializeField] public Sprite TwoDiamonds;
    [SerializeField] public Sprite ThreeDiamonds;
    [SerializeField] public Sprite FourDiamonds;
    [SerializeField] public Sprite FiveDiamonds;
    [SerializeField] public Sprite SixDiamonds;
    [SerializeField] public Sprite SevenDiamonds;
    [SerializeField] public Sprite EightDiamonds;
    [SerializeField] public Sprite NineDiamonds;
    [SerializeField] public Sprite TenDiamonds;
    [SerializeField] public Sprite JackDiamonds;
    [SerializeField] public Sprite QueenDiamonds;
    [SerializeField] public Sprite KingDiamonds;
    [Header("Spades")]
    [SerializeField] public Sprite AceSpades;
    [SerializeField] public Sprite TwoSpades;
    [SerializeField] public Sprite ThreeSpades;
    [SerializeField] public Sprite FourSpades;
    [SerializeField] public Sprite FiveSpades;
    [SerializeField] public Sprite SixSpades;
    [SerializeField] public Sprite SevenSpades;
    [SerializeField] public Sprite EightSpades;
    [SerializeField] public Sprite NineSpades;
    [SerializeField] public Sprite TenSpades;
    [SerializeField] public Sprite JackSpades;
    [SerializeField] public Sprite QueenSpades;
    [SerializeField] public Sprite KingSpades;
    [Header("Clubs")]
    [SerializeField] public Sprite AceClubs;
    [SerializeField] public Sprite TwoClubs;
    [SerializeField] public Sprite ThreeClubs;
    [SerializeField] public Sprite FourClubs;
    [SerializeField] public Sprite FiveClubs;
    [SerializeField] public Sprite SixClubs;
    [SerializeField] public Sprite SevenClubs;
    [SerializeField] public Sprite EightClubs;
    [SerializeField] public Sprite NineClubs;
    [SerializeField] public Sprite TenClubs;
    [SerializeField] public Sprite JackClubs;
    [SerializeField] public Sprite QueenClubs;
    [SerializeField] public Sprite KingClubs;

    public Sprite GetCardSprite(CardRangs rang,CardSuits suit)
    {
        Sprite sprite = null;
        
        switch (suit)
        {
            case CardSuits.Hearts:
                switch (rang)
                {
                    case CardRangs.Ace:
                        sprite= AceHearts;
                        break;
                    case CardRangs.Two:
                        sprite=  TwoHearts;
                        break;
                    case CardRangs.Three:
                        sprite= ThreeHearts;
                        break;
                    case CardRangs.Four:
                        sprite= FourHearts;
                        break;
                    case CardRangs.Five:
                        sprite= FiveHearts;
                        break;
                    case CardRangs.Six:
                        sprite= SixHearts;
                        break;
                    case CardRangs.Seven:
                        sprite= SevenHearts;
                        break;
                    case CardRangs.Eight:
                        sprite= EightHearts;
                        break;
                    case CardRangs.Nine:
                        sprite= NineHearts;
                        break;
                    case CardRangs.Ten:
                        sprite= TenHearts;
                        break;
                    case CardRangs.Jack:
                        sprite= JackHearts;
                        break;
                    case CardRangs.Queen:
                        sprite= QueenHearts;
                        break;
                    case CardRangs.King:
                        sprite= KingHearts;
                        break;
                }
                break;
            case CardSuits.Diamonds:
                switch (rang)
                {
                    case CardRangs.Ace:
                        sprite= AceDiamonds;
                        break;
                    case CardRangs.Two:
                        sprite= TwoDiamonds;
                        break;
                    case CardRangs.Three:
                        sprite= ThreeDiamonds;
                        break;
                    case CardRangs.Four:
                        sprite= FourDiamonds;
                        break;
                    case CardRangs.Five:
                        sprite= FiveDiamonds;
                        break;
                    case CardRangs.Six:
                        sprite= SixDiamonds;
                        break;
                    case CardRangs.Seven:
                        sprite= SevenDiamonds;
                        break;
                    case CardRangs.Eight:
                        sprite= EightDiamonds;
                        break;
                    case CardRangs.Nine:
                        sprite= NineDiamonds;
                        break;
                    case CardRangs.Ten:
                        sprite= TenDiamonds;
                        break;
                    case CardRangs.Jack:
                        sprite=  JackDiamonds;
                        break;
                    case CardRangs.Queen:
                        sprite= QueenDiamonds;
                        break;
                    case CardRangs.King:
                        sprite= KingDiamonds;
                        break;
                }
                break;
            case CardSuits.Spades:
                switch (rang)
                {
                    case CardRangs.Ace:
                        sprite=  AceSpades;
                        break;
                    case CardRangs.Two:
                        sprite= TwoSpades;
                        break;
                    case CardRangs.Three:
                        sprite= ThreeSpades;
                        break;
                    case CardRangs.Four:
                        sprite= FourSpades;
                        break;
                    case CardRangs.Five:
                        sprite= FiveSpades;
                        break;
                    case CardRangs.Six:
                        sprite= SixSpades;
                        break;
                    case CardRangs.Seven:
                        sprite= SevenSpades;
                        break;
                    case CardRangs.Eight:
                        sprite= EightSpades;
                        break;
                    case CardRangs.Nine:
                        sprite= NineSpades;
                        break;
                    case CardRangs.Ten:
                        sprite= TenSpades;
                        break;
                    case CardRangs.Jack:
                        sprite= JackSpades;
                        break;
                    case CardRangs.Queen:
                        sprite= QueenSpades;
                        break;
                    case CardRangs.King:
                        sprite= KingSpades;
                        break;
                }
                break;
            case CardSuits.Clubs:
                switch (rang)
                {
                    case CardRangs.Ace:
                        sprite= AceClubs;
                        break;
                    case CardRangs.Two:
                        sprite= TwoClubs;
                        break;
                    case CardRangs.Three:
                        sprite= ThreeClubs;
                        break;
                    case CardRangs.Four:
                        sprite= FourClubs;
                        break;
                    case CardRangs.Five:
                        sprite= FiveClubs;
                        break;
                    case CardRangs.Six:
                        sprite=  SixClubs;
                        break;
                    case CardRangs.Seven:
                        sprite= SevenClubs;
                        break;
                    case CardRangs.Eight:
                        sprite= EightClubs;
                        break;
                    case CardRangs.Nine:
                        sprite= NineClubs;
                        break;
                    case CardRangs.Ten:
                        sprite= TenClubs;
                        break;
                    case CardRangs.Jack:
                        sprite= JackClubs;
                        break;
                    case CardRangs.Queen:
                        sprite= QueenClubs;
                        break;
                    case CardRangs.King:
                        sprite= KingClubs;
                        break;
                }
                break;
        }

        return sprite;
    }
}
