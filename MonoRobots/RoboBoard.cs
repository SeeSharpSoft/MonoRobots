using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SeeSharpSoft.MonoRobots
{
    public class RoboBoard
    {
        public RoboBoard()
        {
        }

        public RoboBoard(int width, int height) : this()
        {
            Size = new Size(width + 2, height + 2);
            Clear();
        }

		private RoboField[,] _fields;
        public Difficulty Difficulty { private set; get; }
        private Size _size;
        public Size Size { set { _size = value; _fields = new RoboField[_size.Width, _size.Height]; } get { return _size; } }

        public RoboField[,] Fields {
        	get {
        		return _fields;
        	}
        }

        public String BoardFile { set; get; }

        public void Save(String filename)
        {
            StreamWriter writer = new StreamWriter(filename, false);
            writer.WriteLine((this.Size.Width - 2) + " " + (this.Size.Height - 2));
            foreach(String line in this.ToString(null, false).Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
                writer.WriteLine(line);

            writer.Close();

            BoardFile = filename;
        }

        public StringWriter ToStringWriter()
        {
            StringWriter writer = new StringWriter();
            writer.WriteLine((this.Size.Width - 2) + " " + (this.Size.Height - 2));
            foreach (String line in this.ToString(null, false).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                writer.WriteLine(line);

            writer.Close();

            return writer;
        }

        public RoboBoard CopyBoard()
        {
            RoboBoard board = new RoboBoard();
            board.Load(new StringReader(this.ToStringWriter().ToString()), this.Difficulty);
            board.BoardFile = this.BoardFile;
            return board;
        }

        public bool Load(String filename, Difficulty difficulty)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
            {
                Load(reader, difficulty);
                if (reader != null) reader.Close();
            }

            BoardFile = filename;
            return true;
        }

        public void Load(TextReader reader, Difficulty difficulty)
        {
            Difficulty = difficulty;

            Size = GetSize(reader.ReadLine());

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    RoboField field = null;
                    if (y == 0 || x == 0 || y == Size.Height - 1 || x == Size.Width - 1)
                    {
                        field = (difficulty == Difficulty.Hard ?
                            RoboField.CreateField(FieldType.Hole) : RoboField.DecodeField('p'));
                    }
                    else
                    {
                        char encoded = (char)reader.Read();
                        if (encoded == '\r' || encoded == '\n')
                        {
                            x--;
                            continue;
                        }
                        field = (difficulty == Difficulty.Easy &&
                                (encoded == RoboField.EncodeField(FieldType.Hole) ||
                                encoded == RoboField.EncodeField(FieldType.Oil) ||
                                encoded == RoboField.EncodeField(FieldType.ScrapLeftRight) ||
                                encoded == RoboField.EncodeField(FieldType.ScrapUpDown))) ?
                                    RoboField.DecodeField(' ') :
                                    RoboField.DecodeField(encoded);
                    }

                    SetField(field, x, y);
                }
                //new line?!
                if (y > 0 && y < Size.Height - 1) reader.Read();
            }
        }

        public RoboField GetField(RoboPosition position)
        {
            if (position.Y < 0 || position.X < 0 || position.Y >= Size.Height || position.X >= Size.Width) return null;
            return Fields[position.X, position.Y];
        }

        public void SetField(RoboField field, int x, int y)
        {
            SetField(Fields, field, x, y);
        }

        private void SetField(RoboField[,] fields, RoboField field, int x, int y)
        {
            field.X = x;
            field.Y = y;
            field.Board = this;

            fields[x, y] = field;
        }

        public RoboField GetDestination()
        {
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    if (Fields[x, y].IsDestination) return Fields[x, y];
                }
            }
            return null;
        }

        public RoboField GetStart()
        {
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    if (Fields[x, y].IsStart) return Fields[x, y];
                }
            }
            return null;
        }

        public RoboPosition GetStartPosition()
        {
            RoboField start = GetStart();
            return new RoboPosition(start.X, start.Y, start.StartDirection);
        }

        public void Clear()
        {
            Clear(Fields, Size);
        }

        private void Clear(RoboField[,] fields, Size size)
        {
            if (size == null) return;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    SetField(fields, RoboField.CreateField(FieldType.Empty), x, y);
                }
            }
        }

        public void SetSize(int x, int y)
        {
            RoboField[,] fields = new RoboField[x + 2, y + 2];

            Clear(fields, new Size(x+2,y+2));

            if (Fields != null)
            {
                for (int i = 0; i < x + 2 && i < Size.Width; i++)
                {
                    for (int j = 0; j < y + 2 && j < Size.Height; j++)
                    {
                        fields[i, j] = Fields[i, j];
                    }
                }
            }

            _size = new Size(x + 2, y + 2);
            _fields = fields;
        }

        public void MirrorFieldsHorizontal()
        {
            RoboField[,] fields = new RoboField[Size.Width, Size.Height];

            if (Fields != null)
            {
                for (int i = 0; i < Size.Width; i++)
                {
                    for (int j = 0; j < Size.Height; j++)
                    {
                        Fields[i, j].MirrorHorizontal();
                        fields[Size.Width - i - 1, j] = Fields[i, j];
                    }
                }
            }

            _fields = fields;
        }

        public void MirrorFieldsVertical()
        {
            RoboField[,] fields = new RoboField[Size.Width, Size.Height];

            if (Fields != null)
            {
                for (int i = 0; i < Size.Width; i++)
                {
                    for (int j = 0; j < Size.Height; j++)
                    {
                        Fields[i, j].MirrorVertical();
                        fields[i, Size.Height - j - 1] = Fields[i, j];
                    }
                }
            }

            _fields = fields;
        }

        public void RotateFields()
        {
            RoboField[,] fields = new RoboField[Size.Height, Size.Width];

            if (Fields != null)
            {
                for (int i = 0; i < Size.Width; i++)
                {
                    for (int j = 0; j < Size.Height; j++)
                    {
                        Fields[i, j].Rotate();
                        fields[Size.Height - j - 1, i] = Fields[i, j];
                    }
                }
            }

            _size = new Size(Size.Height, Size.Width);
            _fields = fields;
        }

        private static Size GetSize(String sizeLine)
        {
            String[] split = sizeLine.Split(' ');
            return new Size(int.Parse(split[0]) + 2, int.Parse(split[1]) + 2);
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public string ToString(RoboPosition position)
        {
            return ToString(position, true);
        }

        public string ToString(RoboPosition position, bool printBorder)
        {
            StringBuilder result = new StringBuilder();
            for (int y = (printBorder ? 0 : 1); y < (printBorder ? Size.Height : Size.Height - 1); y++)
            {
                for (int x = (printBorder ? 0 : 1); x < (printBorder ? Size.Width : Size.Width - 1); x++)
                {
                    if (position != null && position.X == x && position.Y == y) result.Append('X');
                    else result.Append(Fields[x, y]);
                }
                result.Append("\r\n");
            }
            return result.ToString();
        }
    }
}