using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BaseMaterialChanger))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _startQuantitySkeletons;
    [SerializeField] private TextMeshPro _quantity;
    [SerializeField] private List<Skeleton> _skeletonSquad;
    [SerializeField] private ChestSpawner _chestSpawner;
    [SerializeField] private Skeleton _skeleton;
    [SerializeField] private NewBaseBuilder _baseBuilder;

    private Queue<Chest> _chests = new Queue<Chest>();
    private int _quantityResources = 0;
    private BaseMaterialChanger _clickBase;

    private void Start()
    {
        _clickBase = GetComponent<BaseMaterialChanger>();
        
        for (int i = 0; i < _startQuantitySkeletons; i++)
        {
            GameObject skeleton = Instantiate(_skeleton.gameObject, gameObject.transform);
            Skeleton newSkeleton = skeleton.GetComponent<Skeleton>();
            _skeletonSquad.Add(newSkeleton);
        }
    }

    private void Update()
    {
        _quantity.text = _quantityResources.ToString();

        if (_clickBase.IsClicked)
        {
            _baseBuilder.CreateNewBase();
            SendSkeletonForNewBase(_baseBuilder.ReturnNewBase());
        }
        else
        {
            CreateNewSkeleton();
        }
 
        foreach (var skeleton in _skeletonSquad.Where(skeleton => skeleton.IsBusy == false))
        {
            FindChest();
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Skeleton skeleton))
        {
            Chest chest = skeleton.GetComponentInChildren<Chest>();

            if (chest != null)
            {
                _quantityResources++;
                Destroy(chest.gameObject);
                skeleton.GetFree();
            }

            if (skeleton.NewBase != null)
            {
                skeleton.NewBase.GetComponent<BaseMaterialChanger>().ChangeStatus();
                skeleton.NewBase.GetNewSkeleton(ref skeleton);
                skeleton.GetComponent<Transform>().SetParent(skeleton.NewBase.GetComponent<Transform>());
                _clickBase.ChangeStatus();
            }
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

    private void CreateNewSkeleton()
    {
        const int costNewSkeleton = 3;

        if (_quantityResources >= costNewSkeleton)
        {
            GameObject newSkeleton = Instantiate(_skeleton.gameObject, gameObject.transform);
            Skeleton skeleton = newSkeleton.GetComponent<Skeleton>();
            _skeletonSquad.Add(skeleton);
            _quantityResources -= costNewSkeleton;
        }
    }

    private void SendSkeletonForNewBase(Base newBase)
    {
        int quantityResourcesForCreateNewBase = 5;

        if (_quantityResources >= quantityResourcesForCreateNewBase)
        {
            Skeleton skeleton = _skeletonSquad.FirstOrDefault(skeleton => skeleton.IsBusy == false);

            skeleton?.GoToNewBase(ref newBase);

            // if (skeleton.GetComponent<SkeletonMovement>().IsSkeletonOnNewBase)
            // {
            //     _clickBase.ChangeStatus();
            //     newBase.GetComponent<BaseMaterialChanger>().ChangeStatus();
            //     newBase.GetNewSkeleton( skeleton);
            //     _quantityResources -= quantityResourcesForCreateNewBase;
            // }
        }
    }

    private void GetNewSkeleton(ref Skeleton skeleton)
    {
        _skeletonSquad.Add(skeleton);
        _quantityResources -= 5;
    }
}