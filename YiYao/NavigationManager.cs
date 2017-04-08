using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
    }

    public interface INavigable
    {
        void Start(object args);
        void Stop();
    }
}
