using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Systems.PuzzleSystem
{
    public class PuzzleComponent : MonoBehaviour
    {
        public bool isActive = false;

        public void SetState(bool state)
        {
            isActive = state;
        }
    }
}
