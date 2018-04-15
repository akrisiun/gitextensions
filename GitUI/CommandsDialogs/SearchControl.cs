using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GitCommands;

namespace GitUI.CommandsDialogs
{
    public partial class SearchControl<T> : UserControl, IDisposable where T : class
    {
        private readonly Func<string, IList<T>> getCandidates;
        private readonly Action<Size> _onSizeChanged;
        private AsyncLoader backgroundLoader = new AsyncLoader();
        private bool _isUpdatingTextFromCode = false;
        public event Action OnTextEntered;
        public event Action OnCancelled;

        public override string Text
        {

            get { return txtSearchBox.Text; }
            set { txtSearchBox.Text = value; }
        }

        public SearchControl(Func<string, IList<T>> getCandidates, Action<Size> onSizeChanged)
        {
            InitializeComponent();

            listBox1.Left = Left;
            textBox1.Select();

            listBoxSearchResult.Left = Left;
            txtSearchBox.Select();

            if (getCandidates == null)
            {
                throw new InvalidOperationException("getCandidates cannot be null");
            }
            this.getCandidates = getCandidates;
            _onSizeChanged = onSizeChanged;
            AutoFit();
        }

        public void CloseDropdown()
        {

            listBox1.Visible = false;
            listBoxSearchResult.Visible = false;
        }

        private void SearchForCandidates(IList<T> candidates)
        {

            var selectionStart = textBox1.SelectionStart;
            var selectionLength = textBox1.SelectionLength;
            listBox1.BeginUpdate();
            listBox1.Items.Clear();

            for (int i = 0; i < candidates.Count && i < 20; i++)
            {
                listBox1.Items.Add(candidates[i]);
            }

            listBox1.EndUpdate();
            if (candidates.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
            textBox1.SelectionStart = selectionStart;
            textBox1.SelectionLength = selectionLength;
            var selectionStart = txtSearchBox.SelectionStart;
            var selectionLength = txtSearchBox.SelectionLength;
            listBoxSearchResult.BeginUpdate();
            listBoxSearchResult.Items.Clear();

            for (int i = 0; i < candidates.Count && i < 20; i++)
            {
                listBoxSearchResult.Items.Add(candidates[i]);
            }

            listBoxSearchResult.EndUpdate();
            if (candidates.Count > 0)
            {
                listBoxSearchResult.SelectedIndex = 0;
            }
            txtSearchBox.SelectionStart = selectionStart;
            txtSearchBox.SelectionLength = selectionLength;

            AutoFit();
        }

        private void AutoFit()
        {

            if (listBox1.Items.Count == 0)
            {
                listBox1.Visible = false;
                return;
            }

            listBox1.Visible = true;

            int width = 300;

            using (Graphics g = listBox1.CreateGraphics())
            {
                for (int i1 = 0; i1 < listBox1.Items.Count; i1++)
                {
                    int itemWidth = Convert.ToInt32(g.MeasureString(Convert.ToString(listBox1.Items[i1]), listBox1.Font).Width);

            if (listBoxSearchResult.Items.Count == 0)
            {
                listBoxSearchResult.Visible = false;
                return;
            }

            listBoxSearchResult.Visible = true;

            int width = 300;

            using (Graphics g = listBoxSearchResult.CreateGraphics())
            {
                for (int i1 = 0; i1 < listBoxSearchResult.Items.Count; i1++)
                {
                    int itemWidth = Convert.ToInt32(g.MeasureString(Convert.ToString(listBoxSearchResult.Items[i1]), listBoxSearchResult.Font).Width);

                    width = Math.Max(width, itemWidth);
                }
            }


            listBox1.Width = width;
            listBox1.Height = Math.Min(800, listBox1.Font.Height * (listBox1.Items.Count + 1));

            Width = listBox1.Width;
            _onSizeChanged(new Size(width + Margin.Right + Margin.Left,
                listBox1.Height + textBox1.Height));
            var txtBoxOnScreen = PointToScreen(textBox1.Location + new Size(0, textBox1.Height));

            if (ParentForm != null && !ParentForm.Controls.Contains(listBox1))
            {
                ParentForm.Controls.Add(listBox1);
                var listBoxLocationOnScreen = txtBoxOnScreen;
                listBox1.Location = ParentForm.PointToClient(listBoxLocationOnScreen);
            }
            listBox1.BringToFront();

            listBoxSearchResult.Width = width;
            listBoxSearchResult.Height = Math.Min(800, listBoxSearchResult.Font.Height * (listBoxSearchResult.Items.Count + 1));

            Width = listBoxSearchResult.Width;
            _onSizeChanged(new Size(width + Margin.Right + Margin.Left,
                listBoxSearchResult.Height + txtSearchBox.Height));
            var txtBoxOnScreen = PointToScreen(txtSearchBox.Location + new Size(0, txtSearchBox.Height));

            if (ParentForm != null && !ParentForm.Controls.Contains(listBoxSearchResult))
            {
                ParentForm.Controls.Add(listBoxSearchResult);
                var listBoxLocationOnScreen = txtBoxOnScreen;
                listBoxSearchResult.Location = ParentForm.PointToClient(listBoxLocationOnScreen);
            }
            listBoxSearchResult.BringToFront();

        }

        public T SelectedItem
        {

            get { return (T)listBox1.SelectedItem; }

            get { return (T)listBoxSearchResult.SelectedItem; }

        }

        void IDisposable.Dispose()
        {
            if (backgroundLoader != null)
            {
                backgroundLoader.Cancel();
            }
            backgroundLoader = null;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {}

        private void txtSearchBox_TextChange(object sender, EventArgs e)
        {
            OnTextChanged(e);
            if (_isUpdatingTextFromCode)
            {
                _isUpdatingTextFromCode = false;
                return;
            }

            string  _selectedText = textBox1.Text;
            // string  _selectedText = txtSearchBox.Text;
            backgroundLoader.Load(() => getCandidates(_selectedText), SearchForCandidates);
        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        // private void txtSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ItemSelectedFromList();
            }
            else if (e.KeyCode == Keys.Escape)
            {

                listBox1.SelectedItem = null;
                listBox1.Visible = false;
                listBoxSearchResult.SelectedItem = null;
                listBoxSearchResult.Visible = false;

                e.SuppressKeyPress = true;
                if (OnCancelled != null)
                {
                    OnCancelled();
                }
            }
        }

        private void ItemSelectedFromList()
        {
            _isUpdatingTextFromCode = true;

            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
            listBox1.Visible = false;
            if (listBoxSearchResult.SelectedItem != null)
            {
                txtSearchBox.Text = listBoxSearchResult.SelectedItem.ToString();
            }
            listBoxSearchResult.Visible = false;

            if (OnTextEntered != null)
            {
                OnTextEntered();
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (listBox1.Items.Count > 1)
                {
                    listBox1.SelectedIndex = (listBox1.SelectedIndex + 1) % listBox1.Items.Count;
                    // listBoxSearchResult.SelectedIndex = (listBoxSearchResult.SelectedIndex + 1) % listBoxSearchResult.Items.Count;

                    e.SuppressKeyPress = true;
                }
            }

            if (e.KeyCode == Keys.Up)
            {

                if (listBox1.Items.Count > 1)
                {
                    var newSelectedIndex = listBox1.SelectedIndex - 1;
                    if (newSelectedIndex < 0)
                        newSelectedIndex = listBox1.Items.Count - 1;

                    listBox1.SelectedIndex = newSelectedIndex;
                    // listBoxSearchResult.SelectedIndex = newSelectedIndex;

                    e.SuppressKeyPress = true;
                }
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                e.SuppressKeyPress = true;
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        // private void listBoxSearchResult_DoubleClick(object sender, EventArgs e)
        {
            ItemSelectedFromList();
        }
    }
}
