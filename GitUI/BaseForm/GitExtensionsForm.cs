using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Settings = GitCommands.AppSettings;
#if !__MonoCS__
using Microsoft.WindowsAPICodePack.Taskbar;
#endif

namespace GitUI
{
    
    /// <summary>Base class for a Git Extensions <see cref="Form"/>.
    /// <remarks>
    /// Includes support for font, hotkey, icon, translation, and position restore.
    /// </remarks></summary>
    public class GitExtensionsForm2 : GitExtensionsFormBase, IWin32Window
    {
        #region ctor

        // internal
        //public static Icon ApplicationIcon = GetApplicationIcon(Settings.IconStyle, Settings.IconColor);

        /// <summary>indicates whether the <see cref="Form"/>'s position will be restored</summary>
        readonly bool _enablePositionRestore;

        /// <summary>Creates a new <see cref="GitExtensionsForm"/> without position restore.</summary>
        public GitExtensionsForm2()
            : this(false)
        {
        }

        /// <summary>Creates a new <see cref="GitExtensionsForm"/> indicating position restore.</summary>
        /// <param name="enablePositionRestore">Indicates whether the <see cref="Form"/>'s position
        /// will be restored upon being re-opened.</param>
        public GitExtensionsForm2(bool enablePositionRestore)
            : base()
        {
            _enablePositionRestore = enablePositionRestore;

            //Icon = ApplicationIcon;
            FormClosing += GitExtensionsForm_FormClosing;

            var cancelButton = new Button();
            cancelButton.Click += CancelButtonClick;

            CancelButton = cancelButton;
        }

        public virtual void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        void GitExtensionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_enablePositionRestore)
                SavePosition(GetType().Name);

#if !__MonoCS__
            if (GitCommands.Utils.EnvUtils.RunningOnWindows() && TaskbarManager.IsPlatformSupported)
            {
                try
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
                catch (InvalidOperationException) { }
            }
#endif
        }

#if NOTRANS
        public virtual void Translate() { }
#endif
        #endregion

        #region icon

        /// <summary>Specifies a Git Extensions' color index.</summary>
        protected enum ColorIndex
        {
            Default,
            Blue,
            Green,
            LightBlue,
            Purple,
            Red,
            Yellow,
            Unknown = -1
        }

        #endregion icon

        #region Load, Shown

        public bool IsSkinLoaded { get; protected set; }

        // override
        public  void SetSkin()
        {
            if (!IsSkinLoaded) {
                //base.SetSkin();
                IsSkinLoaded = true;
            }
        }

        /// <summary>Sets <see cref="AutoScaleMode"/>,
        /// restores position, raises the <see cref="Form.Load"/> event,
        /// and .
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetSkin();

            if (_enablePositionRestore)
                RestorePosition(GetType().Name);

            //if (!CheckComponent(this))
            //    OnRuntimeLoad(e);
        }

        public bool IsShown { get; protected set; }

        protected override void OnShown(EventArgs e)
        {
            if (!CheckComponent(this))
                OnRuntimeLoad(e);

            base.OnShown(e);

            IsShown = true;
        }

        /// <summary>Invoked at runtime during the <see cref="OnLoad"/> method.</summary>
        protected virtual void OnRuntimeLoad(EventArgs e)
        {

        }

        #endregion Load, Shown

        #region Position

        private bool _windowCentred;

        /// <summary>
        ///   Restores the position of a form from the user settings. Does
        ///   nothing if there is no entry for the form in the settings, or the
        ///   setting would be invisible on the current display configuration.
        /// </summary>
        /// <param name = "name">The name to use when looking up the position in
        ///   the settings</param>
        private void RestorePosition(String name)
        {
            if (!Visible ||
                WindowState == FormWindowState.Minimized)
                return;

            _windowCentred = (StartPosition == FormStartPosition.CenterParent);

            var position = LookupWindowPosition(name);

            if (position == null)
                return;

            StartPosition = FormStartPosition.Manual;
            if (FormBorderStyle == FormBorderStyle.Sizable ||
                FormBorderStyle == FormBorderStyle.SizableToolWindow)
                Size = position.Rect.Size;
            if (Owner == null || !_windowCentred)
            {
                Point location = position.Rect.Location;
                Rectangle? rect = FindWindowScreen(location);
                if (rect != null)
                    location.Y = rect.Value.Y;
                DesktopLocation = location;
            }
            else
            {
                // Calculate location for modal form with parent
                Location = new Point(Owner.Left + Owner.Width / 2 - Width / 2,
                    Math.Max(0, Owner.Top + Owner.Height / 2 - Height / 2));
            }
            if(WindowState != position.State)
                WindowState = position.State;
        }

        static Rectangle? FindWindowScreen(Point location)
        {
            SortedDictionary<float, Rectangle> distance = new SortedDictionary<float, Rectangle>();
            foreach (var rect in (from screen in Screen.AllScreens
                                  select screen.WorkingArea))
            {
                if (rect.Contains(location) && !distance.ContainsKey(0.0f))
                    return null; // title in screen

                int midPointX = (rect.X + rect.Width / 2);
                int midPointY = (rect.Y + rect.Height / 2);
                float d = (float)Math.Sqrt((location.X - midPointX) * (location.X - midPointX) +
                    (location.Y - midPointY) * (location.Y - midPointY));
                distance.Add(d, rect);
            }
            if (distance.Count > 0)
            {
                return distance.First().Value;
            }
            else
            {
                return null;
            }
        }

        private static WindowPositionList _windowPositionList;
        /// <summary>
        ///   Save the position of a form to the user settings. Hides the window
        ///   as a side-effect.
        /// </summary>
        /// <param name = "name">The name to use when writing the position to the
        ///   settings</param>
        private void SavePosition(String name)
        {
            try
            {
                var rectangle =
                    WindowState == FormWindowState.Normal
                        ? DesktopBounds
                        : RestoreBounds;

                var formWindowState =
                    WindowState == FormWindowState.Maximized
                        ? FormWindowState.Maximized
                        : FormWindowState.Normal;

                // Write to the user settings:
                if (_windowPositionList == null)
                    _windowPositionList = WindowPositionList.Load(); 
                WindowPosition windowPosition = _windowPositionList.Get(name);
                // Don't save location when we center modal form
                if (windowPosition != null && Owner != null && _windowCentred)
                {
                    if (rectangle.Width <= windowPosition.Rect.Width && rectangle.Height <= windowPosition.Rect.Height)
                        rectangle.Location = windowPosition.Rect.Location;
                }

                //var position = new WindowPosition(rectangle, formWindowState, name);
                //_windowPositionList.AddOrUpdate(position);
                //_windowPositionList.Save();
            }
            catch (Exception)
            {
                //TODO: howto restore a corrupted config?
            }
        }

        /// <summary>
        ///   Looks up a window in the user settings and returns its saved position.
        /// </summary>
        /// <param name = "name">The name.</param>
        /// <returns>
        ///   The saved window position if it exists. Null if the entry
        ///   doesn't exist, or would not be visible on any screen in the user's
        ///   current display setup.
        /// </returns>
        private static WindowPosition LookupWindowPosition(String name)
        {
            try
            {
                if (_windowPositionList == null)
                    _windowPositionList = WindowPositionList.Load();
                if (_windowPositionList == null)
                {
                    return null;
                }

                var position = _windowPositionList.Get(name);
                if (position == null || position.Rect.IsEmpty)
                    return null;

                if (Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(position.Rect)))
                {
                    return position;
                }
            }
            catch (Exception)
            {
                //TODO: howto restore a corrupted config?
            }

            return null;
        }

        #endregion
    }
}
