using Avalonia.Controls;

namespace LightMvvm.Avalonia;

public static class LightMvvmViewHelper
{
    public static void SetSetupAsLightMvvmView(Control control, bool value)
    {
        if (control.GetValue(AttachedLightMvvmViewImpl.AttachedViewImplProperty) != null)
        {
            if (value == false)
                throw new InvalidOperationException();
            return;
        }

        control.SetValue(AttachedLightMvvmViewImpl.AttachedViewImplProperty, new AttachedLightMvvmViewImpl(control));
    }
}