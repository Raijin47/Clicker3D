using UnityEngine;
using UnityEngine.UI;

public class TestFlexable : MonoBehaviour
{
    [SerializeField] private RectTransform _referenceSize;
    [SerializeField] private GridLayoutGroup _gridLayout;

    private void Update()
    {
        Vector2 newSize = new(_referenceSize.rect.width / 2, 160);
        _gridLayout.cellSize = newSize;
    }
}