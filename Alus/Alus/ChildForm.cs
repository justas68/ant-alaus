using System.Windows.Forms;

namespace Alus
{
    public class ChildForm : Form
    {
        public ChildForm()
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!e.Cancel)
            {
                MainForm.Instance.Show();
            }
        }
    }
}
