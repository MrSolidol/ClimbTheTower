using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SwapCalculation swapCalculation;

    private void OnEnable()
    {
        playerMove.eDisplayFlip.AddListener(OnFlipDisplay);
        playerMove.eDisplaySpring.AddListener(OnSpringDisplay);
        playerMove.eDisplayJump.AddListener(OnJumpDisplay);
        playerMove.eDisplayTrap.AddListener(OnWebbDisplay);
        playerMove.eDisplayGrounded.AddListener(OnGroundDisplay);
        playerMove.eDisplayRoofHit.AddListener(OnRoofHitDisplay);
        playerMove.eDisplayWallHit.AddListener(OnWallHitDisplay);
        playerMove.eDisplaySlide.AddListener(OnSlideDisplay);
    }

    private void OnDisable()
    {
        playerMove.eDisplayFlip.RemoveListener(OnFlipDisplay);
        playerMove.eDisplaySpring.RemoveListener(OnSpringDisplay);
        playerMove.eDisplayJump.RemoveListener(OnJumpDisplay);
        playerMove.eDisplayTrap.RemoveListener(OnWebbDisplay);
        playerMove.eDisplayGrounded.RemoveListener(OnGroundDisplay);
        playerMove.eDisplayRoofHit.RemoveListener(OnRoofHitDisplay);
        playerMove.eDisplayWallHit.RemoveListener(OnWallHitDisplay);
        playerMove.eDisplaySlide.RemoveListener(OnSlideDisplay);
    }

    public void HandlerFallAnimation() 
    {
        playerAnimator.SetBool("is_falling", false);
        playerMove.SetSwapActive(true);
    }

    private void OnFlipDisplay(bool flag) 
    {
        playerSprite.flipX = flag;
    }

    private void OnSpringDisplay(float value) 
    {
        playerAnimator.SetFloat("spring_value", value);
    }

    private void OnJumpDisplay()
    {
        playerAnimator.SetTrigger("jumped");
        playerAnimator.SetBool("is_standing", false);
    }

    private void OnGroundDisplay()
    {
        playerAnimator.SetBool("is_standing", true);
        if (playerAnimator.GetBool("is_falling")) 
        { playerMove.SetSwapActive(false); }
    }

    private void OnWallHitDisplay() 
    {
        playerAnimator.SetTrigger("wall_hitted");
        playerAnimator.SetBool("is_falling", true);
    }

    private void OnRoofHitDisplay() 
    {
        playerAnimator.SetTrigger("roof_hitted");
        playerAnimator.SetBool("is_falling", true);
    }

    private void OnSlideDisplay()
    {
        playerAnimator.SetTrigger("slipped");
    }

    private void OnFallingDisplay()
    {
        playerAnimator.SetBool("is_falling", true);
    }

    private void OnWebbDisplay()
    {
        playerAnimator.SetTrigger("webbed");
    }



}
