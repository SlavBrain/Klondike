using UnityEngine;

public class Panel : MonoBehaviour
{
    public void Enable() => gameObject.SetActive(true);
        
    public void Disable() => gameObject.SetActive(false);
}
