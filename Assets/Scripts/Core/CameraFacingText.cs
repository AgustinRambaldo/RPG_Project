using UnityEngine;

namespace RPG.Core
{
    public class CameraFacingText : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}