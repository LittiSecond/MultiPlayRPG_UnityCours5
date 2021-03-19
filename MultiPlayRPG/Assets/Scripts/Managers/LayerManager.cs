using UnityEngine;

namespace MultiPlayRPG
{
    public static class LayerManager
    {
        public static LayerMask GetLayerMask(params Layers[] layers)
        {
            LayerMask layerMask = 0;

            if (layers != null)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    layerMask += 1 << (int)layers[i];
                }
            }

            return layerMask;
        }
    }
}
