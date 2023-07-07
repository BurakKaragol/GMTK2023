using MrLule.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace MrLule.Managers.TimelineMan
{
    public class TimelineManager : Manager
    {
        public PlayableDirector playableDirector;
        public UnityEvent OnTimelineFinished;

        private bool isTimelinePlaying = false;

        private void Start()
        {
            playableDirector.stopped += TimelineFinished;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isTimelinePlaying)
            {
                StartTimeline();
            }
        }

        public void StartTimeline()
        {
            playableDirector.Play();
            isTimelinePlaying = true;
        }

        private void TimelineFinished(PlayableDirector director)
        {
            if (director == playableDirector)
            {
                isTimelinePlaying = false;
                OnTimelineFinished?.Invoke();
            }
        }

        public override void OnEnable()
        {
            timelineManager = this;
        }

        public override void OnDisable()
        {
            timelineManager = null;
        }
    }
}
