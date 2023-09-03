using Avalonia;
using Avalonia.Controls;

namespace LightMvvm.Avalonia;

internal class DataTemplateHelper
{
    public static Control? ResolveViewFromApp(object model)
    {
        foreach (var template in Application.Current.DataTemplates)
        {
            if (template.Match(model))
                return template.Build(model);
        }

        return new ContentControl() { Content = model };
    }
}