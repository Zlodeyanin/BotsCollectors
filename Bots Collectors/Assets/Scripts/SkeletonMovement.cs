using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Skeleton))]
public class SkeletonMovement : MonoBehaviour
{
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed;

    private Vector3 _relativePozition;
    private Skeleton _skeleton;
    private Animator _animator;
    private bool _isChestTaken;
    private Chest _chest;
    private Base _base;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _skeleton = GetComponent<Skeleton>();
        _base = GetComponentInParent<Base>();
    }

    private void Update()
    {
        _chest = _skeleton.Chest;

        if (_isChestTaken == false && _chest != null)
        {
            _relativePozition = _chest.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _chest.transform.position, _speed * Time.deltaTime);
            Quaternion rotation = Quaternion.LookRotation(_relativePozition, Vector3.up);
            transform.rotation = rotation;
            _animator.SetFloat(Speed, _speed);
        }
        else if(_isChestTaken)
        {
            transform.position = Vector3.MoveTowards(transform.position, _base.transform.position, _speed * Time.deltaTime);
            _relativePozition = _base.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(_relativePozition, Vector3.up);
            transform.rotation = rotation;
            _animator.SetFloat(Speed, _speed);
        }
        else
        {
            _animator.SetFloat(Speed, 0);
        }

        if (transform.position == _base.transform.position)
        {
            _isChestTaken = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Chest chest) && chest.IsTaken == false)
        {
            _isChestTaken = true;
            _chest.SetSkeletonParent(_skeleton.transform);
            _chest.ChangeIsTaken();
        }
    }
}