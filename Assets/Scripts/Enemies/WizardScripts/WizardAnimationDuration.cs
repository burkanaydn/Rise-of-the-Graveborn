using UnityEngine;

public class WizardAnimationDuration : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float GetAnimationLength(string animationName)
    {
        // Animator Controller'dan animasyonun süresini al
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        if (ac == null)
        {
            Debug.LogError("Animator Controller bulunamadý!");
            return 0f;
        }

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogError("Animasyon bulunamadý: " + animationName);
        return 0f;
    }
}