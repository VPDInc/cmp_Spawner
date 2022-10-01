using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.DrawingGizmos
{
    public sealed class DrawingCube : Drawing
    {
        protected override void DrawGizmos() => DrawGizmoCube();
        protected override void DrawGizmosSelected() => DrawGizmoCube();

        private void DrawGizmoCube()
        {
            Gizmos.matrix = SelfTransform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}
