using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace SeeSharpSoft.MonoRobots.GUI
{
    public class FieldToolbox : Control
    {
        public FieldToolbox()
        {
            _painter = new RoboFieldPainter();

            this.DoubleBuffered = true;

            BoardControl.ImagesLoaded += BoardControl_ImagesLoaded;
        }

        private void BoardControl_ImagesLoaded(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private static List<RoboField> _fieldRepository;

        static FieldToolbox()
        {
            _fieldRepository = new List<RoboField>();
            foreach (char encoded in BoardControl.IMAGENAMES.Keys.Where(elem => elem != 'X' && elem != 'B' && elem != 'a'))
            {
                _fieldRepository.Add(RoboField.DecodeField(encoded));
            }
        }
        
        private int _fieldSize = 60;
        [DefaultValue(60)]
        public int FieldSize
        {
            get { return _fieldSize; }
            set { _fieldSize = value; }
        }

        private RoboFieldPainter _painter;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RoboFieldPainter Painter
        {
            get { return _painter; }
            set { _painter = value; }
        }

        private Point _selectedField = GUIHelper.NULLPOINT;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point SelectedField
        {
            get { return _selectedField; }
            set { _selectedField = value; }
        }

        private Size CalculateSize()
        {
            Size result = new Size();
            result.Width = this.Width / FieldSize;
            result.Height = (int)Math.Ceiling((double)_fieldRepository.Count / result.Width);

            return result;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            Size dimension = CalculateSize();

            for (int x = 0; x < dimension.Width; x++)
            {
                for (int y = 0; y < dimension.Height && x * dimension.Height + y < _fieldRepository.Count; y++)
                {
                    Painter.DoPaint(
                        _fieldRepository[x * dimension.Height + y],
                        pevent.Graphics,
                        new RectangleF(x * FieldSize, y * FieldSize, FieldSize, FieldSize));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (SelectedField == GUIHelper.NULLPOINT) return;

            e.Graphics.DrawRectangle(new Pen(Color.Red, 2f), SelectedField.X*FieldSize, SelectedField.Y*FieldSize, FieldSize, FieldSize);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point location = CalcPointHit(e.Location);
            RoboField field = CalcFieldHit(location);

            SelectedField = location;

            Refresh();

            if (field == null) return;

            DoDragDrop(field, DragDropEffects.Copy);
        }

        private Point CalcPointHit(Point mouseLocation)
        {
            int x = mouseLocation.X / FieldSize;
            int y = mouseLocation.Y / FieldSize;

            Size dimension = CalculateSize();

            if (x < dimension.Width && y < dimension.Height && x * dimension.Height + y < _fieldRepository.Count)
                return new Point(x, y);

            return GUIHelper.NULLPOINT;
        }

        private RoboField CalcFieldHit(Point location)
        {
            Size dimension = CalculateSize();

            if (location == GUIHelper.NULLPOINT) return null;
            
            return _fieldRepository[location.X * dimension.Height + location.Y];
        }
    }

    public class RoboFieldPainter
    {
        public virtual void DoPaint(RoboField roboField, Graphics g)
        {
            DoPaint(roboField, g, g.ClipBounds);
        }

        public virtual void DoPaint(RoboField roboField, Graphics g, RectangleF bounds)
        {
            if (BoardControl.IMAGES == null) return;

            Image image = BoardControl.IMAGES[roboField.EncodedField];

            g.DrawImage(image, bounds);
        }
    }
}