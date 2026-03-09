using Content.Shared.Actions;

namespace Content.Shared._Carpmosia.NightVision;

public sealed class NightVisionSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NightVisionComponent, GetItemActionsEvent>(OnGetActions);
    }

    public void OnGetActions(EntityUid uid, NightVisionComponent component, GetItemActionsEvent args)
    {
        args.AddAction(ref component.ToggleActionEntity, component.ToggleAction);
        Dirty(uid, component);
    }
}
