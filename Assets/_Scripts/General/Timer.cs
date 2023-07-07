using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.General
{
    public class Timer
    {
        public event Action OnTimerDone;

        private float startTime;
        private float pauseTime;
        private float continueTime;
        private float duration;
        private float targetTime;
        private float remainingTime;

        private bool isActive;
        private bool isPaused;

        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void StartTimer()
        {
            startTime = Time.time;
            targetTime = startTime + duration;
            isActive = true;
        }

        public void PauseTimer()
        {
            isPaused = true;
            pauseTime = Time.time;
            remainingTime = targetTime - pauseTime;
        }

        public void ContinueTimer()
        {
            isPaused = false;
            continueTime = Time.time;
            targetTime = continueTime + remainingTime;
        }

        public void StopTimer()
        {
            isActive = false;
        }

        private void TimerFinished()
        {
            StopTimer();
            OnTimerDone?.Invoke();
        }

        public void Tick()
        {
            if (!isActive || isPaused)
            {
                return;
            }

            if (Time.time >= targetTime)
            {
                TimerFinished();
            }
        }
    }
}
