﻿using System;
using UnityEngine;

namespace KerboKatz
{
  [KSPAddon(KSPAddon.Startup.EveryScene, false)]
  public partial class FPSViewer : KerboKatzBase
  {
    public FPSViewer()
    {
      modName = "FPSViewer";
      displayName = "FPS Viewer";
      tooltip = "Use left click to show/hide the fps overlay.\n Use right click to open the settings menu.";
      requiresUtilities = new Version(1, 2, 10);
    }

    protected override void Started()
    {
      currentSettings.load("SmallUtilities/FPSViewer", "FPSViewerSettings", modName);
      currentSettings.setDefault("anchorOptionSelected", "4");
      anchorOptionSelected = currentSettings.getInt("anchorOptionSelected");
      position.x = currentSettings.getFloat("fpsPosX");
      position.y = currentSettings.getFloat("fpsPosY");
      showMinFPS = currentSettings.getBool("showMinFPS");
      showMaxFPS = currentSettings.getBool("showMaxFPS");
      hideOnUIHidden = currentSettings.getBool("hideOnUIHidden");

      setIcon(Utilities.getTexture("icon", "SmallUtilities/FPSViewer/Textures"));
      setAppLauncherScenes(ApplicationLauncher.AppScenes.ALWAYS);
    }

    protected override void onToolbar()
    {
      if (Input.GetMouseButtonUp(0))
      {
        if (currentSettings.getBool("showFPS"))
        {
          currentSettings.set("showFPS", false);
        }
        else
        {
          currentSettings.set("showFPS", true);
        }
      }
      else if (Input.GetMouseButtonUp(1))
      {
        toggleSettingsWindow();
      }
    }

    private void toggleSettingsWindow()
    {
      if (currentSettings.getBool("showSettings"))
      {
        currentSettings.set("showSettings", false);
      }
      else
      {
        currentSettings.set("showSettings", true);
      }
    }

    protected override void beforeSaveOnDestroy()
    {
      if (currentSettings != null)
      {
        currentSettings.set("showSettings", false);
        currentSettings.set("changePosition", false);
        currentSettings.set("fpsPosX", position.x);
        currentSettings.set("fpsPosY", position.y);
      }
    }
  }
}