using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveMoneyTextUpAnimation_script : MonoBehaviour
{
    //public static passiveMoneyTextUpAnimation_script Instance;

    public AnimationClip passiveMoneyTextUpAnimationClip;
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        //Instance = this;
        anim = GetComponent<Animation>();
    }


    public void PlayClipPassiveMoney()
    {
        anim.clip = passiveMoneyTextUpAnimationClip;
        anim.Play();
    }
}
