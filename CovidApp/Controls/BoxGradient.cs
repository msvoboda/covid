using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CovidApp.Controls
{
    public class BoxGradient : SKCanvasView
    {
        public static readonly BindableProperty Color1Property = BindableProperty.Create(nameof(Color1),
        typeof(Color), typeof(BoxGradient), Color.Blue,
        propertyChanging: (currentControl, oldValue, newValue) =>
        {
            var thisControl = currentControl as BoxGradient;
            thisControl.Color1 = (Color)newValue;
        });

        public static readonly BindableProperty Color2Property = BindableProperty.Create(nameof(Color2),
        typeof(Color), typeof(BoxGradient), Color.Blue,
        propertyChanging: (currentControl, oldValue, newValue) =>
        {
            var thisControl = currentControl as BoxGradient;
            thisControl.Color2 = (Color)newValue;
        });

        public static readonly BindableProperty CircleStrokeWidthProperty = BindableProperty.Create(nameof(CircleStrokeWidth),
        typeof(float), typeof(BoxGradient), (float)4,
        propertyChanging: (currentControl, oldValue, newValue) =>
        {
            var thisControl = currentControl as BoxGradient;
            thisControl.CircleStrokeWidth = (float)newValue;
        });

        public static readonly BindableProperty RoundRadiusProperty = BindableProperty.Create(nameof(RoundRadius),
    typeof(float), typeof(BoxGradient), (float)4,
    propertyChanging: (currentControl, oldValue, newValue) =>
    {
        var thisControl = currentControl as BoxGradient;
        thisControl.myRoundRadius = (float)newValue;
    });


        private SKColor myColor1;
        private SKColor myColor2;
        private SKColor myBackgroundColor;
        private float myCircleStrokeWidth;
        private float myRoundRadius;

        public BoxGradient()
        {
            BackgroundColor = myBackgroundColor.ToFormsColor();
            Color1 = Color.Blue;
            Color2 = Color.Blue;
            CircleStrokeWidth = 40;
        }

        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set
            {
                SetValue(Color1Property, value);
                myColor1 = value.ToSKColor();
                InvalidateSurface();
            }
        }

        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set
            {
                SetValue(Color2Property, value);
                myColor2 = value.ToSKColor();
                InvalidateSurface();
            }
        }

        public float CircleStrokeWidth
        {
            get { return (float)GetValue(CircleStrokeWidthProperty); }
            set
            {
                SetValue(CircleStrokeWidthProperty, value);
                myCircleStrokeWidth = value;
                InvalidateSurface();
            }
        }

        public float RoundRadius
        {
            get { return (float)GetValue(CircleStrokeWidthProperty); }
            set
            {
                SetValue(CircleStrokeWidthProperty, value);
                myRoundRadius = value;
                InvalidateSurface();
            }
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(BackgroundColor))
            {
                myBackgroundColor = BackgroundColor.ToSKColor();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            SKRoundRect skRoundRect= new SKRoundRect(e.Info.Rect, myRoundRadius);
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(myBackgroundColor);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 48,
                StrokeWidth = myCircleStrokeWidth,
                IsAntialias = true
            };

            paint.Style = SKPaintStyle.Fill;
            paint.Color = myColor1;

            paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(skRoundRect.Rect.Left, skRoundRect.Rect.Top),
                    new SKPoint(skRoundRect.Rect.Right, skRoundRect.Rect.Bottom),
                    new SKColor[] { myColor1, myColor2 },
                    new float[] { 0, 1f },
                    SKShaderTileMode.Repeat);

            
            canvas.DrawRoundRect(skRoundRect, paint);
        }
    }
}
