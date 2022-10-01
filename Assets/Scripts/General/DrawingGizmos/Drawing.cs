using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.DrawingGizmos
{
    public abstract class Drawing : MonoBehaviour
    {
        #region Inspector fields
        [Header("Settings")]
        [SerializeField] private DrawMode _mode;
        [SerializeField] private Color _gizmoColor;
        #endregion

        private Transform _transform;

        protected Transform SelfTransform => _transform ??= transform;

        #region Draw functions
        private void OnDrawGizmos()
        {
            if (_mode != DrawMode.Simple) return;
            
            SetGizmoColor(_gizmoColor);
            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (_mode != DrawMode.Selected) return;
            
            SetGizmoColor(_gizmoColor);
            DrawGizmosSelected();
        }

        protected abstract void DrawGizmos();
        protected abstract void DrawGizmosSelected();
        
        private static void SetGizmoColor(Color color) =>
            Gizmos.color = color;
        #endregion
    }
}
