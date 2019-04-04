using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace NavDemo.Behaviours
{
    public class ListViewBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; set; }

        /// <summary>
        /// 需要做动画的列表
        /// </summary>
        public ListView ListView;

        /// <summary>
        /// listView 里面的滚动条
        /// </summary>
        public ScrollViewer scroll;


        /// <summary>
        /// 容器的布局方向
        /// </summary>
        private Orientation _panelOrientation;


        /// <summary>
        /// 当前可视化视图的第一项
        /// </summary>
        private int firstVisibleIndex;

        /// <summary>
        /// 当前可视化视图的最后一项
        /// </summary>
        private int lastVisibleIndex;

        /// <summary>
        /// 上次滚动时可视化视图的第一项
        /// </summary>
        private int oldFirstVisibleIndex;

        /// <summary>
        /// 上次滚动时可视化视图的最后一项
        /// </summary>
        private int oldLastVisibleIndex;

        /// <summary>
        /// 标识，是否已找到第一项
        /// </summary>
        private bool isFindFirst;

        /// <summary>
        /// 当前累计已遍历过的Item高度或宽度的值，用于寻找第一项和最后一项
        /// </summary>
        private double cumulativeNum;

        /// <summary>
        /// 列表子控件的高度
        /// </summary>
        private double itemsHeight;
        //平移特效对象
        private TranslateTransform tt;


        public void Attach(DependencyObject associatedObject)
        {
            //获取 ListView 列表对象
            ListView = associatedObject as ListView;

            if (ListView == null)
            {
                return;
            }

            //传进来的 对象不为空，我们就对这个对象注册一个事件
            ListView.Loaded += ListView_Loaded;
        }

        public void Detach()
        {

        }
        /// <summary>
        /// 列表对象加载完成后的逻辑代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            //查找滚动视图，并赋值到我们刚刚添加的属性里
            scroll = FindVisualChild<ScrollViewer>(ListView, "ScrollViewer");
            //判断是否为空，不为空的话就添加一个事件
            if (scroll == null)
            {
                return;
            }
            else
            {
                //监听滚动事件
                scroll.ViewChanged += Scroll_ViewChanged;
            }


            ItemsPresenter v = FindVisualChild<ItemsPresenter>(ListView, "");

            ItemsStackPanel items = FindVisualChild<ItemsStackPanel>(v, "");

            _panelOrientation = items.Orientation;


        }

        /// <summary>
        /// 获取模板控件
        /// </summary>
        /// <typeparam name="T">获取的类型</typeparam>
        /// <param name="obj">控件对象</param>
        /// <returns></returns>
        protected T FindVisualChild<T>(DependencyObject obj, string name) where T : DependencyObject
        {
            //获取控件可视化树中的子对象数量
            int count = VisualTreeHelper.GetChildrenCount(obj);

            //根据索引遍历每一个对象
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                //根据参数判断是不是我们要找的对象，如果是 就返回，并退出该方法，
                //如果不是则再递归到下一层查找
                if (child is T && ((FrameworkElement)child).Name == name)
                {
                    return (T)child;
                }
                else
                {
                    var child1 = FindVisualChild<T>(child, name);

                    if (child1 != null)
                    {
                        return (T)child1;
                    }


                }
            }

            return null;

        }
        /// <summary>
        /// 监听滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //每次滚动时都计算当前可视化区域的首尾项
            CalculationIndex();
        }

        /// <summary>
        /// 计算可视化区域的第一项和最后一项
        /// </summary>
        private void CalculationIndex()
        {
            //赋值旧的第一个可视化视图
            oldFirstVisibleIndex = firstVisibleIndex;
            //赋值最后一个可视化视图
            oldLastVisibleIndex = lastVisibleIndex;

            ;
            //标记第一项是否找到了
            isFindFirst = false;
            //判断列表的方向
            if (_panelOrientation == Orientation.Vertical)
            {
                cumulativeNum = 0.0;


                //遍历列表的全部可视化视图，寻找第一项和最后一项
                for (int i = 0; i < ListView.Items.Count; i++)
                {


                    //转换成 ListViewItem 对象，用于操作
                    var _item = ListView.ContainerFromIndex(i) as ListViewItem;

                    //这里有个坑，应为 ListView 支持虚拟化的，所以每次获取列表的
                    //子项最多只会有20项左右，所以我们要记录一下 高度，将所遍历到的
                    //可视化视图的高度累加。

                    if (_item == null)
                    {

                        cumulativeNum += itemsHeight;
                    }
                    else
                    {
                        itemsHeight = _item.ActualHeight;
                        cumulativeNum += _item.ActualHeight + _item.Margin.Top + _item.Margin.Bottom;

                    }
                    //判断当前所累加的高度大于我们滚动的距离找到 现在显示在屏幕上的第一项
                    if (!isFindFirst && cumulativeNum >= scroll.VerticalOffset)
                    {
                        //记录第一项的索性
                        firstVisibleIndex = i;
                        //表明第一项已经找到了
                        isFindFirst = true;

                        Up();
                    }

                    //当前所累加的高度 大于 当前移动的距离和 滚动视图的可见高度，找出最后一项
                    if (cumulativeNum >= (scroll.VerticalOffset + scroll.ViewportHeight))
                    {
                        //记录最后一项的索引
                        lastVisibleIndex = i;

                        Down();

                        //已经找到的第一项和最后一项了 跳出循环
                        break;
                    }


                    ;

                }
            }

        }

        /// <summary>
        /// 滚动条向下，类容向上移动
        /// </summary>
        private void Down()
        {

            if ((firstVisibleIndex == oldFirstVisibleIndex && lastVisibleIndex == oldLastVisibleIndex) || oldFirstVisibleIndex == 0 && oldLastVisibleIndex == 0)
                return;

            //判断 当前最后一项 是否大于上次移动的最后一项
            if (lastVisibleIndex > oldLastVisibleIndex)
            {
                //获取可视化对象
                var _item = ListView.ContainerFromIndex(lastVisibleIndex) as ListViewItem;
                //
                tt = new TranslateTransform();

                //这里要判断一下 当前可视化是否为空，如果你移动得比较快的化，列表的虚拟化会给不到对象来的。
                if (_item == null)
                {
                    return;
                }


                _item.RenderTransform = tt;

                Storyboard board = new Storyboard();

                //创建一个 double 动画
                DoubleAnimation animation = new DoubleAnimation() { AutoReverse = false, RepeatBehavior = new RepeatBehavior(1), EnableDependentAnimation = true, To = 0, From = _item.ActualWidth / 2, Duration = TimeSpan.FromSeconds(0.2) };
                Storyboard.SetTarget(animation, tt);
                Storyboard.SetTargetProperty(animation, nameof(TranslateTransform.X));

                board.Children.Add(animation);

                board.Begin();




            }
        }

        /// <summary>
        /// 滚动条向上，内容向下
        /// </summary>
        private void Up()
        {
            if (firstVisibleIndex < oldFirstVisibleIndex)
            {
                var _item = ListView.ContainerFromIndex(firstVisibleIndex) as ListViewItem;

                tt = new TranslateTransform();
                if (_item == null)
                {
                    return;
                }
                _item.RenderTransform = tt;



                Storyboard board = new Storyboard();


                DoubleAnimation animation = new DoubleAnimation() { AutoReverse = false, RepeatBehavior = new RepeatBehavior(1), EnableDependentAnimation = true, To = 0, From = _item.ActualWidth / 2, Duration = TimeSpan.FromSeconds(0.3) };
                Storyboard.SetTarget(animation, tt);
                Storyboard.SetTargetProperty(animation, nameof(TranslateTransform.X));

                board.Children.Add(animation);

                board.Begin();


            }
        }
    }
}
