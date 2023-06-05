using System;
using System.Drawing;
using System.Windows.Forms;
using CG_2.Render;
using Transform;
using Renderer.Layer;
using Renderer._3DObject;
using CG_2.Render.Light;

namespace CG_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Render_Click(object sender, EventArgs e)
        {
            var graph = Screen.CreateGraphics();
            var camera = new Camera(new Vector3(-1, -4, -2), new Vector3(30, 0, 0));
            var light = new MultiLight();
            light.Add(new Light(new Vector3(3, 3, 1), 0.5f, 4f));
            light.Add(new Light(new Vector3(0, 1, -2), 1f, 2f));
            var layer = new Layer("Default");
            layer.Add(new Cone("Cone1", new Vector3(-3, 1, -1), Color.Red, 3, 1));
            layer.Add(new Cone("Cone2", new Vector3(1, 2, 0.5f), Color.Green, 4, 1));
            layer.Add(new Cone("Cone", new Vector3(0, 1, 0), Color.Blue, 1, 1));
            camera.Render(graph, light, layer);
        }
    }
}
