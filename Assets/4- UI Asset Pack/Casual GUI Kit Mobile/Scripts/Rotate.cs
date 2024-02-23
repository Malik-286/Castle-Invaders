using UnityEngine;

namespace hardartcore.CasualGUI
{
    public class Rotate : MonoBehaviour
    {
        public float TurnSpeed;

        void Update()
        {
            transform.Rotate(Vector3.forward, TurnSpeed * Time.deltaTime);
        }
    }
}
