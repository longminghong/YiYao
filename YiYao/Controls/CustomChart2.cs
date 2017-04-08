using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YiYao
{
    public class CustomChart2 : Control
    {
        
        private Dictionary<string, double>[] mData;
     
        private int mStepY;
        private double mMinY;
        private int mRows;
        public static readonly DependencyProperty DotWidthProperty = DependencyProperty.Register("DotWidth", typeof(int), typeof(CustomChart2), new PropertyMetadata(15));
        public int DotWidth
        {
            get
            {
                return (int)GetValue(DotWidthProperty);
            }
            set
            {
                SetValue(DotWidthProperty, value);
            }
        }

        public void Intialize(object[][] data, object[][] data2,int yStep)
        {
            if (data.Length == 0 || data == null)
                return;
            mData = new Dictionary<string, double>[2];
            mData[0] = new Dictionary<string, double>();
            mData[1] = new Dictionary<string, double>();
            
            for (int i = 0; i < data.Length; i++)
            {
                var item = data[i];
                var date = DateTime.Parse((string)item[0]);
                mData[0].Add((i + 1) + "次", Convert.ToDouble(item[1]));
            }
            for (int i = 0; i < data2.Length; i++)
            {
                var item = data2[i];
                var date = DateTime.Parse((string)item[0]);
                mData[1].Add((i + 1) + "次", Convert.ToDouble(item[1]));
            }

            var maxY = (int)(Math.Ceiling(Math.Max(mData[0].Values.Max(), mData[1].Values.Max())) + yStep - 1) / yStep * yStep;
            mMinY = (int)Math.Min(mData[0].Values.Min(), mData[1].Values.Min()) / yStep * yStep;
            mRows = (int)(maxY - mMinY) / yStep;
            mStepY = yStep;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (mData == null)
                return;
            var cellHeight = Height / mRows;
            var cellWidth = Width / (mData.Length - 1);
            SolidColorBrush lineBrush = new SolidColorBrush(Color.FromRgb(194,194,194));
            //draw lines
            double y = 0;
            for (int i = 0; i <= mRows; i++)
            {
                drawingContext.DrawLine(new Pen(lineBrush,1), new System.Windows.Point(0, y), new System.Windows.Point(Width, y));
                y += cellHeight;

            }
            double x = 0;
            for (int i = 0; i < mData.Length; i++)
            {
                drawingContext.DrawLine(new Pen(lineBrush, 1), new System.Windows.Point(x, 0), new System.Windows.Point(x, Height));
                x += cellWidth;
            }
            //draw y label
            double marginRight = mRows;
            for (int i = 0; i <= mRows; i++)
            {
                if (i == 0)
                    continue;
                var yValue = FormatText($"{mMinY + mStepY * i}");
                drawingContext.DrawText(yValue, new System.Windows.Point(-yValue.Width - marginRight, Height - cellHeight * i - yValue.Height/2));
            }
            //draw x label
            for (int i = 0; i < mData.Length; i++)
            {
                
                var keyvalue = mData[0].ElementAt(i);
                var xValue = keyvalue.Value;
                var xLabel = FormatText(keyvalue.Key);
                drawingContext.DrawText(xLabel, new System.Windows.Point(cellWidth * i - xLabel.Width/2, Height + mRows));
            }
            //draw circle and lines
            List<Point> points = new List<Point>();
            for (int i = 0; i < mData.Length; i++)
            {

                var keyvalue = mData[0].ElementAt(i);
                var xValue = keyvalue.Value;
               var percentage = (xValue - mMinY) /( mRows * mStepY);
                var point =new Point(cellWidth * i, Height * (1 - percentage));
                points.Add(point);
            }
            DrawData(drawingContext, points);
            points.Clear();
            for (int i = 0; i < mData.Length; i++)
            {

                var keyvalue = mData[1].ElementAt(i);
                var xValue = keyvalue.Value;
                var percentage = (xValue - mMinY) / (mRows * mStepY);
                var point = new Point(cellWidth * i, Height * (1 - percentage));
                points.Add(point);
            }
            DrawData(drawingContext, points);
        }

        private void DrawData(DrawingContext drawingContext,  List<Point> points)
        {
            SolidColorBrush dotBrush = new SolidColorBrush(Color.FromRgb(11, 176, 135));
            SolidColorBrush dotlineBrush = new SolidColorBrush(Color.FromRgb(4, 217, 211));
            SolidColorBrush dotFillBrush = new SolidColorBrush(Color.FromArgb(180, 26, 130, 113));
            Point lastPoint = new Point(double.MaxValue, 0);
            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                var startPoint = points.First();
                var endPoint = points.Last();
                geometryContext.BeginFigure(startPoint, true, true);
                var pp = new PointCollection(points);
                if (startPoint.Y < endPoint.Y)
                {
                    pp.Add(new Point(0, Math.Max(startPoint.Y, endPoint.Y)));
                }
                else
                {
                    pp.Add(new Point(startPoint.X + Width, Math.Max(startPoint.Y, endPoint.Y)));
                }
                geometryContext.PolyLineTo(pp, true, true);
            }
            drawingContext.DrawGeometry(dotFillBrush, null, streamGeometry);

            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                if (lastPoint.X != double.MaxValue)
                {
                    drawingContext.DrawLine(new Pen(dotlineBrush, 2), lastPoint, point);
                }
                lastPoint = point;
            }
            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                if (i != 0)
                {
                    drawingContext.DrawEllipse(Brushes.White, new Pen(dotBrush, DotWidth / 4.0), point, DotWidth, DotWidth);
                    drawingContext.DrawEllipse(dotBrush, null, point, DotWidth / 4.0, DotWidth / 3.0);
                }
            }
        }


        private FormattedText FormatText(string text)
        {
            SolidColorBrush lineBrush = new SolidColorBrush(Color.FromRgb(194, 194, 194));
            return new FormattedText(text, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface(""), 14, lineBrush);

        }
    }
}
