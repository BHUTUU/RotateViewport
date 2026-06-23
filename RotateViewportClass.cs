using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
namespace RotateViewport
{
    public class RotateViewportClass
    {
        [CommandMethod("ROTATEVIEWPORT")]
        [CommandMethod("RV")]
        public void RotateViewport()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;
            Editor ed = doc.Editor;
            Database db = doc.Database;
            try
            {
                short cvport = Convert.ToInt16(Application.GetSystemVariable("CVPORT"));
                if (cvport <= 1)
                {
                    ed.WriteMessage("\nYou must be inside an active viewport.");
                    return;
                }
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    Viewport vp = tr.GetObject(ed.ActiveViewportId, OpenMode.ForRead) as Viewport;
                    if (vp == null)
                    {
                        ed.WriteMessage("\nUnable to obtain viewport.");
                        return;
                    }
                    if (vp.Locked)
                    {
                        ed.WriteMessage("\nViewport is locked. Unlock it first.");
                        return;
                    }
                    tr.Commit();
                }
                PromptPointResult p1 = ed.GetPoint("\nSpecify first point: ");
                if (p1.Status != PromptStatus.OK) return;
                PromptPointOptions ppo = new PromptPointOptions("\nSpecify second point: ");
                ppo.BasePoint = p1.Value;
                ppo.UseBasePoint = true;
                PromptPointResult p2 = ed.GetPoint(ppo);
                if (p2.Status != PromptStatus.OK) return;
                double angle = p1.Value.GetVectorTo(p2.Value).AngleOnPlane(new Plane(Point3d.Origin, Vector3d.ZAxis));
                ed.Command("_.DVIEW","","TW",$"-{(angle * 180.0 / Math.PI)}","");
                Application.SetSystemVariable("SNAPANG", angle);
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nError: {ex.Message}");
            }
        }
    }
}