using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SkeletonAnimator : MonoBehaviour
{
    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }
}