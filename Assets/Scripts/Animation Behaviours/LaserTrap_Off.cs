using UnityEngine;

public class LaserTrap_Off : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<LaserBeam>().DeactivateBeam();
        AudioMaster.Instance.PlaySoundEffect("LaserOff");
    }
}
