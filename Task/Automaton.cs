using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;

namespace Task
{
    internal class Automaton
    {
        private readonly string Directory = 
            Path.Combine("..\\..\\Output");

        private List<string> Alphabet;
        private List<string> States;
        private List<string> TerminalStates;
        private string[,] TransitionMatrix;
        private string StartState;

        public Automaton(List<string> sigma, List<string> states,
            string start, List<string> terminal, string[,] delta)
        {
            Alphabet = sigma;
            States = states;
            StartState = start;
            TerminalStates = terminal;
            TransitionMatrix = delta;
        }

        private Automaton() 
        {
            Alphabet = new();
            States = new();
            TerminalStates = new();
        }

        public Automaton CrossProduct(Automaton DFA)
        {
            Automaton product = new();
            product.StartState = StartState + DFA.StartState;

            List<string> alphabet = new(); 

            foreach (var liter in Alphabet)
                alphabet.Add(liter);
            
            foreach (var liter in DFA.Alphabet)
                alphabet.Add(liter);

            product.Alphabet = alphabet.Distinct().ToList();

            foreach (var stateSource in States)
                foreach (var stateCross in DFA.States)
                    product.States.Add(stateSource + stateCross);

            foreach (var terminalSource in TerminalStates)
                foreach (var terminalCross in DFA.TerminalStates)
                    product.TerminalStates.Add(terminalSource + terminalCross);

            product.TransitionMatrix = 
                new string[product.States.Count, product.Alphabet.Count];

            var shift = DFA.TransitionMatrix.GetLength(1);

            for (int i = 0; i < product.States.Count; i++)
                for (int j = 0; j < product.Alphabet.Count; j++)
                    product.TransitionMatrix[i, j] = 
                        TransitionMatrix[i / shift, j] + DFA.TransitionMatrix[i % shift, j];
            return product;
        }

        public Automaton Difference(Automaton DFA)
        {
            DFA.Negation();
            return CrossProduct(DFA);
        }

        public Automaton Negation()
        {
            Automaton negate = this;
            negate.TerminalStates = States;
            negate.States = TerminalStates;
            return negate;
        }

        public Automaton Union(Automaton DFA)
        {
            Automaton unit = CrossProduct(DFA);

            foreach (var terminalSource in TerminalStates)
                foreach (var stateUnit in DFA.States)
                    unit.TerminalStates.Add(terminalSource + stateUnit);

            foreach (var stateSource in States)
                foreach (var terminalUnit in DFA.TerminalStates)
                    unit.TerminalStates.Add(terminalUnit + stateSource);

            return unit;
        }

        public void CreateDOT(string fileName)
        {
            var path = Path.Combine(Directory, fileName + ".dot");
            File.Create(path).Dispose();
            using StreamWriter stream = new(path);

            stream.Write("digraph {\r\n" +
                "\trankdir=LR;\r\n" +
                "\tnode [shape=point]; null; \r\n" +
                "\tnode [shape=doublecircle];");

            foreach (var state in TerminalStates)
                stream.Write($" {state}");
            stream.Write(";\r\n" +
                "\tnode [shape=circle];\r\n" +
                $"\tnull->{StartState};\r\n");

            for (int i = 0; i < TransitionMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < TransitionMatrix.GetLength(0); j++)
                {
                    stream.Write($"\t{States[j]}->{TransitionMatrix[j, i]} " +
                        $"[label=\"{Alphabet[i]}\"];\r\n");
                }    
            }
            stream.Write($"\tlabel =\"{fileName}\";\r\n}}");  
            stream.Close();
        }

        public bool CompileSVG(string fileName)
        {
            var path = Path.Combine(Directory, $"{fileName}.dot");
            if (File.Exists(path))
            {
                var compile = $"dot -Tsvg {path} > {path.Replace(".dot", ".svg")} & exit";
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/k " + compile,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process.WaitForExit();
                if (process.ExitCode.Equals(0))
                    return true;
            }
            return false;
        }

        public bool GenerateAutomaton(string modelName)
        {
            CreateDOT(modelName);
            if (CompileSVG(modelName))
                return true;
            return false;
        }
    }
}
