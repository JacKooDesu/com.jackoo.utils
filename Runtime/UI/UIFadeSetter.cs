using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Jackoo.Utils.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeSetter : MonoBehaviour
    {
        public EventTrigger trigger;
        public bool fadeIn;
        public bool fadeOut;

        CanvasGroup canvasGroup;
        bool originBlockRaycast;

        public bool bindSound = true;

        public bool hideAtStart = true;

        bool status;
        public bool Status
        {
            private set
            {
                if (originBlockRaycast)
                    canvasGroup.blocksRaycasts = value;
                status = value;
            }
            get => status;
        }

        private void OnEnable()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            originBlockRaycast = canvasGroup.blocksRaycasts;

            if (bindSound)
                BindButton();

            if (hideAtStart)
            {
                canvasGroup.alpha = 0;
                Status = false;
            }
            else
            {
                Status = true;
            }
        }

        private void OnDisable()
        {
            CoroutineUtility.Manager.Singleton.Stop(GetInstanceID().ToString());
        }

        public void TurnOn()
        {
            if (status)
                return;

            if (fadeIn)
            {
                CoroutineUtility.Manager.Singleton.Add((FadingIn()), GetInstanceID().ToString());
            }
        }

        public void TurnOff()
        {
            if (!status)
                return;
            if (fadeOut)
            {
                CoroutineUtility.Manager.Singleton.Add(FadingOut(), GetInstanceID().ToString());
            }
        }

        void BindButton()
        {
            var clickables = GetComponentsInChildren<IPointerClickHandler>();

            // WIP
            foreach (var b in clickables)
            {
                // GameHandler.Singleton.BindEvent(
                //     b.gameObject,
                //     EventTriggerType.PointerEnter,
                //     delegate
                //     {
                //         JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.select);
                //     });

                // GameHandler.Singleton.BindEvent(
                //     b.gameObject,
                //     EventTriggerType.PointerDown,
                //     delegate
                //     {
                //         JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.hover);
                //     });
            }
        }

        IEnumerator FadingIn()
        {
            Status = true;
            while (Mathf.Abs(canvasGroup.alpha - 1) > 0.01f && Status)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, .1f);
                yield return null;
            }
            canvasGroup.alpha = 1;
        }

        IEnumerator FadingOut()
        {
            Status = false;
            while (Mathf.Abs(canvasGroup.alpha - 0) > 0.01f && !Status)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, .1f);
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        public IEnumerator WaitStatusChange(UnityAction action, bool status)
        {
            while (Status != status)
            {
                yield return null;
            }
            // Debug.Log("bool change");
            action.Invoke();
        }
    }

}
