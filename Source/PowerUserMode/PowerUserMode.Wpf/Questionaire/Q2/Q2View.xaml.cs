namespace PowerUserMode.Wpf.Questionaire.Q2
{
    /// <summary>
    /// Interaction logic for Q2View.xaml
    /// </summary>
    public partial class Q2View 
    {
        public Q2View(IQ2ViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
