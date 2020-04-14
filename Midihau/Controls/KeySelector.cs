using System;
using System.Drawing;
using System.Windows.Forms;

namespace Midihau.Controls
{
    class KeySelector : Control
    {
        private bool isSelectingKey = false;

        public event EventHandler<KeySelectedEvent> OnKeySelected;

        public string SelectingText { get; set; } = "Press key...";

        public Keys? SelectedKeyCode {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return null;

                return (Keys)new KeysConverter().ConvertFromString(Text);
            }
            set
            {
                Text = new KeysConverter().ConvertToString((object)value).ToString();
            }
        }

        public string SelectedKeyCodeAsString => new KeysConverter().ConvertToString((object)Text).ToString();

        public KeySelector()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!isSelectingKey)
            {
                var keyCodeTxt = SelectedKeyCodeAsString;
                e.Graphics.DrawString(!string.IsNullOrEmpty(keyCodeTxt) ? keyCodeTxt : "(None)", Font, new SolidBrush(ForeColor), new PointF(0, 3));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            using (var pen = new Pen(Focused ? SystemColors.Highlight :  Color.FromArgb(255, 122, 122, 122)))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            if (isSelectingKey)
            {
                using (var foreBrush = new SolidBrush(ForeColor))
                using (var grayBrush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillRectangle(grayBrush, new Rectangle(1, 1, Width - 2, Height - 2));
                    e.Graphics.DrawString(SelectingText, Font, foreBrush, new PointF(0, 3));
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            isSelectingKey = true;
            Select();
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            isSelectingKey = true;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isSelectingKey = false;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (isSelectingKey)
            {
                SelectedKeyCode = e.KeyCode;
                isSelectingKey = false;

                OnKeySelected?.Invoke(this, new KeySelectedEvent() { Key = e.KeyCode });
            }

            Invalidate();
        }
    }
}
