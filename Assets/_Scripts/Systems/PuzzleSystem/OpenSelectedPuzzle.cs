using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Systems.PuzzleSystem
{
    public class OpenSelectedPuzzle : PuzzleSystem
    {
        [SerializeField] GameObject obstacle;

        protected override void Solve()
        {
            base.Solve();
            obstacle.SetActive(false);
        }
    }
}
