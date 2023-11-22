using UnityEngine;

[RequireComponent(typeof(Skeleton))]
public class SkeletonTakeChest : MonoBehaviour
{
    public bool IsChestTaken { get; private set; }

    private Chest _chest;
    private Skeleton _skeleton;

    private void Start()
    {
        _skeleton = GetComponent<Skeleton>();
    }

    private void Update()
    {
        _chest = _skeleton.Chest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Chest chest) && chest.IsTaken == false)
        {
            IsChestTaken = true;
            _chest.SetSkeletonParent(_skeleton.transform);
            _chest.ChangeIsTaken();
        }
    }

    public void ChangeStatus()
    {
        if (IsChestTaken == true)
        {
            IsChestTaken = false;
        }
    }
}
