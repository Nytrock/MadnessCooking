using UnityEngine;

public class IngredientPlantAnimation : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("leftCount", animator.GetInteger("leftCount") - 1);
    }
}
