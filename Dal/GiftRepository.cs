﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Gifter.ViewModel;
using Gifter.Model;
using System.IO;

namespace Gifter.DAL
{
    public class GiftRepository
    {
        private string _path;
        private XElement _root;
        public GiftRepository(string path)
        {
            _path = Path.GetFullPath(path) ;
            if (!File.Exists(path))
            {
                CreateEmptyFile(path);
            }
            else _root = XElement.Load(path);
        }
        public void DeleteAllGifts()
        {
            _root.Descendants("gift").Take(_root.Descendants("gift").Count()).Remove();
            _root.Save(_path);
        }
        public void Delete(GiftViewModel g)
        {
            _root.Descendants("gift").Where(n => n.Element("giftid")
                .Value == g.GiftId.ToString()).Single().Remove();
            _root.Save(_path);
        }
        public void Create(GiftViewModel g)
        {
            if (_root.Descendants("gift").Any())
                g.GiftId = Int32.Parse(_root.Descendants("gift").Last().Element("giftid").Value) + 1;
            else
                g.GiftId = 1;
            _root.Add(new XElement("gift",
                new XElement("giftid", g.GiftId.ToString()),
                new XElement("name", new XCData(g.Name)),
                new XElement("imageurl", g.ImageUrl.ToString()),
                new XElement("description", new XCData(g.Description.ToString()))));
            _root.Save(_path);
        }

        public void CreateEmptyFile(string path)
        {
            _root =
              (
              new XElement("root",
              new XElement("gift",
              new XElement("giftid", "1"),
              new XElement("name", "Nokia lumia"),
              new XElement("imageurl", @"\Images\lumia.png"),
              new XElement("description", "Przykładowy opis..."
              ))));
            _root.Save(path);
        }

        public IEnumerable<GiftViewModel> GetAllGifts()
        {
            var tmp = (from n in _root.Descendants("gift")
                       select
                           new GiftViewModel
                           {
                               GiftId = int.Parse(n.Element("giftid").Value),
                               Name = n.Element("name").Value,
                               ImageUrl = AppDomain.CurrentDomain.BaseDirectory + n.Element("imageurl").Value,
                               Description = n.Element("description").Value
                           });
            _root.Save(_path);
            return tmp.ToList();
        }
    }
}
