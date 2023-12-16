using System;
using UnityEngine;

public class NewBaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _newBase;
    
    private Base _createdBase;

    public void CreateNewBase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }

    private void Clicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (_createdBase != null)
        {
            Destroy(_createdBase.gameObject);
        }
        
        if (Physics.Raycast (ray, out hit))
        {
            GameObject newBase = Instantiate(_newBase.gameObject, hit.point, Quaternion.identity);
            _createdBase = newBase.GetComponent<Base>();
        }
    }

    public Base ReturnNewBase()
    {
        return _createdBase;
    }
}