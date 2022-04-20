using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<string> operation = new List<string>();
        double lastOperation = 0;
        string whatToDo = "";
        public Form1()
        {
            InitializeComponent();

            foreach (var button in this.Controls.OfType<Button>())
            {
                decimal Numb;   
                if (decimal.TryParse(button.Text, out Numb))
                {
                    button.Click += new System.EventHandler((s, e) => this.add(s, e, Numb.ToString()));
                }
                else
                {
                    button.Click += new System.EventHandler((s, e) => this.performAction(s, e, button.Text));
                }
            }
        }

        private void add(object sender, EventArgs e,string value)
        {
            if (operation.Count > 0)
            {
                if (operation[0] == "0" && operation.Count == 1 )
                {
                    return;
                }

            }
            if(lastOperation!=0 && operation.Count == 0 && whatToDo == "")
            {
                this.ClearCalc();
            }
            operation.Add(value);
            textBox2.Text += value;
        }
        private void performAction(object sender, EventArgs e, string value)
        {
            
            if (operation.Count > 0 || lastOperation !=0)
            {
                switch (value)
                {
                    case ".":
                        if (operation.Contains(".") || operation.Count == 0)
                        {
                            return;
                        }
                        operation.Add(".");
                        textBox2.Text += value;
                        break;
                    case "-":
                        this.saveLastOperation("-");
                        break;
                    case "+":
                        this.saveLastOperation("+");
                        break;
                    case "C":
                        this.ClearCalc();
                        break;
                    case "X":
                        this.saveLastOperation("*");
                        break;
                    case "/":
                        this.saveLastOperation("/");
                        break;
                    case "=":
                        if (whatToDo == "") return;
                        this.showResult();
                        textBox1.Text = lastOperation.ToString();
                        textBox2.Text = textBox1.Text;
                        operation.Clear();
                        whatToDo = "";
                        break;
                }
            }
            
        }
        private void saveLastOperation(string what)
        {
            
            if (lastOperation !=0)
            {

                this.showResult();
                this.whatToDo = what;
            }
            else
            {
                this.whatToDo = what;
                lastOperation = double.Parse(String.Join("", operation));
                
            }
            operation.Clear();
            textBox1.Text = lastOperation.ToString(); 
            textBox2.Text = lastOperation.ToString() + whatToDo;
            
        }

       private void showResult()
        {
            if (whatToDo == "/" && String.Join("", operation) == "0" || operation.Count<=0) return;
            //Szukalem zamiennika EVAL w JavaScript i internet wskazal mi taka funckje 
            System.Data.DataTable table = new System.Data.DataTable();
            lastOperation = Convert.ToDouble(table.Compute(lastOperation + whatToDo + String.Join("", operation), String.Empty));
            operation.Clear();
        }
       private void ClearCalc()
        {
            this.whatToDo = "";
            this.operation.Clear();
            this.lastOperation = 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
    
}
