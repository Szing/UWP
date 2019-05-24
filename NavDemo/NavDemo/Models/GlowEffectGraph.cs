using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Models
{
    class GlowEffectGraph : IDisposable
    {
        private MorphologyEffect morphology;
        public GlowEffectGraph()
        {
            Blur.BlurAmount = 10;
            Blur.BorderMode = EffectBorderMode.Soft;

            morphology = new MorphologyEffect()
            {
                Mode = MorphologyEffectMode.Dilate,
                Width = 10,
                Height = 10,
            };

            Blur.Source = morphology;

        }

        public GaussianBlurEffect Blur { get; set; } = new GaussianBlurEffect();

        public void Dispose()
        {
            Blur.Dispose();
            morphology.Dispose();
        }

        public void Setup(ICanvasImage canvas, double amount = 10)
        {
            morphology.Source = canvas;
            amount = Math.Min(amount / 2, 100);
            morphology.Width = (int)Math.Truncate(Math.Floor(amount));
            morphology.Height = (int)Math.Truncate(Math.Floor(amount));
            Blur.BlurAmount = (float)amount;
        }
    }

}
