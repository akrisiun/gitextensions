using GitUI;
using GitUI.CommandsDialogs;
using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace GitExtensions
{
    // using IWin32Window = GitUI.IWin32Window2;
    using IWin32Window = GitUI.IWin32Window;

    [CLSCompliant(false)]
    public class GitUICommandsCommit : GitUICommands
    {
        public GitUICommandsCommit(string dir) : base(dir)
        {
            // GetWorkingDir(args)
        }

        public event GitUIPluginInterfaces.GitUIEventHandler PreCommit2;
        public event GitUIPluginInterfaces.GitUIPostActionEventHandler PostCommit2;

        public override bool StartCommitDialog(IWin32Window owner, bool showOnlyWhenChanges)
        {
            // base.StartCommitDialog
            Func<bool> action = () =>
            {
                Thread thread = new Thread(
                    () => CreateForm(showOnlyWhenChanges)
                    );
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                return true;
            };

            return DoActionOnRepo(owner, true, false, PreCommit2, PostCommit2, action);
        }

        void CreateForm(bool showOnlyWhenChanges)
        {
            IWin32Window ownerNonModal = null;
            // FormCommit = null;
            var form = new GitUI.CommandsDialogs.Commit.FormCommit(this);

            form.ShowInTaskbar = true;
            form.Shown += FormCommit_Shown;
            form.FormClosing += FormCommit_FormClosing;

            // modal form
            // if (showOnlyWhenChanges)
            //  form.ShowDialogWhenChanges(ownerNonModal);
            //  form.Dispose();

            if (Application.OpenForms.Count == 0)
                Application.Run(form);
            else 
                form.Show(ownerNonModal);
        }

        private void FormCommit_Shown(object sender, EventArgs e)
        {
            var form = sender as Form;

            // FormCommit = new Tuple<IntPtr, CommandsDialogs.FormCommit>(form.Handle, form as FormCommit);
            ReadOnlyCollectionBase forms = Application.OpenForms;
            if (forms.Count > 0)
            {
                var num = forms.GetEnumerator();
                num.MoveNext();
                var mainForm = num.Current as Form;
                if (mainForm == null || !mainForm.Visible || mainForm.Equals(form))
                {
                    forms = null;
                }
                else
                {
                    // https://stackoverflow.com/questions/11995466/c-sharp-calling-form-show-from-another-thread
                    mainForm.Invoke(
                        (MethodInvoker)delegate ()
                        {
                            if (!form.InvokeRequired)
                                form.Show();
                            else
                                InvokeAction(form,
                                    () => form.Show());
                            
                            // GitCommands.Env.DoEvents();
                        }
                        );
                }
            }

            if (forms == null || forms.Count <= 1)
            {
                form.Focus();
                /*
                // Application.Run();
                System.InvalidOperationException: Starting a second message loop on a single thread is not a valid operation. Use Form.ShowDialog instead.
                at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
                at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
                at GitExtensions.GitUICommandsCommit.FormCommit_Shown(Object sender, EventArgs e) in D:\Beta\GitExt1\gitextensions\GitExt.Commit
                System.EventHandler.Invoke(Object sender, EventArgs e)
                at System.Windows.Forms.Form.OnShown(EventArgs e)
                at GitUI.GitExtensionsForm.OnShown(EventArgs e)
                */

                // GitCommands.Env.DoEvents();
            }

        }

        static void InvokeAction(Form form, Action act) => form.Invoke((Delegate)act);

        private void FormCommit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (FormCommit2 == sender)
            //    FormCommit = null;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [CLSCompliant(false)]
        public Tuple<IntPtr, FormCommit> FormCommit { get; set; }
        // public Tuple<IntPtr, FormCommit> FormCommit2 => FormCommit;

    }
}
