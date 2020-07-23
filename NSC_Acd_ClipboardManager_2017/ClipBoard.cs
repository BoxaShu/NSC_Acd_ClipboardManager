using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App = Autodesk.AutoCAD.ApplicationServices;
using cad = Autodesk.AutoCAD.ApplicationServices.Application;
using Db = Autodesk.AutoCAD.DatabaseServices;
using Ed = Autodesk.AutoCAD.EditorInput;
using Gem = Autodesk.AutoCAD.Geometry;
using Rtm = Autodesk.AutoCAD.Runtime;

using Autodesk.AutoCAD.Windows;
using System.Diagnostics;

[assembly: Rtm.CommandClass(typeof(NSC_Acd_ClipboardManager.ClipBoard))]


/// <summary>
/// Реализация вот этой программы на C#
/// https://through-the-interface.typepad.com/through_the_interface/2009/09/clipboard-manager-octobers-adn-plugin-of-the-month-now-live-on-autodesk-labs.html
/// </summary>
namespace NSC_Acd_ClipboardManager
{
  public class ClipBoard : Rtm.IExtensionApplication
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private CbPalette _cp = null;
    public CbPalette ClipboardPalette
    {
      get
      {
        if (_cp == null)
        {
          _cp = new CbPalette();
        }
        return _cp;
      }
    }



    private static PaletteSet _ps = null;

    public static PaletteSet PaletteSet
    {
      get
      {
        if (_ps == null)
        {
          _ps = new PaletteSet("Clipboard", new System.Guid("ED8CDB2B-3281-4177-99BE-E1A46C3841AD"));
          _ps.Text = "Clipboard";
          _ps.DockEnabled = DockSides.Left & DockSides.Right & DockSides.None;
          _ps.MinimumSize = new System.Drawing.Size(200, 300);
          _ps.Size = new System.Drawing.Size(300, 500);
          _ps.Add("Clipboard", ClipboardPalette);
        }
        return _ps;
      }

    }



    public void Initialize()
    {
      DemandLoading.RegistryUpdate.RegisterForDemandLoading();
      //throw new NotImplementedException();
    }

    public void Terminate()
    {
      //throw new NotImplementedException();
    }



    [Rtm.CommandMethod("ADNPLUGINS", "CLIPBOARD", Rtm.CommandFlags.Modal)]
    public static void ShowClipboard()
    {
      PaletteSet.Visible = true;
    }


    [Rtm.CommandMethod("ADNPLUGINS", "REMOVECB", Rtm.CommandFlags.Modal)]
    public static void RemoveClipboard()
    {
      DemandLoading.RegistryUpdate.UnregisterForDemandLoading();
      Ed.Editor ed = App.Application.DocumentManager.MdiActiveDocument.Editor;
      ed.WriteMessage("\nThe Clipboard Manager will not be loaded  automatically in future editing sessions.");
    }



  }
}

