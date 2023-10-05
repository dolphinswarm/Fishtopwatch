using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Extensions;
using System.Runtime.InteropServices;

namespace Fishtopwatch.Animations
{
    class StopwatchRunningAnimation : BaseAnimation
    {
        Animation StopwatchRunning(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((v) => view.ScaleX = v, 0.85, 1.15, beginAt: 0, finishAt: 0.5);
            animation.WithConcurrent((v) => view.ScaleY = v, 1.15, 0.85, beginAt: 0, finishAt: 0.5);

            animation.WithConcurrent((v) => view.ScaleY = v, 0.85, 1.15, beginAt: 0.5, finishAt: 1);
            animation.WithConcurrent((v) => view.ScaleX = v, 1.15, 0.85, beginAt: 0.5, finishAt: 1);

            return animation;
        }

        public override Task Animate(VisualElement view)
        {
            view.Animate("StopwatchRunning", StopwatchRunning(view), 16, 1000, Easing.CubicInOut, repeat: () => true);
            return Task.CompletedTask;
        }
    }

    public class StartStopwatchAnimationAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            var animation = new StopwatchRunningAnimation();
            animation.Animate(sender);
        }
    }

    public class StopStopwatchAnimationAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            sender.AbortAnimation("StopwatchRunning");

            // Reset to the original size
            sender.ScaleX = 1;
            sender.ScaleY = 1;
        }
    }
}