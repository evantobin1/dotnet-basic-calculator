using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dotnet_basic_calculator
{
    public partial class Calculator : Form
    {
        private string output;          // The text that is displayed into the outputTextBox
        private bool isValidEquation;   // Whether the output text is a valid equation
        
        // Calculator constructor
        public Calculator()
        {
            output = "";
            isValidEquation = false;
            InitializeComponent();
        }

        // When a number, parentheses, or operation is pressed
        private void button_NumClick(object sender, EventArgs e)
        {
            String requestedButton = (sender as Button).Text;
            isValidEquation = checkEquationValid(output += requestedButton);
            buttonEqual.BackColor = isValidEquation ? Color.LightSeaGreen : Color.Gainsboro;
            outputTextBox.Text = output;
        }

        // When an equals sign is pressed
        private void button_Equals_sign_click(object sender, EventArgs e)
        {
            if(isValidEquation)
            {
                outputTextBox.Text = new DataTable().Compute(output, null).ToString();
                output = outputTextBox.Text;
            }
        }

        // When the clear is pressed
        private void buttonC_Click(object sender, EventArgs e)
        {
            output = "";
            outputTextBox.Text = output;
            buttonEqual.BackColor = Color.LightSeaGreen;

        }

        // When backspace is pressed
        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (output.Length == 0)
                return;
            output = output.Substring(0, output.Length - 1);
            isValidEquation = checkEquationValid(output);
            buttonEqual.BackColor = isValidEquation ? Color.LightSeaGreen : Color.Gainsboro;
            outputTextBox.Text = output;
        }

        private bool checkEquationValid(string equation)
        {
            try
            {
                // This leverages the .NET framework's builtin DataTable library, with its Compute method.
                // This can will read a string and operate on it as if it was a spreadsheet formula
                // i.e. Compute("AVG(SALARY) + 31000", null)
                new DataTable().Compute(equation, null).ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
