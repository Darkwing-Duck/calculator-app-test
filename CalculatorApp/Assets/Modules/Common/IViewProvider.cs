using UnityEngine;

namespace Modules.Common
{
    public interface IViewProvider<out TView> where TView : MonoBehaviour
    {
        TView Get(Transform parent = null);
        void Release();
    }
}