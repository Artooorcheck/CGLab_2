using System;
using System.Drawing;
using System.Windows.Forms;
using CG_2.Render;
using Transform;
using Renderer.Layer;
using Renderer._3DObject;
using CG_2.Render.Light;
using CG_2.Render.Interfaces;
using CG_2.Render.Light.Interfaces;
using System.Collections.Generic;

namespace CG_2
{
    public partial class Form1 : Form
    {
        private Camera _camera;
        private ILight _light;
        private IRenderer _renderer;
        private Vector3 _direction = new Vector3(0, 0, 0);
        private Dictionary<Keys, (Action, Action)> _keyDirection;
        private Point _lastMousePosition;
        private bool _mouseClicked;

        private float _mouseSensetive = 5;
        private float _speed = 0.1f;

        public Form1()
        {
            InitializeComponent();
            _camera = new Camera(new Vector3(5, 0, -1), new Vector3(0, 0, 90));
            var light = new MultiLight();
            light.Add(new Light(new Vector3(3, 3, 1), 0.5f, 4f));
            light.Add(new Light(new Vector3(0, 1, -2), 1f, 2f));
            var layer = new Layer("Default");
            //layer.Add(new Cone("Cone1", new Vector3(-3, 1, -1), Color.Red, 3, 1));
            //layer.Add(new Cone("Cone2", new Vector3(1, 2, 0.5f), Color.Green, 4, 1));
            layer.Add(new Cone("Cone", new Vector3(0, 1, 0), Color.Blue, 1, 1));
            _light = light;
            _renderer = layer;
            _keyDirection = new Dictionary<Keys, (Action, Action)>();

            _keyDirection[Keys.W] = (() => _direction.y = 1, () => _direction.y = 0);
            _keyDirection[Keys.S] = (() => _direction.y = -1, () => _direction.y = 0);
            _keyDirection[Keys.D] = (() => _direction.x = 1, () => _direction.x = 0);
            _keyDirection[Keys.A] = (() => _direction.x = -1, () => _direction.x = 0);
            _keyDirection[Keys.Space] = (() => _direction.z = 1, () => _direction.z = 0);
            _keyDirection[Keys.ShiftKey] = (() => _direction.z = -1, () => _direction.z = 0);

            this.KeyDown += ControlKeyDown;
            this.KeyUp += ControlKeyUp;

            pictureBox1.MouseDown += (o, e) => _mouseClicked = true;
            pictureBox1.MouseUp += (o, e) => _mouseClicked = false;


            _lastMousePosition = MousePosition;
        }

        private void ControlKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                _keyDirection[e.KeyCode].Item2?.Invoke();
            }
            catch { }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                _keyDirection[e.KeyCode].Item1?.Invoke();
            }
            catch { }
        }

        private void RenderScene(object sender, EventArgs e)
        {
            _camera.Render(pictureBox1, _light, _renderer);
        }

        private void Update(object sender, EventArgs e)
        {
            if (_mouseClicked)
            {
                Vector3 deltaMousePosition = new Vector3(_lastMousePosition.Y - MousePosition.Y, 0, _lastMousePosition.X - MousePosition.X) * _mouseSensetive;
                _camera.Rotation += deltaMousePosition;
            }
            _camera.LocalMove(_direction * _speed);
            RenderScene(sender, e);
            _lastMousePosition = MousePosition;
        }
    }
}
