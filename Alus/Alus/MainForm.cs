using System;
using Alus.Client;
using Alus.Core.Models;
using Unity;

namespace Alus
{
    public partial class MainForm : ChildForm
    {
        private static readonly Lazy<MainForm> instance = new Lazy<MainForm>(() => new MainForm());

        public static MainForm Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private readonly IUnityContainer _container;

        public MainForm()
        {
            InitializeComponent();
            _container = new UnityContainer();
            _container.RegisterType<IEmailValidator, EmailValidator>();
            _container.RegisterType<IColorPicker, ColorPicker>();
            _container.RegisterInstance<IFeedbackSender>(new FeedbackSender(new AlusClient()));
            _container.RegisterInstance<IBarContainer>(new BarFileContainer("./../../../BarList.txt"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _container.Resolve<EvaluationForm>().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new ImageRecognitionForm()).Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _container.Resolve<LocationForm>().Show();
             Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void suggestions_Click(object sender, EventArgs e)
        {
            _container.Resolve<FeedbackForm>().Show();
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _container.Resolve<StatisticalTableForm>().Show(); ;
            Hide();
        }
    }
}
