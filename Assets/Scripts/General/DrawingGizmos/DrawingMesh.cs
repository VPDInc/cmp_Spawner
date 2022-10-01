using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.DrawingGizmos
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class DrawingMesh : Drawing
    { 
        private MeshFilter _meshFilter;

        private void Awake() => 
            _meshFilter = GetComponent<MeshFilter>();

        protected override void DrawGizmos() => DrawGizmoMesh();
        protected override void DrawGizmosSelected() => DrawGizmoMesh();

        private void DrawGizmoMesh()
        {
            Gizmos.matrix = SelfTransform.localToWorldMatrix;
            Gizmos.DrawMesh(_meshFilter.mesh, Vector3.one);
        }
    }
}
