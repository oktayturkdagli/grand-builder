using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    public void SetTrigger(Animator animator, string parameter)
    {
        animator.SetTrigger(parameter);
    }
    
    public void SetBool(Animator animator, string parameter, bool value)
    {
        animator.SetBool(parameter, value);
    }
    
    public void SetFloat(Animator animator, string parameter, float value)
    {
        animator.SetFloat(parameter, value);
    }
    
    public void SetInteger(Animator animator, string parameter, int value)
    {
        animator.SetInteger(parameter, value);
    }
    
    public void PlayAnimation(Animation anim, Vector3 position = default)
    {
        anim.Play();
    }
}