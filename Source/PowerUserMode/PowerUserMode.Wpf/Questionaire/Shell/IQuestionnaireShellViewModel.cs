using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Shell
{
    public interface IQuestionnaireShellViewModel
    {
        ICommand AdvanceNextCommand { get; }
        ICommand AdvancePreviousCommand { get; }
    }
}
