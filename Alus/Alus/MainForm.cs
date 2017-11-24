using System;
using System.Windows.Forms;
using Alus.Client;
using Alus.Core.Models;
using Unity;

namespace Alus
{
    public partial class MainForm : Form
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
        private readonly IUnityContainer _onlineContainer;
        private readonly IUnityContainer _offlineContainer;

        public MainForm()
        {
            InitializeComponent();
            var client = new AlusClient();
            _container = new UnityContainer();
            _container.RegisterType<IEmailValidator, EmailValidator>();
            _container.RegisterType<IColorPicker, ColorPicker>();

            _onlineContainer = _container.CreateChildContainer();
            _onlineContainer.RegisterInstance<IFeedbackSender>(new FeedbackSender(client));
            _onlineContainer.RegisterInstance<IBarContainer>(new BarWebServiceContainer(client));

            _offlineContainer = _container.CreateChildContainer();
            _offlineContainer.RegisterInstance<IFeedbackSender>(new FeedbackFileSender("feedback.txt"));
            _offlineContainer.RegisterInstance<IBarContainer>(new BarFileContainer("./../../../BarList.txt"));

        }

        public T Resolve<T>()
        {
            var container = modeCheckbox.Checked ? _onlineContainer : _offlineContainer;
            return container.Resolve<T>();
        }

        public T ResolveForm<T>() where T : Form
        {
            var t = Resolve<T>();
            t.Show();
            Hide();
            return t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResolveForm<EvaluationForm>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResolveForm<ImageRecognitionForm>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResolveForm<LocationForm>();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void suggestions_Click(object sender, EventArgs e)
        {
            ResolveForm<FeedbackForm>();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResolveForm<StatisticalTableForm>();
        }
    }
}
