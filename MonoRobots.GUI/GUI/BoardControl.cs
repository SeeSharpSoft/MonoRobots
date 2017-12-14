using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SeeSharpSoft.MonoRobots.GUI
{
    public partial class BoardControl : Control
    {

        #region Static
        public static IDictionary<char, string> IMAGENAMES = new Dictionary<char,string>();
        public static IDictionary<char, Image> IMAGES;

        static BoardControl()
        {
            IMAGENAMES.Add('B', "bot.png");
            IMAGENAMES.Add(' ', "floor.png");
            IMAGENAMES.Add('S', "floor.png");
            IMAGENAMES.Add('T', "floor.png");
            IMAGENAMES.Add('U', "floor.png");
            IMAGENAMES.Add('V', "floor.png");
            IMAGENAMES.Add('H', "hole.png");
            IMAGENAMES.Add('O', "oil.png");
            IMAGENAMES.Add('Z', "finish.png");
            IMAGENAMES.Add('<', "belt-left.png");
            IMAGENAMES.Add('>', "belt-right.png");
            IMAGENAMES.Add('^', "belt-up.png");
            IMAGENAMES.Add('v', "belt-down.png");
            IMAGENAMES.Add('L', "rotate-left.png");
            IMAGENAMES.Add('R', "rotate-right.png");
            IMAGENAMES.Add('p', "wall-leftrightupdown.png");
            IMAGENAMES.Add('C', "pusher-left.png");
            IMAGENAMES.Add('D', "pusher-right.png");
            IMAGENAMES.Add('E', "pusher-up.png");
            IMAGENAMES.Add('F', "pusher-down.png");
            IMAGENAMES.Add('M', "squeezer-leftright.png");
            IMAGENAMES.Add('N', "squeezer-updown.png");
            IMAGENAMES.Add('a', "floor.png");
            IMAGENAMES.Add('b', "wall-left.png");
            IMAGENAMES.Add('c', "wall-right.png");
            IMAGENAMES.Add('d', "wall-leftright.png");
            IMAGENAMES.Add('e', "wall-up.png");
            IMAGENAMES.Add('f', "wall-leftup.png");
            IMAGENAMES.Add('g', "wall-rightup.png");
            IMAGENAMES.Add('h', "wall-leftrightup.png");
            IMAGENAMES.Add('i', "wall-down.png");
            IMAGENAMES.Add('j', "wall-leftdown.png");
            IMAGENAMES.Add('k', "wall-rightdown.png");
            IMAGENAMES.Add('l', "wall-leftrightdown.png");
            IMAGENAMES.Add('m', "wall-updown.png");
            IMAGENAMES.Add('n', "wall-leftupdown.png");
            IMAGENAMES.Add('o', "wall-rightupdown.png");
        }

        private static void CreateStartFields(string basePath)
        {
            Image line = Image.FromFile(basePath + "/../additional/scratchline.png");

            Image tmp = Bitmap.FromFile((basePath + "/" + IMAGENAMES[' ']));
            tmp = tmp.GetThumbnailImage(tmp.Width, tmp.Height, null, IntPtr.Zero);
            line = line.GetThumbnailImage(tmp.Width, tmp.Height, null, IntPtr.Zero);
            Graphics graph = Graphics.FromImage(tmp);
            
            graph.DrawImage(line, 0, 0);
            IMAGES['T'] = tmp;

            line.RotateFlip(RotateFlipType.Rotate90FlipNone);
            tmp = Bitmap.FromFile((basePath + "/" + IMAGENAMES[' ']));
            tmp = tmp.GetThumbnailImage(tmp.Width, tmp.Height, null, IntPtr.Zero);
            graph = Graphics.FromImage(tmp);
            graph.DrawImage(line, 0, 0);
            IMAGES['V'] = tmp;

            line.RotateFlip(RotateFlipType.Rotate90FlipNone);
            tmp = Bitmap.FromFile((basePath + "/" + IMAGENAMES[' ']));
            tmp = tmp.GetThumbnailImage(tmp.Width, tmp.Height, null, IntPtr.Zero);
            graph = Graphics.FromImage(tmp);
            graph.DrawImage(line, 0, 0);
            IMAGES['S'] = tmp;

            line.RotateFlip(RotateFlipType.Rotate90FlipNone);
            tmp = Bitmap.FromFile((basePath + "/" + IMAGENAMES[' ']));
            tmp = tmp.GetThumbnailImage(tmp.Width, tmp.Height, null, IntPtr.Zero);
            graph = Graphics.FromImage(tmp);
            graph.DrawImage(line, 0, 0);
            IMAGES['U'] = tmp;
        }

        #endregion


        public BoardControl()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.AllowDrop = true;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RoboBoard Board { set; get;}

        private float _zoom = 1f;
        [DefaultValue(1f)]
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public static event EventHandler ImagesLoaded;
        protected void OnImagesLoaded(EventArgs e)
        {
            ImagesLoaded?.Invoke(this, e);
        }

        public void LoadImages(string basePath)
        {
            IMAGES = new Dictionary<char, Image>();
            foreach (KeyValuePair<char, string> elem in IMAGENAMES)
            {
                IMAGES.Add(elem.Key, Image.FromFile(basePath + "/" + elem.Value));
            }
            IMAGES.Add('X', Image.FromFile(basePath + "/../additional/rip.png"));

            CreateStartFields(basePath);

            OnImagesLoaded(EventArgs.Empty);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (Board == null) return;

            if (IMAGES == null) LoadImages(Application.StartupPath + @"/Images/new");

            int fieldSize = GetFieldSize();
            PointFloat offset = GetOffset();

            for (int y = 1; y < Board.Size.Height - 1; y++)
            {
                float yPos = offset.Y + (y - 1) * fieldSize * Zoom;
                if (yPos > e.ClipRectangle.Height) break;

                for (int x = 1; x < Board.Size.Width - 1; x++)
                {
                    float xPos = offset.X + (x - 1) * fieldSize * Zoom;
                    if (xPos > e.ClipRectangle.Width) break;

                    Image image = IMAGES[Board.Fields[x, y].EncodedField];

                    e.Graphics.DrawImage(image, xPos, yPos, fieldSize * Zoom, fieldSize * Zoom);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                return;
            }

            int fieldSize = GetFieldSize();
            PointFloat offset = GetOffset();

            if (_hoverPoint != GUIHelper.NULLPOINT)
            {
                e.Graphics.DrawRectangle(new Pen(Color.WhiteSmoke, 2f), offset.X + _hoverPoint.X * fieldSize * Zoom, offset.Y + _hoverPoint.Y * fieldSize * Zoom, fieldSize * Zoom, fieldSize * Zoom);
            }
        }

        public void DrawRobo(PaintEventArgs e, RoboPosition position, String name)
        {
            if (position == null) return;

            if(!IsInsideBoard(new Point(position.X, position.Y))) return;

            int fieldSize = GetFieldSize();
            PointFloat offset = GetOffset();

            Image roboImage = IMAGES['B'];

            if (position.IsDead)
            {
                roboImage = IMAGES['X'];
                e.Graphics.DrawImage(roboImage,
                                    offset.X + position.X * fieldSize * Zoom,
                                    offset.Y + position.Y * fieldSize * Zoom,
                                    fieldSize,
                                    fieldSize);
            }
            else
            {
                switch (position.Direction)
                {
                    case Direction.Left:
                        roboImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        e.Graphics.DrawImage(roboImage,
                            offset.X + position.X * fieldSize * Zoom,
                            offset.Y + position.Y * fieldSize * Zoom,
                            fieldSize,
                            fieldSize);
                        roboImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case Direction.Up:
                        roboImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        e.Graphics.DrawImage(roboImage,
                            offset.X + position.X * fieldSize * Zoom,
                            offset.Y + position.Y * fieldSize * Zoom,
                            fieldSize,
                            fieldSize);
                        roboImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case Direction.Down:
                        roboImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        e.Graphics.DrawImage(roboImage,
                            offset.X + position.X * fieldSize * Zoom,
                            offset.Y + position.Y * fieldSize * Zoom,
                            fieldSize,
                            fieldSize);
                        roboImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    default:
                        e.Graphics.DrawImage(roboImage,
                            offset.X + position.X * fieldSize * Zoom,
                            offset.Y + position.Y * fieldSize * Zoom,
                            fieldSize,
                            fieldSize);
                        break;
                }
            }
            e.Graphics.DrawString(name, new Font("Arial", 10), new SolidBrush(Color.FloralWhite), offset.X + position.X * fieldSize * Zoom, offset.Y + position.Y * fieldSize * Zoom);
        }

        public int GetFieldSize()
        {
            if (Board == null) return 0;

            return Math.Min(this.Width / (Board.Size.Width - 2), this.Height / (Board.Size.Height - 2));
        }

        public PointFloat GetOffset()
        {
            int fieldSize = GetFieldSize();

            float height = (Board.Size.Height - 2) * fieldSize * Zoom;
            float width = (Board.Size.Width - 2) * fieldSize * Zoom;

            return new PointFloat((this.Width - width) / 2.0, (this.Height - height) / 2.0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Invalidate();
        }

        #region EditHandling (DragDrop)

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            HandleDrop(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            HandleDragEvent(drgevent);
        }

        private void HandleDragEvent(DragEventArgs drgevent)
        {
            object drgObject = drgevent.Data.GetData(drgevent.Data.GetFormats()[0]);
            switch (drgevent.Data.GetFormats()[0])
            {
                case "SeeSharpSoft.MonoRobots.RoboField":
                case "SeeSharpSoft.MonoRobots.RoboFieldConveyor":
                case "SeeSharpSoft.MonoRobots.RoboFieldPusher":
                case "SeeSharpSoft.MonoRobots.RoboFieldHole":
                case "SeeSharpSoft.MonoRobots.RoboFieldOil":
                case "SeeSharpSoft.MonoRobots.RoboFieldScrap":
                case "SeeSharpSoft.MonoRobots.RoboFieldRotator":
                    if (drgevent.AllowedEffect == DragDropEffects.Copy || drgevent.KeyState == 1)
                        drgevent.Effect = DragDropEffects.Copy;
                    else drgevent.Effect = DragDropEffects.Move;
                    break;
            }
        }

        private void HandleDrop(DragEventArgs drgevent)
        {
            object drgObject = drgevent.Data.GetData(drgevent.Data.GetFormats()[0]);

            if (drgObject is RoboField) HandleDropRoboField(drgevent, drgObject as RoboField);
        }

        private void HandleDropRoboField(DragEventArgs drgevent, RoboField field)
        {
            Point target = CalcPointHit(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
            if (target == GUIHelper.NULLPOINT) return;

            switch (drgevent.Effect)
            {
                case DragDropEffects.Copy:
                    Board.SetField(RoboField.CreateField(field.FieldType), target.X + 1, target.Y + 1);
                    break;
                case DragDropEffects.Move:
                    Board.SetField(RoboField.CreateField(FieldType.Empty), field.X, field.Y);
                    Board.SetField(field, target.X + 1, target.Y + 1);
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            
            RoboField field = CalcFieldHit(CalcPointHit(e.Location));

            if (field == null) return;

            if (e.Clicks > 1)
            {
                field.Rotate();
                if (e.Button == MouseButtons.Right)
                {
                    field.Rotate();
                    field.Rotate();
                }
            }
            else
            {
                DoDragDrop(field, e.Button == MouseButtons.Left ? DragDropEffects.Copy : DragDropEffects.Move);
            }
        }

        private Point _hoverPoint = GUIHelper.NULLPOINT;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _hoverPoint = CalcPointHit(e.Location);

            Invalidate();
        }

        private Point CalcPointHit(Point mouseLocation)
        {
            int fieldSize = GetFieldSize();
            PointFloat offset = GetOffset();

            if (fieldSize == 0) return GUIHelper.NULLPOINT;

            int x = (int)Math.Floor((mouseLocation.X - offset.X) / (fieldSize * Zoom));
            int y = (int)Math.Floor((mouseLocation.Y - offset.Y) / (fieldSize * Zoom));

            Point point = new Point(x, y);
            if (IsInsideBoard(point)) return point;

            return GUIHelper.NULLPOINT;
        }

        private bool IsInsideBoard(Point point)
        {
            if (point.X < Board.Size.Width - 2 && point.Y < Board.Size.Height - 2 && point.Y >= 0 && point.X >= 0)
                return true;
            return false;
        }

        private RoboField CalcFieldHit(Point location)
        {
            if (location == GUIHelper.NULLPOINT) return null;

            return Board.Fields[location.X + 1, location.Y + 1];
        }

        #endregion
    }

    public enum BoardMode
    {
        Edit
    }

    public class PointFloat
    {
        public PointFloat(float x, float y)
        {
            X = x;
            Y = y;
        }

        public PointFloat(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public float X { set; get; }
        public float Y { set; get; }
    }
}