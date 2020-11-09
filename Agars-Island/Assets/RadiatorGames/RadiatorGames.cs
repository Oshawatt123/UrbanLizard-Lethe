using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadiatorGames
{
    namespace UI
    {
        namespace SwapCanvasGroup
        {
            public class GroupSwapper
            {
                public static void ShowCanvasGroup(CanvasGroup canvasGroup)
                {
                    canvasGroup.alpha = 1.0f;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }

                public static void HideCanvasGroup(CanvasGroup canvasGroup)
                {
                    canvasGroup.alpha = 0.0f;
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }
            }
        }
    }

    namespace Sanity
    {
        public class SanityTime
        {
            public static void SetTimeScale(float timeScale)
            {
                Time.timeScale = timeScale;
            }
        }
    }
}