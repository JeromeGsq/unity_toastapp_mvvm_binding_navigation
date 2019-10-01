using UnityEngine;
using UnityWeld.Binding;

[CreateAssetMenu(menuName = "Unity Weld/Adapter options/Toastapp/NullToDefaultSpriteAdapterOptions")]
public class NullToDefaultSpriteAdapterOptions : AdapterOptions
{
    [SerializeField]
    private Sprite defaultSprite;

    public Sprite DefaultSprite { get => this.defaultSprite; set => this.defaultSprite = value; }
}
