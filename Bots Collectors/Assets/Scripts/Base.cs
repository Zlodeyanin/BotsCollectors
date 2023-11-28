using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private TextMeshPro _quantityResourses;
    [SerializeField] private Skeleton[] _skeletonSquad;
    [SerializeField] private ChestSpawner _chestSpawner;
    
    private Queue<Chest> _chests = new Queue<Chest>();
    private int _quantity = 0;

    private void Update()
    {
        foreach (var skeleton in _skeletonSquad)
        {
            if (skeleton.IsBusy == false)
            {
                FindChest();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Chest chest))
        {
            _quantity++;
            _quantityResourses.text = _quantity.ToString();
            Skeleton skeleton = chest.GetComponentInParent<Skeleton>();
            skeleton.GetFree();
            Destroy(chest.gameObject);
        }
    }

    private void FindChest()
    {
        _chests.Enqueue(_chestSpawner.TransferChest());
        SendSkeleton();

        if (_chests.Count > 0)
        {
            _chests.Dequeue(); 
        }
    }

    private void SendSkeleton()
    {
        if (_chests.Peek() != null && _chests.Count > 0)
        {
            Chest chest = _chests.Peek();
            _skeletonSquad.FirstOrDefault(skeleton => skeleton.IsBusy == false)?.GoToChest(ref chest);
        }
    }
}