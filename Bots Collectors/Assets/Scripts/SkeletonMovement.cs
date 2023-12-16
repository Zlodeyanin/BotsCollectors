using System;
using UnityEngine;

[RequireComponent(typeof(Skeleton))]
[RequireComponent(typeof(SkeletonAnimator))]
[RequireComponent(typeof(ChestCollector))]
public class SkeletonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    public bool IsSkeletonOnNewBase { get; private set; }

    private Skeleton _skeleton;
    private SkeletonAnimator _skeletonAnimator;
    private ChestCollector _takeChest;
    private bool _isChestTaken;
    private Chest _chest;
    private Base _base;
    private Base _newBase;

    private void Start()
    {
        IsSkeletonOnNewBase = false;
        _takeChest = GetComponent<ChestCollector>();
        _skeletonAnimator = GetComponent<SkeletonAnimator>();
        _skeleton = GetComponent<Skeleton>();
        _base = GetComponentInParent<Base>();
    }

    private void Update()
    {
        _chest = _skeleton.Chest;
        _newBase = _skeleton.NewBase;
        Move();

        if (transform.position == _base.transform.position)
        {
            _takeChest.ChangeStatus();
        }
    }

    private void Move()
    {
        if(_newBase != null)
        {
            transform.LookAt(_newBase.transform);
            transform.position = Vector3.MoveTowards(transform.position, _newBase.transform.position, _speed * Time.deltaTime);
            _skeletonAnimator.ChangeAnimation(_speed);

            if (transform.position == _newBase.transform.position)
            {
                IsSkeletonOnNewBase = true;
            }
        }
        else
        {
            if (_takeChest.IsChestTaken == false && _chest != null)
            {
                transform.LookAt(_chest.transform);
                transform.position = Vector3.MoveTowards(transform.position, _chest.transform.position, _speed * Time.deltaTime);
                _skeletonAnimator.ChangeAnimation(_speed);
            }
            else if (_takeChest.IsChestTaken)
            {
                transform.LookAt(_base.transform);
                transform.position = Vector3.MoveTowards(transform.position, _base.transform.position, _speed * Time.deltaTime);
                _skeletonAnimator.ChangeAnimation(_speed);
            }
            // else if (_newBase != null)
            // {
            //     transform.LookAt(_base.transform);
            //     transform.position = Vector3.MoveTowards(transform.position, _newBase.transform.position, _speed * Time.deltaTime);
            //     _skeletonAnimator.ChangeAnimation(_speed);
            // }
            else
            {
                _skeletonAnimator.ChangeAnimation(0);
            }
        }
    }
}