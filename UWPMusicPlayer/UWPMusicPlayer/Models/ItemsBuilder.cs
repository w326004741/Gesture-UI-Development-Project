using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMusicPlayer.Pages;

namespace UWPMusicPlayer.Models
{
    /// <summary>
    /// create the want menus and setting menu buttom function
    /// 创建你所需要的菜单栏内容，添加菜单栏按钮功能。
    /// </summary>
    public static class ItemsBuilder
    {
        // 添加菜单栏底部功能：反馈 和设置
        // create the buttom of menu: Feedback and Setting.
        private static IList<NavItem> _footItems = null;
        private static IList<NavItem> _navItems = null;

        /// <summary>
        /// Build Menu Item Navigation : PlayList, Recently, Playing. 建立菜单项目导航
        /// </summary>
        /// <returns></returns>
        public static IList<NavItem> BuildNavItems()
        {
            if (_navItems == null)
            {
                _navItems = new List<NavItem>()
                {
                    new NavItem() {Icon = "\xE8D6", Title="PlayList", View=typeof(MyMusicPage)}, //View=typeof(MyMusicPage).
                    new NavItem() { Icon = "\xE121", Title="Recently", View=typeof(RecentlyPage)}, // 把null换成typeof(你想要跳转的页面，like RentlyPage).
                    new NavItem() { Icon = "\xE142", Title="Playing", View=typeof(PlayingPage)}, // View=typeof(PlayingPage)
                };
            }
            return _navItems;
        }


        /// <summary>
        /// Build Menu Foot Item Navigation: Setting, Feedback.   建立菜单底部项目导航
        /// </summary>
        /// <returns></returns>
        public static IList<NavItem> BuildFootItems()
        {
            if (_footItems == null)
            {
                _footItems = new List<NavItem>()
                {                                        // The two page setting is doesn't work????
                    new NavItem() { Icon = "\xE713", Title="Setting", View = typeof(SettingPage) }, //View=typeof(SettingPage)
                    new NavItem() { Icon = "\xE76E", Title="Feedback", View = typeof(FeedbackPage) }, // View=typeof(FeedbackPage)
                };
            }
            return _footItems;
        }

    }
}
