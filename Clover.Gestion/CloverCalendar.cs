using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Clover.Gestion
{
    public class CloverCalendar
    {
        public DateTime Time
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = new DateTime(value.Year, value.Month, 1);
            }
        }
        public event EventHandler<Bitmap> NewFrame;
        public enum ColoringOptions { Event, Shipment, Both };
        public Dictionary<DateTime, ColoringOptions> CustomColoring = new Dictionary<DateTime, ColoringOptions>();

        private DateTime _Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        private int _RingDiameter;
        private Dictionary<DateTime, Point> _DayRings;

        public CloverCalendar()
        {
        }

        public void DrawCalendar(Size CalendarSize)
        {
            int daysThisMonth = DateTime.DaysInMonth(_Time.Year, _Time.Month);
            int weeksThisMonth = 1;
            for (int i = 0; i < daysThisMonth; i++)
            {
                if (_Time.AddDays(i).DayOfWeek == DayOfWeek.Saturday && i != (daysThisMonth - 1))
                    weeksThisMonth++;
            }
            int xSpacing = (int)(Math.Round((CalendarSize.Width - 40) / 7M));
            int ySpacing = (int)(Math.Round((CalendarSize.Height - 130) / (decimal)(weeksThisMonth)));
            _RingDiameter = Math.Min(xSpacing, ySpacing) - 10;
            _DayRings = new Dictionary<DateTime, Point>();
            int row = 0;
            for (int i = 0; i < daysThisMonth; i++)
            {
                var current = _Time.AddDays(i);
                int x = (int)(Math.Round(20 + ((xSpacing - _RingDiameter) / 2M) + (xSpacing * (int)(current.DayOfWeek))));
                int y = 110 + (ySpacing * row);
                _DayRings.Add(current, new Point(x, y));
                if (current.DayOfWeek == DayOfWeek.Saturday)
                    row++;
            }
            Bitmap background = new Bitmap(CalendarSize.Width, CalendarSize.Height);
            using (var graphics = Graphics.FromImage(background))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.White);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                using (var font = new Font("Calibri", 20F))
                {
                    graphics.DrawString("Domingo", font, Brushes.Black, new Rectangle(20, 60, xSpacing, 30), sf);
                    graphics.DrawString("Lunes", font, Brushes.Black, new Rectangle(20 + xSpacing, 60, xSpacing, 30), sf);
                    graphics.DrawString("Martes", font, Brushes.Black, new Rectangle(20 + (xSpacing * 2), 60, xSpacing, 30), sf);
                    graphics.DrawString("Miércoles", font, Brushes.Black, new Rectangle(20 + (xSpacing * 3), 60, xSpacing, 30), sf);
                    graphics.DrawString("Jueves", font, Brushes.Black, new Rectangle(20 + (xSpacing * 4), 60, xSpacing, 30), sf);
                    graphics.DrawString("Viernes", font, Brushes.Black, new Rectangle(20 + (xSpacing * 5), 60, xSpacing, 30), sf);
                    graphics.DrawString("Sábado", font, Brushes.Black, new Rectangle(20 + (xSpacing * 6), 60, xSpacing, 30), sf);
                }
                using (var font = new Font("Calibri", 18F, FontStyle.Bold))
                {
                    graphics.DrawString(_Time.ToString("MMMM yyyy"), font, Brushes.Black, new Rectangle(20 + (xSpacing * 6), 20, xSpacing, 30), sf);
                }
                using (var font = new Font("Calibri", 25F))
                {
                    foreach (var ring in _DayRings)
                    {
                        if (CustomColoring.ContainsKey(ring.Key))
                        {
                            switch (CustomColoring[ring.Key])
                            {
                                case ColoringOptions.Event:
                                    graphics.FillEllipse(Brushes.IndianRed, ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter);
                                    break;
                                case ColoringOptions.Shipment:
                                    graphics.FillEllipse(Brushes.DarkCyan, ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter);
                                    break;
                                case ColoringOptions.Both:
                                    graphics.FillPie(Brushes.IndianRed, ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter, 45, 180);
                                    graphics.FillPie(Brushes.DarkCyan, ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter, 225, 180);
                                    break;
                            }
                            graphics.DrawString(ring.Key.Day.ToString(), font, Brushes.White, new Rectangle(ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter), sf);
                        }
                        else if (ring.Key == DateTime.Now.Date)
                        {
                            graphics.FillEllipse(Brushes.LightGray, ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter);
                            graphics.DrawEllipse(new Pen(Color.Black, 3), ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter);
                            graphics.DrawString(ring.Key.Day.ToString(), font, Brushes.Black, new Rectangle(ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter), sf);
                        }
                        else
                        {
                            graphics.DrawEllipse(new Pen(Color.Black, 3), ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter);
                            graphics.DrawString(ring.Key.Day.ToString(), font, Brushes.Black, new Rectangle(ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter), sf);
                        }
                    }
                }
            }
            OnNewFrame(background);
        }
        public DateTime? GetTimeFromLocation(Point input)
        {
            DateTime? timeFromLocation = null;
            if (_DayRings == null)
            {
                return timeFromLocation;
            }
            foreach (var ring in _DayRings)
            {
                if (new Rectangle(ring.Value.X, ring.Value.Y, _RingDiameter, _RingDiameter).Contains(input))
                {
                    timeFromLocation = ring.Key;
                    break;
                }
            }
            return timeFromLocation;
        }

        protected virtual void OnNewFrame(Bitmap frame)
        {
            if (NewFrame != null)
            {
                NewFrame.Invoke(this, frame);
            }
        }
    }
}
