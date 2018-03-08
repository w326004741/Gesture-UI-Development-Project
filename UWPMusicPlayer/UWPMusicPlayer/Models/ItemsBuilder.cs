using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicPlayer.Models
{
    /// <summary>
    /// create the want menus
    /// </summary>
    public static class ItemsBuilder
    {
        // 添加菜单栏底部功能：反馈 和设置
        // create the buttom of menu: Feedback and Setting.
        private static IList<NavItem> _footItems = null;
        private static IList<NavItem> _navItems = null;

        public static IList<NavItem> BuildNavItems()
        {
            if (_navItems == null)
            {
                _navItems = new List<NavItem>()
                {
                    new NavItem() {Icon = "\xE8D6", Title="My Music", View=null}, //View=typeof(MyMusicPage)
                    new NavItem() { Icon = "\xE121", Title="Recently", View=null},
                    new NavItem() { Icon = "\xE142", Title="Playing", View=null},
                };
            }
            return _navItems;
        }



        public static IList<NavItem> BuildFootItems()
        {
            if (_footItems == null)
            {
                _footItems = new List<NavItem>()
                {
                    new NavItem() { Icon = "\xE76E", Title="Feedback", View = null},
                    new NavItem() { Icon = "\xE713", Title="Setting", View = null},
                };
            }
            return _footItems;
        }

    }
}
