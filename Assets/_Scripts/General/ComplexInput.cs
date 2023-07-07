using MrLule.ExtensionMethods;
using UnityEngine;

namespace MrLule.General
{
    public static class ComplexInput
    {
        static Camera mainCamera = Camera.main;

        #region Mouse Position
        public static Vector3 GetMouseWorldPositionWithoutZ()
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPositionWithoutZ2D();
            }
            else
            {
                return GetMouseWorldPositionWithoutZ3D();
            }
        }

        public static Vector3 GetMouseWorldPositionWithoutY()
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPositionWithoutY2D();
            }
            else
            {
                return GetMouseWorldPositionWithoutY3D();
            }
        }

        public static Vector3 GetMouseWorldPosition()
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPosition2D(Input.mousePosition, mainCamera);
            }
            else
            {
                return GetMouseWorldPosition3D(Input.mousePosition, mainCamera);
            }
        }

        public static Vector3 GetMouseWorldPositionWithoutZ(LayerMask layerMask)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPositionWithoutZ2D();
            }
            else
            {
                return GetMouseWorldPositionWithoutZ3D(layerMask);
            }
        }

        public static Vector3 GetMouseWorldPositionWithoutY(LayerMask layerMask)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPositionWithoutY2D();
            }
            else
            {
                return GetMouseWorldPositionWithoutY3D(layerMask);
            }
        }

        public static Vector3 GetMouseWorldPosition(LayerMask layerMask)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPosition2D(Input.mousePosition, mainCamera);
            }
            else
            {
                return GetMouseWorldPosition3D(Input.mousePosition, mainCamera, layerMask);
            }
        }

        public static Vector3 GetMouseWorldPosition(Camera camera)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPosition2D(Input.mousePosition, camera);
            }
            else
            {
                return GetMouseWorldPosition3D(Input.mousePosition, camera);
            }
        }

        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera camera)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPosition2D(Input.mousePosition, camera);
            }
            else
            {
                return GetMouseWorldPosition3D(Input.mousePosition, camera);
            }
        }

        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera camera, LayerMask layerMask)
        {
            if (mainCamera.orthographic)
            {
                return GetMouseWorldPosition2D(Input.mousePosition, camera);
            }
            else
            {
                return GetMouseWorldPosition3D(Input.mousePosition, camera, layerMask);
            }
        }
        #endregion

        #region Prespective Camera
        private static Vector3 GetMouseWorldPositionWithoutZ3D()
        {
            Vector3 vector = GetMouseWorldPosition3D(Input.mousePosition, mainCamera);
            return vector.SetZ(0);
        }

        private static Vector3 GetMouseWorldPositionWithoutY3D()
        {
            Vector3 vector = GetMouseWorldPosition3D(Input.mousePosition, mainCamera);
            return vector.SetY(0);
        }

        private static Vector3 GetMouseWorldPositionWithoutZ3D(LayerMask layerMask)
        {
            Vector3 vector = GetMouseWorldPosition3D(Input.mousePosition, mainCamera, layerMask);
            return vector.SetZ(0);
        }

        private static Vector3 GetMouseWorldPositionWithoutY3D(LayerMask layerMask)
        {
            Vector3 vector = GetMouseWorldPosition3D(Input.mousePosition, mainCamera, layerMask);
            return vector.SetY(0);
        }

        private static Vector3 GetMouseWorldPosition3D()
        {
            return GetMouseWorldPosition3D(Input.mousePosition, mainCamera);
        }

        private static Vector3 GetMouseWorldPosition3D(Camera camera)
        {
            return GetMouseWorldPosition3D(Input.mousePosition, camera);
        }

        private static Vector3 GetMouseWorldPosition3D(Camera camera, LayerMask layerMask)
        {
            return GetMouseWorldPosition3D(Input.mousePosition, camera, layerMask);
        }

        private static Vector3 GetMouseWorldPosition3D(Vector3 screenPosition, Camera camera, LayerMask layerMask = default(LayerMask))
        {
            Ray ray = camera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
        #endregion

        #region Orthographic Camera
        private static Vector3 GetMouseWorldPositionWithoutZ2D()
        {
            Vector3 vector = GetMouseWorldPosition2D(Input.mousePosition, mainCamera);
            return vector.SetZ(0);
        }

        private static Vector3 GetMouseWorldPositionWithoutY2D()
        {
            Vector3 vector = GetMouseWorldPosition2D(Input.mousePosition, mainCamera);
            return vector.SetY(0);
        }

        private static Vector3 GetMouseWorldPosition2D()
        {
            return GetMouseWorldPosition2D(Input.mousePosition, mainCamera);
        }

        private static Vector3 GetMouseWorldPosition2D(Camera camera)
        {
            return GetMouseWorldPosition2D(Input.mousePosition, camera);
        }

        private static Vector3 GetMouseWorldPosition2D(Vector3 screenPosition, Camera camera)
        {
            Vector3 vector = camera.ScreenToWorldPoint(screenPosition);
            return vector;
        }
        #endregion
    }
}
