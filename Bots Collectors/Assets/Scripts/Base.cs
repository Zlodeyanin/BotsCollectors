using System.Linq;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private TextMeshPro _quantityResourses;
    [SerializeField] private Skeleton[] _skeletonSquad;
    [SerializeField] private ChestSpawner _chestSpawner;
    
    private Chest[] _chests;
    private bool _isChestFind;
    private int _quantity = 0;

    private void Update()
    {
        FindChest();
        
        if (_isChestFind)
            _skeletonSquad.FirstOrDefault(skeleton => skeleton.IsBusy == false)?.GoToChest(_chests.FirstOrDefault(chest => chest.IsFree));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Chest chest))
        {
            _quantity++;
            _quantityResourses.text = _quantity.ToString();
            Skeleton skeletony = chest.GetComponentInParent<Skeleton>();
            skeletony.GetFree();
            Destroy(chest.gameObject);
        }
    }

    private void FindChest()
    {
        _chests = _chestSpawner.GetComponentsInChildren<Chest>();
        _isChestFind = _chests != null;
    }
}
