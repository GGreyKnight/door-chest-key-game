using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [SerializeField] private Animator myAnimator = null;

    //[SerializeField] private string animName = null;

    private bool animationOncePlayed = false;

    public void playAnimation(string animName)
    {
        if(animationOncePlayed == false)
        {
            myAnimator.Play(animName, 0, 0.0f);
        }
        animationOncePlayed = true;
    }
}
