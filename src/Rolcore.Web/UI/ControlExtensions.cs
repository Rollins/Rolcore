using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace Rolcore.Web.UI
{
    /// <summary>
    /// Extensions for <see cref="Control"/>.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Finds a child control with the specified <see cref="Control.ID"/>.
        /// </summary>
        /// <param name="control">Specifies the control to search.</param>
        /// <param name="id">Specifies the ID of the control being searched for.</param>
        /// <returns>The control if found, or null if not found.</returns>
        public static Control FindControlRecursive(this Control control, string id)
        {
            // Pre-conditions
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("controlId cannot be null or empty", "id");
            if (control.ID == id) return control;

            Control result = control.FindControl(id);
            if (result == null)
            {
                foreach (Control childControl in control.Controls)
                {
                    result = FindControlRecursive(childControl, id);
                    if (result != null)
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Finds a child control with the specified <see cref="Control.ID"/>.
        /// </summary>
        /// <typeparam name="TControl">Specifies the expected <see cref="Type"/> of the control being 
        /// searched for.</typeparam>
        /// <param name="control">Specifies the control to search.</param>
        /// <param name="id">Specifies the ID of the control being searched for.</param>
        /// <returns>The control if found, or null if not found.</returns>
        public static TControl FindControlRecursive<TControl>(this Control control, string id) 
            where TControl : Control
        {
            return (TControl)control.FindControlRecursive(id);
        }

        /// <summary>
        /// Finds all child controls of the specified type.
        /// </summary>
        /// <typeparam name="TControl">Specifies the type of controls to find.</typeparam>
        /// <param name="parent">Specifies the parent control to search.</param>
        /// <param name="recurse">Specifies if the search is recursive.</param>
        /// <returns>An array of controls of the specified type.</returns>
        public static TControl[] ControlsOfType<TControl>(this Control parent, bool recurse)
            where TControl : System.Web.UI.Control
        {
            // Pre-conditions
            if (parent == null)
                throw new ArgumentNullException("parent", "parent is null.");

            // Non recursive
            if (!recurse)
                return parent.Controls.OfType<TControl>().ToArray();

            List<TControl> result = new List<TControl>();
            foreach (System.Web.UI.Control control in parent.Controls)
            {
                if (control is TControl)
                    result.Add((TControl)control);
                result.AddRange(ControlsOfType<TControl>(control, recurse));
            }

            return result.ToArray();
        }

        public static string RenderControlToString(this Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control", "control is null.");

            StringBuilder resultBuilder = new StringBuilder();
            using (StringWriter resultWriter = new StringWriter(resultBuilder))
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(resultWriter))
                    control.RenderControl(writer);

                return resultWriter.ToString();
            }
        }
    }
}
