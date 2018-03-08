﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicPlayer.Models
{
    /// <summary> 
    ///  Hamburger Menus Class
    ///  汉堡菜单类
    ///   </summary>
    public class NavItem
    {
        // setting icon, title and view
        private string _icon;
        private string _title;
        private Type _view;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Type View
        {
            get { return _view; }
            set { _view = value; }
        }
    }
}
