using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovered;
    [SerializeField] private AudioClip hoverSound;
    // Start is called before the first frame update

    void OnEnable()
    {
        this.transform.gameObject.transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        SoundFXManager.instance.playSoundFxClip(hoverSound, transform, 1f);
        this.transform.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        SoundFXManager.instance.playSoundFxClip(hoverSound, transform, 1f);
        this.transform.gameObject.transform.localScale = Vector3.one;
    }


}
