namespace UnityWeld.Binding.Adapters
{
    using System;
    using UnityEngine;

    [Adapter(typeof(Sprite), typeof(Sprite), typeof(NullToDefaultSpriteAdapterOptions))]
    public class NullToDefaultSpriteAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            var value = (Sprite)valueIn;
            var option = (NullToDefaultSpriteAdapterOptions)options;
            return value ?? option.DefaultSprite;
        }
    }
}