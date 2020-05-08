using UnityEngine;

public class LaserTrap_On : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<LaserBeam>().ActivateBeam();
    }
}
