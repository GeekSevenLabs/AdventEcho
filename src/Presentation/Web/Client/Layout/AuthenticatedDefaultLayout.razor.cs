using Microsoft.AspNetCore.Components;

namespace AdventEcho.Presentation.Web.Server.Client.Layout;

public partial class AuthenticatedDefaultLayout : LayoutComponentBase
{
    private bool _drawerOpen = true;
    private void DrawerToggle() => _drawerOpen = !_drawerOpen;
}