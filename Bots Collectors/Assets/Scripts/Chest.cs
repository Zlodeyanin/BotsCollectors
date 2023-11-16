using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsTaken { get; private set; }
    public bool IsFree { get; private set; }

    private void Start()
    {
        IsFree = true;
        IsTaken = false;
    }

    public void ChangeIsTaken()
    {
        IsTaken= true;
    }

    public void ChangeIsFree()
    {
        IsFree = false;
    }

    public void SetSkeletonParent(Transform skeleton)
    {
        transform.SetParent(skeleton);
    }
}
