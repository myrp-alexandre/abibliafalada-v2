﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sbcore.Persistence;
using Db4objects.Db4o;
using SpokenBible.Presenter;
using sbcore.Model;

namespace SpokenBible.Controller
{
    public class AppController
    {
        public IObjectContainer DefaultContainer { get; set; }

        public void Start()
        {
            DefaultContainer = Container.GetContainer();
            MainPresenter presenter = new MainPresenter(this);
            presenter.ShowView();
        }

        internal void End()
        {
            Container.CloseContainer();
        }
    }
}