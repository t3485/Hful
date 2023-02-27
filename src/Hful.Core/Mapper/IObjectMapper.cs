using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Core.Mapper
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource obj);

        List<TDestination> MapList<TSource, TDestination>(List<TSource> obj);

        TDestination Map<TSource, TDestination>(TSource obj, TDestination d);
    }
}
