using UnityEngine;

public class InterstitialCounter : MonoBehaviour
{
    public static InterstitialCounter Instance;
    private int _count = 0;
    private const int _maxCount=2;
    
    public void Initialize()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Increment()
    {
        _count++;

        if (_count >= _maxCount)
        {
            AdController.Instance.ShowInterstitial(onSuccess:ResetCount);
        }
    }

    public void ResetCount()
    {
        _count = 0;
    }
}
