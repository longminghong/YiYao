using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YiYao
{
    public class NavigationManager : Canvas
    {
        private dynamic mCurUI;
        private dynamic mPreUI;

        public void Init()
        {
            foreach (dynamic item in this.Children)
            {
                item.Init();
            }
        }


        public void GoToUI(UIElement newUI, object args = null)
        {
            if(mCurUI != null)
            {
                mPreUI = mCurUI;
                Children.Remove(mPreUI);
                mPreUI.Stop();
            }

            mCurUI = newUI;
            Children.Add(newUI);
            mCurUI.Start(args);
        }

        public void GoToPage(Type type)
        {
            var ui =(UIElement) Activator.CreateInstance(type);
            GoToUI(ui);
        }

        public bool SimulateImageClick(FrameworkElement control, MouseButtonEventHandler handler)
        {
            if (control == null || handler == null) return false;
            int status = 0;
            control.MouseEnter += delegate { if (status == 0) status = -1; };
            control.MouseLeave += delegate { status = 0; };
            control.MouseLeftButtonDown += delegate { status = 1; };
            control.MouseLeftButtonUp += delegate (object sender, MouseButtonEventArgs e) {
                if (status > 0)
                    handler(sender, e);
                status = 0;
            };
            return true;
        }
    }
    
    public interface INavigable
    {
        void Start(object args);
        void Stop();
    }
}
