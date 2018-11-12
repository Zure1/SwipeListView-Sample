using CoreGraphics;
using Foundation;
using SwipeListViewProject.Components.SwipeListView;
using System;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SwipeItemView), typeof(SwipeListViewProject.iOS.CustomRenderers.SwipeItemRenderer))]
namespace SwipeListViewProject.iOS.CustomRenderers
{
    class SwipeItemRenderer : ViewRenderer
    {
        SwipeItemView touchedElement;
        private CGPoint touchLocation;
        double currentQuota;

        public SwipeItemRenderer()
        {
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            UITouch touch = touches.AnyObject as UITouch;
            touchLocation = touch.LocationInView(this);

            TouchDispatcher.TouchingView = Element as SwipeItemView;
            TouchDispatcher.StartingBiasX = (float)touchLocation.X;
            TouchDispatcher.StartingBiasY = (float)touchLocation.Y;
            TouchDispatcher.InitialTouch = DateTime.Now;

            base.TouchesMoved(touches, evt);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            UITouch touch = touches.AnyObject as UITouch;
            touchLocation = touch.LocationInView(this);

            currentQuota = (float)((touchLocation.X - TouchDispatcher.StartingBiasX) / Bounds.Size.Width);
            touchedElement = (TouchDispatcher.TouchingView as SwipeItemView);

            TouchDispatcher.TouchingView.PerformTranslation(currentQuota);

            base.TouchesMoved(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            if (touchedElement != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (touchedElement != null)
                    {
                        await touchedElement.CompleteTranslationAsync(currentQuota);
                        (Element.Parent.Parent as SwipeListView).AppendTouchedElement(touchedElement);
                    }
                });
            }
            
            (Element.Parent.Parent as SwipeListView).AppendTouchedElement(touchedElement);
            TouchDispatcher.TouchingView = null;
            TouchDispatcher.StartingBiasX = 0;
            TouchDispatcher.StartingBiasY = 0;
            base.TouchesEnded(touches, evt);
        }
    }
}