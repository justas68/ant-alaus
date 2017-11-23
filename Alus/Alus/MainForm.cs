using System;
using System.Windows.Forms;
using Unity;

namespace Alus
{
    public partial class MainForm : Form
    {
        private readonly EvaluationForm brv = new EvaluationForm();
        private readonly IUnityContainer _container;

        public MainForm()
        {
            InitializeComponent();
            _container = new UnityContainer();
            _container.RegisterType<IEmailValidator, EmailValidator>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brv.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new ImageRecognitionForm()).Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new LocationForm()).Show();
             this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void suggestions_Click(object sender, EventArgs e)
        {
            _container.Resolve<FeedbackForm>().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new StatisticalTableForm()).Show();
            this.Hide();
        }
    }
}
