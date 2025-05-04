using System.Collections;
using UnityEngine;

public class SpeedJumpBoost : MonoBehaviour
{
    public float speedBoostAmount = 3.0f;
    public float jumpBoostAmount = 2.0f;
    public float boostDuration = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                StartCoroutine(ApplyBoost(controller));
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ApplyBoost(CharacterController controller)
    {
        float originalSpeed = controller.speed;
        float originalJump = controller.jumpForce;

        controller.speed += speedBoostAmount;
        controller.jumpForce += jumpBoostAmount;

        yield return new WaitForSeconds(boostDuration);

        controller.speed = originalSpeed;
        controller.jumpForce = originalJump;
    }
}

