using Content.Shared._Carpmosia.NightVision;
using Robust.Shared.Audio.Systems;

namespace Content.Server._Carpmosia.NightVision;

public sealed class NightVisionSystem : EntitySystem
{
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NightVisionComponent, ToggleNightVisionActionEvent>(OnToggled);
    }

    private void OnToggled(EntityUid uid, NightVisionComponent component, ToggleNightVisionActionEvent args)
    {
        if (args.Handled)
            return;

        component.Toggled = !component.Toggled;
        if (component.Toggled && component.SoundOn != null)
            _audio.PlayPvs(component.SoundOn, uid);
        if (!component.Toggled && component.SoundOff != null)
            _audio.PlayPvs(component.SoundOff, uid);

        Log.Info("Toggled.");

        Dirty(uid, component);
        args.Handled = true;
    }
}
