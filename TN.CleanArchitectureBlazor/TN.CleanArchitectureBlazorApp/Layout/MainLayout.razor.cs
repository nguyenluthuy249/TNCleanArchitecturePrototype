﻿namespace TN.CleanArchitectureBlazorApp.Layout
{
    public partial class MainLayout
    {
        bool _drawerOpen = false;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
