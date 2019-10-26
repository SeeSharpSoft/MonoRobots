namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Make MonoRobots base independent of System.Drawing assembly
    /// </summary>
    public class Size
    {
        public int Width { set; get; }
        
        public int Height { set; get; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Size() : this(0, 0)
        {
        }
    }
}