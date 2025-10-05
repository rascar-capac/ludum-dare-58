using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameButtonDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TreasureProximityDetector.Instance.SetFreaking(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TreasureProximityDetector.Instance.SetFreaking(false);
    }
}
