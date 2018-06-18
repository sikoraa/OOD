using System;
using System.Collections.Generic;

namespace rpn
{
    class Program
    {
        static void Main(string[] args)
        {
            var programs = new List<string>
                {
                    "1",
                    "0 2 +",
                    "2 3 * 3 -",
                    "20 2 4^-",
                    "   13 7+5   1 -/   ",
                    "5 0 ~ ! +",
                    "\t3\n\t4\n\t+\n",
                    "0 2 - 3 ^",
                    "0 3 - 2 ^",
                    "3 8 - 0 2 - *",
                    "10 30 29 11 3 min 2 max",
                    "13 6 6 + 3 2 max 14 20 4 min",
                    "0 3 - ! ",
                    "13 0 3 - ^",
                    "2147483647",
                };

            Calculator calc = new Calculator();

            foreach (var p in programs)
            {
                Console.WriteLine(p);

                try
                {
                    Console.WriteLine("RESULT: " + calc.Calculate(p));
                }
                catch
                {
                    Console.WriteLine("NO RESULT");
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }

        class Calculator
        {
            Stack<int> memory;
            Tokenizer tokenizer;

            public Calculator()
            {
                memory = new Stack<int>();
                tokenizer = new Tokenizer();
            }

            public int Calculate(string expression)
            {
                List<IExpression> tokens = tokenizer.Parse(expression);

                foreach (var t in tokens)
                {
                    t.Calculate(memory);
                }

                return memory.Pop();
            }

        }

        class Tokenizer
        {
            public List<IExpression> Parse(string expression)
            {
                List<IExpression> tokens = new List<IExpression>();
                Dictionary<string, IExpression> d = new Dictionary<string, IExpression>();
                // dodawanie operatorow, potem sa szukane w slowniku po stringu
                {
                    d.Add("+", new Add());
                    d.Add("-", new Substract());
                    d.Add("*", new Multiply());
                    d.Add("/", new Divide());
                    d.Add("^", new Power());
                    d.Add("~", new Not());
                    d.Add("!", new Factorial());
                    d.Add("min", new Min());
                    d.Add("max", new Max());
                }

            expression.TrimStart();
            string[] s = expression.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            Stack<int> memory = new Stack<int>();
            int maxlen = 0; // dlugosc najdluzszego operatora, potrzebne przy wykrywaniu niezaimplementowanych operatorow
            List<string> keys = new List<string>(); // to w sumiie niepotrzebne, ale wykryje tu najdluzszy operator
            foreach (var k in d.Keys)
            {
                keys.Add(k);
                if (k.Length > maxlen) maxlen = k.Length; // w maxlen trzymamy najdluzsza nazwe operatora
            }

            for (int i = 0; i < s.Length; ++i)
            {
                    int p;
                    if (int.TryParse(s[i], out p)) // string jest liczba
                        tokens.Add(new Number(p));
                    else // albo sam symbol, albo liczba sklejona z symbolem
                    {
                        string ss = s[i];
                        IExpression tmp;
                        if (d.TryGetValue(ss, out tmp)) // wyszukanie samego symbolu w slowniku
                            tokens.Add(tmp);
                        else // nie sam symbol, moze liczby z symbolami, moze sklejone symbole
                        {
                        string tmp2 = ""; // tymczasowy string na liczbe
                        string tmp3 = ""; // tymczasowy string na operator
                        int tmpint = 0; // potrzebne do TryParse
                        while (ss.Length > 0)
                        {
                            if (int.TryParse(ss[0].ToString(), out tmpint)) // znak jest liczba
                            {
                                
                                tmp2 += ss[0];
                                ss = ss.Substring(1);
                                if (ss.Length == 0) // jesli konczysie string i wykryto liczbe, to trzeba ja dodac
                                {
                                    tokens.Add(new Number(int.Parse(tmp2)));
                                    tmp2 = ""; //
                                }
                            }
                            else
                            {
                                if (tmp2 != "") // jesli wykryto liczbe to  wyzeruj wykrycie liczby i zapisz ta liczbe
                                {
                                    tokens.Add(new Number(int.Parse(tmp2)));
                                    tmp2 = ""; //                                   
                                }
                                tmp3 += ss[0];
                                if (d.TryGetValue(tmp3, out tmp))
                                {
                                    tokens.Add(tmp);
                                    tmp3 = "";
                                }
                                else if (tmp3.Length > maxlen) // operator dluzszy niz jakikolwiek znany, odrzucic go
                                {
                                    tmp3 = "";
                                }
                                ss = ss.Substring(1);
                            }
                        }

                        }
                    
                    }   
            }
                //TODO create tokens

                return tokens;
            }


        }

        interface IExpression
        {
            void Calculate(Stack<int> memory);
        }

        class Number : IExpression
        {
            int value;
            public Number(int _value) { value = _value; }
            public void Calculate(Stack<int> memory)
            {
                memory.Push(value);
            }
        }

        class Add : IExpression
        {
            public void Calculate(Stack<int> memory)
            {
                int a = memory.Pop();
                int b = memory.Pop();
                memory.Push(a + b);
            }
        }

        class Substract : IExpression
        {
            public void Calculate(Stack<int> memory)
            {
                int b = memory.Pop();
                int a = memory.Pop();
                memory.Push(a - b);
            }
        }

        class Divide : IExpression
        {
            public void Calculate(Stack<int> memory)
            {
                int b = memory.Pop();
                int a = memory.Pop();
                memory.Push(a / b);
            }
        }

        class Multiply : IExpression
        {
            public void Calculate(Stack<int> memory)
            {
                memory.Push(memory.Pop() * memory.Pop());
            }
        }

        class lessThanZero : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return (y > 0);
            }

            public int GetHashCode(int obj)
            {
                return obj;
            }
        }

        class Power : IExpression
        {
            Dictionary<int, int> c = new Dictionary<int, int>(new lessThanZero());
            public Power()
            {
            }
            public void Calculate(Stack<int> memory)
            {
                int pow = memory.Pop();
                int a = memory.Pop();
                c.Add(pow, pow);
                int tmp;
                c.TryGetValue(pow, out tmp);
                c.Clear();
                memory.Push((int)Math.Pow(a, tmp));
            }
        }

        class equalsZero : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return (x == y);
            }

            public int GetHashCode(int obj)
            {
                return obj;
            }
        }

        class Not : IExpression
        {
            Dictionary<int, int> c = new Dictionary<int, int>(new equalsZero());
            public Not()
            {
                c.Add(0, 1);
            }
            public void Calculate(Stack<int> memory)
            {
                int a = memory.Pop();
                int tmp;
                c.TryGetValue(a, out tmp);
                memory.Push(tmp);
            }
        }

        class Factorial : IExpression
        {
            Dictionary<int, int> c = new Dictionary<int, int>(new lessThanZero());
            public void Calculate(Stack<int> memory)
            {
                int a = memory.Pop();
                c.Add(a, a);
                int tmp;
                c.TryGetValue(a, out tmp);
                int value = 1;
                for (int i = 1; i <= tmp; ++i)
                    value *= i;
                c.Clear();
                memory.Push(value);
            }
        }

        class Min : IExpression
        {
            int n;
            
            public Min() { }
            public void Calculate(Stack<int> memory)
            {
                n = memory.Pop();
                if (n <= 0) return;
                int min = memory.Pop();
                for (int i = 1; i < n; ++i)
                {
                    int tmp = memory.Pop();
                    if (tmp < min) min = tmp;
                }
                memory.Push(min);
            }
        }

        class Max : IExpression
        {
            int n;
            public Max() { }
            public void Calculate(Stack<int> memory)
            {
                n = memory.Pop();
                if (n <= 0) return;
                int max = memory.Pop();
                for (int i = 1; i < n; ++i)
                {
                    int tmp = memory.Pop();
                    if (tmp > max) max = tmp;
                }
                memory.Push(max);

            }
        }
    }


