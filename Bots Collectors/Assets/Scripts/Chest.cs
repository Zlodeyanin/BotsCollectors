using UnityEngine;

public class Chest : MonoBehaviour
{
    public void SetSkeletonParent(Transform skeleton)
    {
        transform.SetParent(skeleton);
    }
}
