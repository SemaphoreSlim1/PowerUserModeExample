namespace PowerUserMode.Wpf.Questionaire.Q3
{
    /// <summary>
    /// Interaction logic for Q3View.xaml
    /// </summary>
    public partial class Q3View
    {
        public Q3View(IQ3ViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
