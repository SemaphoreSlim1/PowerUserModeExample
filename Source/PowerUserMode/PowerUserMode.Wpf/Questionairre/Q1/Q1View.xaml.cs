namespace PowerUserMode.Wpf.Questionairre.Q1
{
    /// <summary>
    /// Interaction logic for Q1View.xaml
    /// </summary>
    public partial class Q1View
    {
        public Q1View(IQ1ViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
