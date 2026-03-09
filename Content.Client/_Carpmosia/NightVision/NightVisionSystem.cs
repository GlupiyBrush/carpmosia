using Content.Client.Overlays;
using Content.Shared._Carpmosia.NightVision;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Robust.Client.Graphics;
using Robust.Client.Player;

namespace Content.Client._Carpmosia.NightVision;

public sealed class NightVisionSystem : EntitySystem
{
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly InventorySystem _inventory = default!;
    [Dependency] private readonly IOverlayManager _overlayMan = default!;
    private NightVisionOverlay? _overlay;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NightVisionComponent, AfterAutoHandleStateEvent>(OnHandleState);
    }

    private void OnHandleState(EntityUid uid, NightVisionComponent component, AfterAutoHandleStateEvent args)
    {
        var playerEnt = _playerManager.LocalEntity;
        if (component.Toggled == component.ToggledLocally)
            return;
        component.ToggledLocally = component.Toggled;
        Log.Info("Changed.");
    }
}
