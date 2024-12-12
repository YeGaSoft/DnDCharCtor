using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public interface ISearchable
{
    bool Search(string searchText, bool includePropertyNames);
}
