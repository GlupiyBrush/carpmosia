using System.Numerics;
using Content.Shared._Carpmosia.Overlays;
using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;

namespace Content.Client._Carpmosia.Overlays;

public sealed partial class NightVisionOverlay : Overlay
{
    private static readonly ProtoId<ShaderPrototype> Shader = "NightVision";

    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;
    public override bool RequestScreenTexture => true;
    private readonly ShaderInstance _nvShader;
    private readonly NightVisionOverlayComponent _component;

    public NightVisionOverlay(NightVisionOverlayComponent component)
    {
        IoCManager.InjectDependencies(this);
        _nvShader = _prototypeManager.Index(Shader).InstanceUnique();
        ZIndex = 9;
        _component = component;
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture == null || !_component.Toggled)
            return;

        var handle = args.WorldHandle;
        _nvShader.SetParameter("SCREEN_TEXTURE", ScreenTexture);
        _nvShader.SetParameter("LIGHT_TEXTURE", args.Viewport.LightRenderTarget.Texture);
        _nvShader.SetParameter("Tint", new Vector3(_component.Tint.R, _component.Tint.G, _component.Tint.B));
        handle.UseShader(_nvShader);
        handle.DrawRect(args.WorldBounds, Color.White);
        handle.UseShader(null);
    }
}
