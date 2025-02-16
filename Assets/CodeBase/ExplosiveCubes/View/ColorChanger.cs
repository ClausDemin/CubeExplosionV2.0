using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View
{
    public class ColorChanger: MonoBehaviour
    {
        private MeshRenderer _renderer;

        private void Awake()
        {
            gameObject.TryGetComponent(out _renderer);
        }

        private void Start()
        {
            SelectRandomColor();
        }

        private void SelectRandomColor()
        {
            if (_renderer != null)
            {
                _renderer.material.color = 
                    new Color(UserUtils.GetRandomFloat(), UserUtils.GetRandomFloat(), UserUtils.GetRandomFloat());
            }
        }
    }
}
