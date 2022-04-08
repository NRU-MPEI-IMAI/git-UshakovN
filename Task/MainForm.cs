using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Task
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void BtnMock_Click(object sender, EventArgs e)
        {
            Automaton Mock1 = new(
                new List<string> {
                    "a", "b"
                },
                new List<string> {
                    "q1", "q2", "q3", "q4"
                },
                "q1",
                new List<string> { 
                    "q4"
                },
                new string[,] {
                    { "q2", "q1" },
                    { "q3", "q2" },
                    { "q4", "q3" },
                    { "q4", "q4" }
                });

            Automaton Mock2 = new(
                new List<string> {
                    "a", "b"
                },
                new List<string> {
                    "p1", "p2"
                },
                "p1",
                new List<string> {
                    "p2"
                },
                new string[,] {
                    { "p1", "p2" },
                    { "p2", "p1" }
                });

            Automaton Mock = Mock1.CrossProduct(Mock2);
            if (Mock.GenerateAutomaton("Mock"))
                MessageBox.Show("Verify");
        }
    }
}
