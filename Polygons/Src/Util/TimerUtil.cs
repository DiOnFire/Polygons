using Polygons.Shape;
using System;
using System.Windows.Forms;

namespace Polygons.Util
{
    class TimerUtil
    {
        private System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private Renderer renderer;
        private DynamicUtil util;
        private Form form;
        public bool paused;

        public TimerUtil(Renderer renderer, Form form)
        {
            this.renderer = renderer;
            this.form = form;
            util = new DynamicUtil(renderer.ShapeManager);
        }

        public void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            util.MoveRandom();
            form.Refresh();
            
            renderer.ClearGarbage(renderer._used);
        }

        public void Setup()
        {
            renderer.ShapeManager.SaveShapesState();

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 200;
            myTimer.Start();

            paused = false;
        }

        public void Pause()
        {
            myTimer.Stop();
            paused = true;
        }

        public void Continue()
        {
            myTimer.Start();
        }

        public void Stop()
        {
            myTimer.Stop();
            renderer.ShapeManager.RestoreState();
            form.Refresh();
            paused = false;
        }
    }
}
