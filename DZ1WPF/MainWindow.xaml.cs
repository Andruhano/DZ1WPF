using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ1WPF
{
    public partial class MainWindow : Window
    {
        private string _currentInput = "";
        private string _operation = "";
        private double _firstOperand;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string content = button.Content.ToString();

                if (double.TryParse(content, out _))
                {
                    _currentInput += content;
                    ResultTextBlock.Text = _currentInput;
                }
                else
                {
                    HandleOperation(content);
                }
            }
        }

        private void HandleOperation(string operation)
        {
            if (operation == "C")
            {
                _currentInput = "";
                _operation = "";
                _firstOperand = 0;
                ResultTextBlock.Text = "0";
            }
            else if (operation == "<")
            {
                if (!string.IsNullOrEmpty(_currentInput))
                {
                    _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                    ResultTextBlock.Text = string.IsNullOrEmpty(_currentInput) ? "0" : _currentInput;
                }
            }
            else if (operation == "=")
            {
                if (!string.IsNullOrEmpty(_operation) && double.TryParse(_currentInput, out double secondOperand))
                {
                    double result = 0;
                    switch (_operation)
                    {
                        case "+":
                            result = _firstOperand + secondOperand;
                            break;
                        case "-":
                            result = _firstOperand - secondOperand;
                            break;
                        case "*":
                            result = _firstOperand * secondOperand;
                            break;
                        case "/":
                            result = secondOperand != 0 ? _firstOperand / secondOperand : 0;
                            break;
                    }
                    ResultTextBlock.Text = result.ToString();
                    _currentInput = result.ToString();
                    _operation = "";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_currentInput) && double.TryParse(_currentInput, out _firstOperand))
                {
                    _operation = operation;
                    _currentInput = "";
                }
            }
        }
    }
}