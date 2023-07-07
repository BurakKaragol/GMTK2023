using UnityEngine;

namespace MrLule.General
{
    public class ParallaxScroller : MonoBehaviour
    {
        [Header("General:")]
        [SerializeField] private Vector2 parallaxEffect;
        [SerializeField] private Vector2 offset;

        private Transform cam;
        private float lengthX;
        private float lengthY;
        private float startPosX;
        private float startPosY;

        void Start()
        {
            cam = Camera.main.transform;
            startPosX = transform.position.x;
            startPosY = transform.position.y;
            lengthX = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
            lengthY = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        }

        void FixedUpdate()
        {
            float tempX = (cam.position.x * (1 - parallaxEffect.x));
            float tempY = (cam.position.y * (1 - parallaxEffect.y));
            float distX = (cam.position.x * parallaxEffect.x);
            float distY = (cam.position.y * parallaxEffect.y);

            transform.position = new Vector3(startPosX + distX + offset.x,
                startPosY + distY + offset.y, transform.position.z);

            if (tempX > startPosX + lengthX)
            {
                startPosX += lengthX;
            }
            else if (tempX < startPosX - lengthX)
            {
                startPosX -= lengthX;
            }

            if (tempY > startPosY + lengthY)
            {
                startPosY += lengthY;
            }
            else if (tempY < startPosY - lengthY)
            {
                startPosY -= lengthY;
            }
        }
    }
}