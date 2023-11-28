using UnityEngine;

[RequireComponent(typeof(Skeleton))]
[RequireComponent(typeof(SkeletonAnimator))]
[RequireComponent(typeof(ChestCollector))]
public class SkeletonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _relativePozition;
    private Skeleton _skeleton;
    private SkeletonAnimator _skeletonAnimator;
    private ChestCollector _takeChest;
    private bool _isChestTaken;
    private Chest _chest;
    private Base _base;

    private void Start()
    {
        _takeChest = GetComponent<ChestCollector>();
        _skeletonAnimator = GetComponent<SkeletonAnimator>();
        _skeleton = GetComponent<Skeleton>();
        _base = GetComponentInParent<Base>();
    }

    private void Update()
    {
        _chest = _skeleton.Chest;

        Move();
        Rotate();

        if (transform.position == _base.transform.position)
        {
            _takeChest.ChangeStatus();
        }
    }

    private void Move()
    {
        if (_takeChest.IsChestTaken == false && _chest != null)
        {
            _relativePozition = _chest.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _chest.transform.position, _speed * Time.deltaTime);
            _skeletonAnimator.ChangeAnimation(_speed);
        }
        else if (_takeChest.IsChestTaken)
        {
            _relativePozition = _base.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _base.transform.position, _speed * Time.deltaTime);
            _skeletonAnimator.ChangeAnimation(_speed);
        }
        else
        {
            _skeletonAnimator.ChangeAnimation(0);
        }
    }

    private void Rotate()
    {
        if (_relativePozition != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_relativePozition, Vector3.up);
            transform.rotation = rotation;
        }
    }
}