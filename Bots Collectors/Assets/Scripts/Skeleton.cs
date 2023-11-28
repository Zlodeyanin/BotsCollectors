using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public bool IsBusy { get; private set; }
    public Chest Chest { get; private set; }

    private void Start()
    {
        IsBusy = false;
    }

    public void GoToChest(ref Chest chest)
    {
        if (chest != null)
        {
            IsBusy = true;
            Chest = chest;
        }
    }

    public void GetFree()
    {
        IsBusy = false;
    }
}