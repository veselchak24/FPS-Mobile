using UnityEngine;
using System.Collections.Generic;

public class LightStend : MonoBehaviour
{
    private static PlayerMove player;
    private static Interact Interact;

    [SerializeField] private List<GameObject> upperPart;

    private new Animator animation;
    private bool isOpen = false;

    private static LightStend true_current = null;

    private void Start()
    {
        animation = GetComponent<Animator>();

        if (!player)
        {
            player = FindObjectOfType<PlayerMove>();
            Interact = FindObjectOfType<Interact>();
        }
    }

    private void Anim()
    {
        AnimatorStateInfo current_anim = animation.GetCurrentAnimatorStateInfo(0);
        if (current_anim.normalizedTime > 1 || current_anim.IsName("Idle"))
        {
            foreach (GameObject @object in upperPart)
            {
                var mesh = @object.GetComponent<MeshRenderer>();
                if (mesh.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.On)
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                else
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                print(mesh.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.On);
            }

            animation.SetTrigger("action");
            animation.SetBool("isOpen", isOpen);
            isOpen = !isOpen;
        }
    }

    private void Update()
    {
        if (true_current != this && true_current != null) return;
        if (Vector3.Distance(transform.position, player.transform.position) <= 5 && Mathf.Abs(Vector3.SignedAngle(transform.position - player.transform.position, player.transform.forward, Vector3.up)) < 65)
        {
            if (true_current == null)
                true_current = this;

            if (!Interact.gameObject.activeInHierarchy)
                Interact.gameObject.SetActive(true);

            if (Interact.isInteract())
                Anim();
        }
        else if (Interact.gameObject.activeInHierarchy)
        {
            Interact.gameObject.SetActive(false);
            true_current = null;
        }
    }
}
