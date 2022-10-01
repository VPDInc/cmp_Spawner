using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.DrawingGizmos
{
    public sealed class DrawingSphere : Drawing
    {
        [SerializeField] [Min(0)] private float _radius;
        
        protected override void DrawGizmos() => DrawGizmoSphere();
        protected override void DrawGizmosSelected() => DrawGizmoSphere();
        
        private void DrawGizmoSphere() =>
            Gizmos.DrawSphere(SelfTransform.position, _radius);
    }
}
