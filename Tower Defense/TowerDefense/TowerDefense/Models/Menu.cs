using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDance.Models
{
    class Menu
    {
        List<string> _titles = new List<string>();
        int _selectedTitle = 0;

        public Menu(List<string> titles)
        {
            _titles = titles;
        }

        public List<string> getTitles()
        {
            return _titles;
        }

        public void selectNext()
        {
            if (_selectedTitle < _titles.Count - 1)
                _selectedTitle++;
            else
                _selectedTitle = 0;
        }

        public void selectPrevious()
        {
            if (_selectedTitle > 0)
                _selectedTitle--;
            else
                _selectedTitle = _titles.Count - 1;
        }

        public void selectTitle(int index)
        {
            _selectedTitle = 0;
        }

        public void selectTitle(string title)
        {
            int idx = 0;
            foreach (string t in _titles)
            {
                if (t.Equals(title))
                {
                    _selectedTitle = idx;
                    return;
                }
                idx++;
            }
        }

        public int getSelectedTitleIndex()
        {
            return _selectedTitle;
        }

        public string getSelectedTitle()
        {
            return _titles[_selectedTitle];
        }
    }
}
