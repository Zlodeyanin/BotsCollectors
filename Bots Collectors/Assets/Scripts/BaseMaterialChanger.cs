using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMaterialChanger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Material _active;
    [SerializeField] private Material _onClick;

    public bool IsClicked { get; private set; }

    private Material _currentMaterial;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentMaterial == _active)
        {
            _currentMaterial = gameObject.GetComponent<MeshRenderer>().material = _onClick;
            IsClicked = true;
        }
        else
        {
            _currentMaterial = gameObject.GetComponent<MeshRenderer>().material = _active;
            IsClicked = false;
        }
    }

    public void ChangeStatus()
    {
        if (_currentMaterial == _onClick)
        {
            _currentMaterial = gameObject.GetComponent<MeshRenderer>().material = _active;
            IsClicked = false;
        }
        else
        {
            _currentMaterial = _currentMaterial = gameObject.GetComponent<MeshRenderer>().material = _onClick;
            IsClicked = true;
        }
    }
}
