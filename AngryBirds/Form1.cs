using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngryBirds
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Definitely not the 'Angry Birds'!";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //double angle = Convert.ToDouble(textBox2.Text);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked on Label! What a useless move!");
        }
        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked on Label! Do not do it again!");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //double initial_velocity = Convert.ToDouble(textBox1.Text);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            double angle = Convert.ToDouble(getAngle());
            double veloc = Convert.ToDouble(getVeloc());
            double X = Convert.ToDouble(getX());
            double Y = Convert.ToDouble(getY());

            Obstacle obs = new Obstacle(X,Y);
            Bird ptica = new Bird();

            ptica.CollisionDidNotHappen += CollisionMessage2;
            ptica.CollisionHappened     += CollisionMessage1;


            ptica.Fly(angle, veloc, obs);
            
        }

        string getAngle() { return textBox1.Text; }
        string getVeloc() { return textBox2.Text; }
        string getX()     { return textBox3.Text; }
        string getY()     { return textBox4.Text; }

        // event hadler
        public void CollisionMessage1()
        {
            MessageBox.Show("Hit the bull's-eye!");
        }
        public void CollisionMessage2()
        {
            MessageBox.Show("Whoops, you missed!");
        }
        // event handler
    }

        // delegates
        public delegate void OnColission();
        public delegate void NoColission();
        // delegates

    public class Bird
    {

        public Bird()
        {

        }

        public event OnColission CollisionHappened;
        public event NoColission CollisionDidNotHappen;

        private const double g = 9.81;
        private const double k = 0.30;
        private List<Tuple<double, double>> ls = new List<Tuple<double, double>>();

        public void Fly(double angle, double veloc, Obstacle obstacle)
        {
            double x1 = 0;
            double y1 = 0;
            int counter = 0;
            double velX1 = veloc * Math.Cos(angle * Math.PI / 180);
            double velY1 = veloc * Math.Sin(angle * Math.PI / 180);

            double time = (2 * veloc * Math.Sin(angle * Math.PI / 180)) / g;

                for (double n = 1000; n >= 0; n--)
                {
                    double timeStep = time / n;

                    x1 += velX1 * timeStep;
                    y1 += velY1 * timeStep;

                    velY1 -= timeStep * (g + k * velY1);
                    velX1 -= timeStep * k * velX1;

                    if (y1 <= 0)
                        break;

                    if (Math.Abs(x1 - obstacle.X) < 0.5 && (Math.Abs(y1 - obstacle.Y) < 0.5))
                    {
                        CollisionHappened?.Invoke(); counter++; break; 
                    }

                    var coords = Tuple.Create(x1, y1);
                    ls.Add(coords);
                }
            if (counter == 0) CollisionDidNotHappen?.Invoke();
        }
    }
    public class Obstacle
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Obstacle(double x, double y)
        {
            X = x;
            Y = y;
        }
    }  
}


